namespace Zen.Utilities.Models;

public class OpcodeMetadata
{
    public string BaseMnemonic { get; set; }

    public string Mnemonic { get; set; }

    public int[] OpCode { get; set; }

    public string OpCodeHex { get; set; }

    public OperandMetadata[] Operands { get; set; }
}