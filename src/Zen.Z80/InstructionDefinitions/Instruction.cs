namespace Zen.Z80.InstructionDefinitions;

public class Instruction
{
    public Func<byte[], int> Execute { get; private set; }

    public string Mnemonic { get; private set; }

    public uint OpCode { get; private set; }

    public int ParameterLength { get; private set; }

    public string? TraceTemplate { get; private set; }

    public Instruction(Func<byte[], int> execute, string mnemonic, uint opCode, int parameterLength, string? traceTemplate = null)
    {
        Execute = execute;

        Mnemonic = mnemonic;

        OpCode = opCode;

        ParameterLength = parameterLength;

        TraceTemplate = traceTemplate;
    }
}