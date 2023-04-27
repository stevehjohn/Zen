// ReSharper disable InconsistentNaming
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseCBInstructions()
    {
        _instructions.Add(0xCB00, new Instruction(_ => RLC_R(Register.B), "RLC B", 0xCB00, 0));

        _instructions.Add(0xCB01, new Instruction(_ => RLC_R(Register.C), "RLC C", 0xCB01, 0));

        _instructions.Add(0xCB02, new Instruction(_ => RLC_R(Register.D), "RLC D", 0xCB02, 0));

        _instructions.Add(0xCB03, new Instruction(_ => RLC_R(Register.E), "RLC E", 0xCB03, 0));

        _instructions.Add(0xCB04, new Instruction(_ => RLC_R(Register.H), "RLC H", 0xCB04, 0));

        _instructions.Add(0xCB05, new Instruction(_ => RLC_R(Register.L), "RLC L", 0xCB05, 0));

        _instructions.Add(0xCB06, new Instruction(_ => RLC_aRR(RegisterPair.HL), "RLC (HL)", 0xCB06, 0));

        _instructions.Add(0xCB07, new Instruction(_ => RLC_R(Register.A), "RLC A", 0xCB07, 0));
    }
}