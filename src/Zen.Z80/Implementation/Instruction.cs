﻿namespace Zen.Z80.Implementation;

public class Instruction
{
    public Action<byte[]> Execute { get; private set; }

    public string Mnemonic { get; private set; }

    public uint OpCode { get; private set; }

    public int ParameterLength { get; private set; }

    public string? TraceTemplate { get; private set; }

    public Instruction(Action<byte[]> execute, string mnemonic, uint opCode, int parameterLength, string? traceTemplate = null)
    {
        Execute = execute;

        Mnemonic = mnemonic;

        OpCode = opCode;

        ParameterLength = parameterLength;

        TraceTemplate = traceTemplate;
    }
}