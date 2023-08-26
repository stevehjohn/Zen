using Moq;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Zen.Utilities.Infrastructure;
using Zen.Z80.Implementation;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Utilities.Visualisations;

[ExcludeFromCodeCoverage]
public class InstructionTableGenerator
{
    private readonly Instructions _instructions = new(new Interface(new Mock<IPortConnector>().Object, new Mock<IRamConnector>().Object), new State());

    private readonly StringBuilder _output = new();

    private readonly StringBuilder _code = new();

    private readonly OpCodeImplementationInspector _inspector = new();

    public void Generate()
    {
        GenerateBaseInstructionTable();

        var template = File.ReadAllText("Templates\\InstructionTable.html");

        template = template.Replace("<!--CONTENT-->", _output.ToString());

        template = template.Replace("<!--CODE-->", _code.ToString());

        File.WriteAllText("SupportedOpCodes.html", template);

        Process.Start("cmd.exe", "/c SupportedOpCodes.html");
    }

    private void GenerateBaseInstructionTable()
    {
        AddTable();

        AddTable(0xCB00);

        AddTable(0xDD00);

        AddTable(0xDDCB00);

        AddTable(0xED00);

        AddTable(0xFD00);

        AddTable(0xFDCB00);
    }

    private void AddTable(int offset = 0x00)
    {
        var title = "Base";

        if (offset != 0x00)
        {
            title = $"{offset >> 8:X}";
        }

        _output.AppendLine($"<div class=\"table-title\">{title} OpCodes</div>");

        _output.AppendLine("<table>");

        _output.AppendLine("  <tr>");

        _output.AppendLine("    <td></td>");

        for (var x = 0; x < 16; x++)
        {
            _output.AppendLine($"    <th>{x:X1}</th>");
        }

        _output.AppendLine("  </tr>");

        for (var y = 0; y < 16; y++)
        {
            _output.AppendLine("  <tr>");

            _output.AppendLine($"    <th>{y:X1}</th>");

            for (var x = 0; x < 16; x++)
            {
                try
                {
                    var opCode = offset + y * 16 + x;

                    var instruction = _instructions[opCode];

                    var code = _inspector.GetOpCodeImplementation(opCode);

                    _code.Append($"<div class=\"code\" id=\"{opCode}\" onclick=\"this.style.display = 'none';\" data-lit=\"false\">{code}</div>");

                    _output.AppendLine($"    <td class=\"done\" onclick=\"showCode({opCode});\">{instruction.Mnemonic}</td>");
                }
                catch
                {
                    _output.AppendLine("    <td class=\"empty\"></td>");
                }
            }

            _output.AppendLine("  </tr>");
        }

        _output.AppendLine("</table>");
    }
}