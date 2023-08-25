namespace Zen.Utilities.Models;

public class OperandMetadata
{
    public string Name { get; set; }

    public bool? Immediate { get; set; }

    public int? Bytes { get; set; }

    public OperandType Type { get; set; }

    public int? RegisterSizeBytes { get; set; }
}