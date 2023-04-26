namespace Zen.Z80.Implementation;

public class Instruction
{
    public Action<byte[]> Execute { get; private set; }

    public string Mnemonic { get; private set; }

    public int OpCode { get; private set; }

    public int ParameterLength { get; private set; }

    public string? TraceTemplate { get; private set; }

    public byte ExtraCycles { get; private set; }

    public Instruction(Action<byte[]> execute, string mnemonic, int opCode, int parameterLength, string? traceTemplate = null, byte extraCycles = 0)
    {
        Execute = execute;

        Mnemonic = mnemonic;

        OpCode = opCode;

        ParameterLength = parameterLength;

        TraceTemplate = traceTemplate;

        ExtraCycles = extraCycles;
    }
}