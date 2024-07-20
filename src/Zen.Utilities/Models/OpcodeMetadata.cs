// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Zen.Utilities.Models;

public class OpcodeMetadata
{
    public string BaseMnemonic { get; set; }

    public string Mnemonic { get; set; }

    public int[] OpCode { get; set; }

    public string OpCodeHex { get; set; }

    public OperandMetadata[] Operands { get; set; }

    public string ConditionFlag { get; set; }

    public string[] AffectedFlags { get; set; }

    public int[] Cycles { get; set; }
}