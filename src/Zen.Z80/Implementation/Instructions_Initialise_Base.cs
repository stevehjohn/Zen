// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseBaseInstructions()
    {
        _instructions.Add(0x00, new Instruction(_ => NOP(), "NOP", 0x00, 0));

        InitialiseLDByteInstructions();

        InitialiseArithmeticLogicInstructions();

        InitialiseRETInstructions();

        _instructions.Add(0x01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));

        _instructions.Add(0xCB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0xCB, 0));

        _instructions.Add(0xDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0xDD, 0));

        _instructions.Add(0xFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0xFD, 0));
    }

    private void InitialiseRETInstructions()
    {
        _instructions.Add(0xC0, new Instruction(_ => RET(Flag.Zero, true), "RET NZ", 0xC0, 0));

        _instructions.Add(0xD0, new Instruction(_ => RET(Flag.Carry, true), "RET NC", 0xD0, 0));

        _instructions.Add(0xE0, new Instruction(_ => RET(Flag.ParityOverflow, true), "RET PO", 0xE0, 0));

        _instructions.Add(0xF0, new Instruction(_ => RET(Flag.Sign, true), "RET NS", 0xF0, 0));

        _instructions.Add(0xC8, new Instruction(_ => RET(Flag.Zero), "RET Z", 0xC8, 0));

        _instructions.Add(0xD8, new Instruction(_ => RET(Flag.Carry), "RET C", 0xD8, 0));

        _instructions.Add(0xE8, new Instruction(_ => RET(Flag.ParityOverflow), "RET PE", 0xE8, 0));

        _instructions.Add(0xF8, new Instruction(_ => RET(Flag.Sign), "RET S", 0xF8, 0));
    }
}