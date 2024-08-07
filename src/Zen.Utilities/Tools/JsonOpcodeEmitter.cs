﻿using Moq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Zen.Utilities.Models;
using Zen.Z80.Implementation;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Utilities.Tools;

public class JsonOpcodeEmitter
{
    private readonly Instructions _instructions;

    private readonly List<OpcodeMetadata> _opCodes = new();

    private readonly string _initialisationCode;

    private readonly string _methodCode;

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public JsonOpcodeEmitter()
    {
        _instructions = new Instructions(new Interface(new Mock<IPortConnector>().Object, new Mock<IRamConnector>().Object), new State());

        _initialisationCode = LoadInitialisationCode();

        _methodCode = LoadMethodCode();
    }

    public void Emit()
    {
        _opCodes.Clear();

        AddSubset();

        AddSubset(0xCB00);

        AddSubset(0xDD00);

        AddSubset(0xDDCB00);

        AddSubset(0xED00);

        AddSubset(0xFD00);

        AddSubset(0xFDCB00);

        var output = JsonSerializer.Serialize(_opCodes, new JsonSerializerOptions
                                                        {
                                                            WriteIndented = true,
                                                            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                                                            Converters = { new JsonStringEnumConverter() }
                                                        });

        File.WriteAllText("Instructions.json", output);
    }

    private void AddSubset(int offset = 0x00)
    {
        for (var i = 0; i < 256; i++)
        {
            AddOpcode(offset + i);
        }
    }

    private void AddOpcode(int opCode)
    {
        Instruction instruction;

        try
        {
            instruction = _instructions[opCode];
        }
        catch
        {
            return;
        }

        var tempOpCode = opCode;

        var opCodeHex = new StringBuilder();

        var opCodeParts = new List<int>();

        if (opCode == 0)
        {
            opCodeHex.Append("0x00");

            opCodeParts.Add(0);
        }
        else
        {
            while (tempOpCode > 0)
            {
                opCodeHex.Insert(0, $"{tempOpCode & 0xFF:X2} ");

                opCodeParts.Insert(0, tempOpCode & 0xFF);

                tempOpCode >>= 8;
            }

            opCodeHex.Insert(0, "0x");
        }

        var parts = SplitMnemonic(instruction.Mnemonic);

        var operands = new List<OperandMetadata>();

        string conditionFlag = null;

        if (parts[0] == "DJNZ")
        {
            conditionFlag = "NZ";
        }

        for (var i = 1; i < parts.Length; i++)
        {
            var part = parts[i].Replace(",", string.Empty);

            if (part.StartsWith("0x"))
            {
                operands.Add(new OperandMetadata { Name = part, Type = OperandType.Integrated });

                continue;
            }

            if (char.IsNumber(part[0]))
            {
                operands.Add(new OperandMetadata { Name = part, Type = OperandType.Integrated });

                continue;
            }

            if (part.Contains('n'))
            {
                operands.Add(new OperandMetadata { Bytes = instruction.ParameterLength, Immediate = part[0] != '(', Name = $"n{instruction.ParameterLength * 8}", Type = OperandType.Parameter });

                continue;
            }

            if (part == "e")
            {
                operands.Add(new OperandMetadata { Bytes = 1, Name = "e", Type = OperandType.Parameter });

                continue;
            }

            if (part.Contains('+'))
            {
                operands.Add(new OperandMetadata { Bytes = 1, Name = part, Type = OperandType.RegisterWithDisplacement, Immediate = false });
            }

            if (parts[0] is "RET" or "JP" or "JR" or "CALL" && part[0] != '(')
            {
                conditionFlag = part;

                continue;
            }

            operands.Add(new OperandMetadata { Bytes = null, Immediate = part[0] != '(', Name = part.Replace("(", string.Empty).Replace(")", string.Empty), Type = OperandType.Register, RegisterSizeBytes = part.Length == 1 ? 1 : 2 });
        }

        var method = GetCalledMethod(opCode);

        var code = GetMethodCode(method);

        var metadata = new OpcodeMetadata
                       {
                           BaseMnemonic = parts[0],
                           ConditionFlag = conditionFlag,
                           Mnemonic = instruction.Mnemonic,
                           OpCode = opCodeParts.ToArray(),
                           OpCodeHex = opCodeHex.ToString().Trim(),
                           Operands = operands.ToArray(),
                           Cycles = GetCycles(code),
                           AffectedFlags = GetAffectedFlags(code)
                       };

        _opCodes.Add(metadata);
    }

    private static string[] SplitMnemonic(string mnemonic)
    {
        var parts = mnemonic.Split(' ');

        if (! mnemonic.Contains('+'))
        {
            return parts;
        }

        var returnParts = new List<string>();

        for (var i = 0; i < parts.Length; i++)
        {
            var part = parts[i];

            if (! part.StartsWith('('))
            {
                returnParts.Add(part);

                continue;
            }

            returnParts.Add($"{part} {parts[i + 1]} {parts[i + 2]}");

            i += 2;
        }

        return returnParts.ToArray();
    }

    private static string LoadInitialisationCode()
    {
        var files = Directory.EnumerateFiles("../../../../../src/Zen.Z80/Implementation", "Instructions_Initialise_*");

        var data = new StringBuilder();

        foreach (var file in files)
        {
            data.Append(File.ReadAllText(file));

            data.AppendLine();
        }

        return data.ToString();
    }

    private static string LoadMethodCode()
    {
        var files = Directory.EnumerateFiles("../../../../../src/Zen.Z80/Implementation", "Instructions*");

        var data = new StringBuilder();

        foreach (var file in files)
        {
            if (! char.IsAsciiLetter(Path.GetFileName(file).Replace("Instructions", string.Empty)[0]))
            {
                continue;
            }

            data.Append(File.ReadAllText(file));

            data.AppendLine();
        }

        return data.ToString();
    }

    private string GetCalledMethod(int opCode)
    {
        var initialisationIndex = _initialisationCode.IndexOf($"0x{opCode:X6}", StringComparison.InvariantCultureIgnoreCase);

        var lambdaIndex = _initialisationCode.IndexOf("=>", initialisationIndex, StringComparison.InvariantCultureIgnoreCase);

        var methodNameEnd = _initialisationCode.IndexOf("(", lambdaIndex, StringComparison.InvariantCultureIgnoreCase);

        var methodString = _initialisationCode.Substring(lambdaIndex, methodNameEnd - lambdaIndex);

        methodString = methodString.Replace("=>", string.Empty);

        return methodString.Trim();
    }

    private string GetMethodCode(string methodName)
    {
        var index = _methodCode.IndexOf(methodName, StringComparison.InvariantCulture);

        var braceCount = -1;

        var code = new StringBuilder();
        
        while (braceCount != 0)
        {
            var character = _methodCode[index];

            if (character == '{')
            {
                if (braceCount == -1)
                {
                    braceCount = 1;
                }
                else
                {
                    braceCount++;
                }
            }
            else if (character == '}')
            {
                braceCount--;
            }

            code.Append(character);

            index++;
        }

        return code.ToString();
    }

    private static int[] GetCycles(string methodCode)
    {
        var cyclesIndex = methodCode.IndexOf(".SetMCycles", StringComparison.InvariantCulture);

        if (cyclesIndex < 0)
        {
            return Array.Empty<int>();
        }

        cyclesIndex = methodCode.IndexOf("(", cyclesIndex, StringComparison.InvariantCulture);

        var cyclesEndIndex = methodCode.IndexOf(")", cyclesIndex, StringComparison.InvariantCulture);

        var cyclesCode = methodCode.Substring(cyclesIndex + 1, cyclesEndIndex - cyclesIndex - 1);

        var cycles = cyclesCode.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);

        return cycles.ToArray();
    }

    private static string[] GetAffectedFlags(string code)
    {
        var lines = code.Split(new [] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var flags = new List<string>();

        foreach (var line in lines)
        {
            if (! line.StartsWith("_state[Flag."))
            {
                continue;
            }

            var flag = line.Replace("_state[", string.Empty);

            flag = flag.Substring(0, flag.IndexOf(']'));

            flag = flag.Replace("Flag.", string.Empty);

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
            flags.Add(flag switch
                      {
                "Carry" => "C",
                "AddSubtract" => "N",
                "ParityOverflow" => "PV",
                "X1" => "X",
                "HalfCarry" => "H",
                "X2" => "X2",
                "Zero" => "Z",
                "Sign" => "S"
            });
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        }

        return flags.ToArray();
    }
}