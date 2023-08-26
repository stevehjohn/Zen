using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Moq;
using Zen.Utilities.Models;
using Zen.Z80.Implementation;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Utilities.Tools;

public class JsonOpcodeEmitter
{
    private readonly Instructions _instructions;

    private readonly List<OpcodeMetadata> _opCodes = new();

    // ReSharper disable once ConvertConstructorToMemberInitializers
    public JsonOpcodeEmitter()
    {
        _instructions = new Instructions(new Interface(new Mock<IPortConnector>().Object, new Mock<IRamConnector>().Object), new State());
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

            operands.Add(new OperandMetadata { Bytes = null, Immediate = part[0] != '(', Name = part.Replace("(", string.Empty).Replace(")", string.Empty), Type = OperandType.Register, RegisterSizeBytes = part.Length == 2 ? 2 : 1 });
        }

        // TODO: Compound operands (eg IX + d)
        // TODO: Affected flags.
        // TODO: Cycles.

        var metadata = new OpcodeMetadata
                       {
                           BaseMnemonic = parts[0],
                           ConditionFlag = conditionFlag,
                           Mnemonic = instruction.Mnemonic,
                           OpCode = opCodeParts.ToArray(),
                           OpCodeHex = opCodeHex.ToString().Trim(),
                           Operands = operands.ToArray()
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
}