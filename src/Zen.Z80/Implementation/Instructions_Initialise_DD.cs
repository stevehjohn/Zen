// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDInstructions()
    {
        _instructions.Add(0xDD00, new Instruction(_ => NOP(), "NOP", 0xDD00, 0, 4));
        
        InitialiseLDByteInstructions(0xDD00);
        
        InitialiseArithmeticLogicInstructions(0xDD00);
        
        InitialiseDDIncDecInstructions();

        _instructions.Add(0xDDCB, new Instruction(_ => PREFIX(0xDDCB), "PREFIX 0xDDCB", 0xDDCB, 2));
    }

    private void InitialiseDDIncDecInstructions()
    {
        _instructions.Add(0xDD04, new Instruction(_ => INC_R(Register.B), "INC B", 0xDD04, 0, 4));

        _instructions.Add(0xDD05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0xDD05, 0, 4));

        _instructions.Add(0xDD0C, new Instruction(_ => INC_R(Register.C), "INC C", 0xDD0C, 0, 4));

        _instructions.Add(0xDD0D, new Instruction(_ => DEC_R(Register.C), "DEC C", 0xDD0D, 0, 4));

        _instructions.Add(0xDD14, new Instruction(_ => INC_R(Register.D), "INC D", 0xDD14, 0, 4));

        _instructions.Add(0xDD15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0xDD15, 0, 4));

        _instructions.Add(0xDD1C, new Instruction(_ => INC_R(Register.E), "INC E", 0xDD1C, 0, 4));

        _instructions.Add(0xDD1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0xDD1D, 0, 4));

        _instructions.Add(0xDD23, new Instruction(_ => INC_RR(RegisterPair.IX), "INC IX", 0xDD23, 0, 4));

        _instructions.Add(0xDD24, new Instruction(_ => INC_R(Register.IXh), "INC IXh", 0xDD24, 0, 4));

        _instructions.Add(0xDD25, new Instruction(_ => DEC_R(Register.IXh), "DEC IXh", 0xDD25, 0, 4));

        _instructions.Add(0xDD2B, new Instruction(_ => DEC_RR(RegisterPair.IX), "DEC IX", 0xDD2B, 0, 4));

        _instructions.Add(0xDD2C, new Instruction(_ => INC_R(Register.IXl), "INC IXl", 0xDD2C, 0, 4));

        _instructions.Add(0xDD2D, new Instruction(_ => DEC_R(Register.IXl), "DEC IXl", 0xDD2D, 0, 4));

        _instructions.Add(0xDD33, new Instruction(_ => INC_RR(RegisterPair.SP), "INC SP", 0xDD33, 0, 4));
        
        _instructions.Add(0xDD34, new Instruction(p => INC_aRRd(RegisterPair.IX, p), "INC (IX + d)", 0xDD34, 1, 4));

        _instructions.Add(0xDD35, new Instruction(p => DEC_aRRd(RegisterPair.IX, p), "DEC (IX + d)", 0xDD35, 1, 4));

        _instructions.Add(0xDD3C, new Instruction(_ => INC_R(Register.A), "INC A", 0xDD3C, 0, 4));

        _instructions.Add(0xDD3D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0xDD3D, 0, 4));
    }
}