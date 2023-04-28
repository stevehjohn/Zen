// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseFDInstructions()
    {
        _instructions.Add(0xFD00, new Instruction(_ => NOP(), "NOP", 0xFD00, 0, 4));
                
        InitialiseLDByteInstructions(0xFD00);
                
        InitialiseArithmeticLogicInstructions(0xFD00);

        InitialiseFDIncDecInstructions();

        _instructions.Add(0xFDCB, new Instruction(_ => PREFIX(0xFDCB), "PREFIX 0xFDCB", 0xFDCB, 2));
    }

    private void InitialiseFDIncDecInstructions()
    {
        _instructions.Add(0xFD04, new Instruction(_ => INC_R(Register.B), "INC B", 0xFD04, 0, 4));

        _instructions.Add(0xFD05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0xFD05, 0, 4));

        _instructions.Add(0xFD0C, new Instruction(_ => INC_R(Register.C), "INC C", 0xFD0C, 0, 4));

        _instructions.Add(0xFD0D, new Instruction(_ => DEC_R(Register.C), "DEC C", 0xFD0D, 0, 4));

        _instructions.Add(0xFD14, new Instruction(_ => INC_R(Register.D), "INC D", 0xFD14, 0, 4));

        _instructions.Add(0xFD15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0xFD15, 0, 4));

        _instructions.Add(0xFD1C, new Instruction(_ => INC_R(Register.E), "INC E", 0xFD1C, 0, 4));

        _instructions.Add(0xFD1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0xFD1D, 0, 4));

        _instructions.Add(0xFD23, new Instruction(_ => INC_RR(RegisterPair.IY), "INC IY", 0xFD23, 0, 4));

        _instructions.Add(0xFD24, new Instruction(_ => INC_R(Register.IYh), "INC IYh", 0xFD24, 0, 4));

        _instructions.Add(0xFD25, new Instruction(_ => DEC_R(Register.IYh), "DEC IYh", 0xFD25, 0, 4));

        _instructions.Add(0xFD2B, new Instruction(_ => DEC_RR(RegisterPair.IY), "DEC IY", 0xFD2B, 0, 4));

        _instructions.Add(0xFD2C, new Instruction(_ => INC_R(Register.IYl), "INC IYl", 0xFD2C, 0, 4));

        _instructions.Add(0xFD2D, new Instruction(_ => DEC_R(Register.IYl), "DEC IYl", 0xFD2D, 0, 4));

        _instructions.Add(0xFD33, new Instruction(_ => INC_RR(RegisterPair.SP), "INC SP", 0xFD33, 0, 4));
        
        _instructions.Add(0xFD34, new Instruction(p => INC_aRRd(RegisterPair.IY, p), "INC (IY + d)", 0xFD34, 1, 4));

        _instructions.Add(0xFD35, new Instruction(p => DEC_aRRd(RegisterPair.IY, p), "DEC (IY + d)", 0xFD35, 1, 4));

        _instructions.Add(0xFD3C, new Instruction(_ => INC_R(Register.A), "INC A", 0xFD3C, 0, 4));

        _instructions.Add(0xFD3D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0xFD3D, 0, 4));
    }}