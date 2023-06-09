﻿using Moq;
using System.Text;
using Zen.Z80.Implementation;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Utilities.Tools;

public class CodeGenerator
{
    private const string FileName = "codes.cs";

    private readonly Instructions _instructions;

    public CodeGenerator()
    {
        _instructions = new(new Interface(new Mock<IPortConnector>().Object, new Mock<IRamConnector>().Object), new State());
    }

    public void GenerateOpCodeInitialisers()
    {
        var prefixes = new[] { 0x000000, 0x00CB00, 0x00DD00, 0xDDCB00, 0x00ED00, 0x00FD00, 0xFDCB00 };

        File.Delete(FileName);

        foreach (var prefix in prefixes)
        {
            if (prefix > 0)
            {
                File.AppendAllText(FileName, $"// 0x{prefix >> 8:X4} Instructions\n\n");
            }
            else
            {
                File.AppendAllText(FileName, "// Base Instructions\n\n");
            }

            for (var code = 0; code < 256; code++)
            {
                Instruction instruction;

                var opcode = prefix | code;

                try
                {
                    instruction = _instructions[opcode];
                }
                catch
                {
                    continue;
                }

                var line = new StringBuilder();

                line.Append("_instructions.Add(");

                line.Append($"0x{opcode:X6}, ");

                line.Append($"new Instruction({GenerateMethodCall(instruction)}, ");

                line.Append($"\"{instruction.Mnemonic}\", ");

                line.Append($"0x{opcode:X6}, ");

                line.Append($"{instruction.ParameterLength}");

                if (instruction.ExtraCycles > 0)
                {
                    line.Append($", {instruction.ExtraCycles}");
                }

                line.AppendLine("));\n");

                File.AppendAllText(FileName, line.ToString());
            }
        }
    }

    private static string GenerateMethodCall(Instruction instruction)
    {
        var parts = instruction.Mnemonic.Split(new[] { ' ', ',', '+' }, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length == 1)
        {
            return $"_ => {parts[0]}()";
        }

        if (parts[0] == "IM")
        {
            return $"_ => IM(InterruptMode.{(InterruptMode) int.Parse(parts[1])})";
        }

        var method = new StringBuilder();

        method.Append(parts[0]);

        var parameters = new StringBuilder();

        var lambda = "_ => ";

        var not = false;

        foreach (var part in parts[1..])
        {
            var components = GenerateComponents(parts[0], part);

            not |= components.Not;

            method.Append(components.MethodSuffix);

            if (components.Parameter == "p" && lambda == "p => ")
            {
                continue;
            }

            if (parameters.Length > 0 && ! string.IsNullOrEmpty(components.Parameter))
            {
                parameters.Append(", ");
            }

            parameters.Append(components.Parameter);

            if (components.Parameter == "p")
            {
                lambda = "p => ";
            }
        }

        if (not)
        {
            parameters.Append(", true");
        }

        if (parameters.ToString() == "p")
        {
            return method.ToString();
        }

        return $"{lambda}{method}({parameters})";
    }

    private static (string MethodSuffix, string Parameter, bool Not) GenerateComponents(string mnemonic, string part)
    {
        if (part.StartsWith("0x"))
        {
            return (string.Empty, part, false);
        }

        if (part == "d)")
        {
            return ("d", "p", false);
        }

        if (part.Length == 1 && char.IsNumber(part[0]))
        {
            return ("_b", $"0x{1 << int.Parse(part):X2}", false);
        }

        if (part == "(C)")
        {
            return ("_C", string.Empty, false);
        }

        var suffix = new StringBuilder();

        suffix.Append("_");

        string argument;

        if (part[0] == '(')
        {
            suffix.Append("a");

            if (part[^1] == ')')
            {
                argument = part[1..^1];
            }
            else
            {
                argument = part[1..];
            }
        }
        else
        {
            argument = part;
        }

        var parameter = string.Empty;

        if (mnemonic is "CALL" or "JP" or "JR" or "RET" && ! (part[0] is '(' or 'n' or 'e'))
        {
            // TODO: NOT

            string flag;

            switch (part)
            {
                case "C":
                case "NC":
                    flag = "Carry";

                    break;
                case "S":
                case "NS":
                    flag = "Sign";

                    break;
                case "Z":
                case "NZ":
                    flag = "Zero";

                    break;
                case "PO":
                case "PE":
                    flag = "ParityOverflow";

                    break;
                default:
                    // TODO: Proper exception?
                    throw new Exception($"Unrecognised flag {part}.");
            }

            return ("_F", $"Flag.{flag}", part[0] == 'N' || part == "PO");
        }

        switch (argument)
        {
            case "AF":
            case "BC":
            case "DE":
            case "HL":
            case "IX":
            case "IY":
            case "SP":
                suffix.Append("RR");
                parameter = $"RegisterPair.{argument}";

                break;

            case "AF'":
            case "BC'":
            case "DE'":
            case "HL'":
                suffix.Append("RR");
                parameter = $"RegisterPair.{argument[..^1]}_";
                break;

            case "A":
            case "F":
            case "B":
            case "C":
            case "D":
            case "E":
            case "H":
            case "L":
            case "I":
            case "R":
            case "IXh":
            case "IXl":
            case "IYh":
            case "IYl":
                suffix.Append("R");
                parameter = $"Register.{argument}";

                break;

            case "n":
            case "nn":
            case "e":
                suffix.Append(argument);
                parameter = "p";

                break;

            default:
                suffix.Append(argument);

                break;
        }

        return (suffix.ToString(), parameter, false);
    }
}