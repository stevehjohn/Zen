// ReSharper disable StringLiteralTypo

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseBaseInstructions()
    {
        //        _instructions.Add(0x00, new Instruction(_ => NOP(), "NOP", 0x00, 0));

        //        _instructions.Add(0x01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));

        //        _instructions.Add(0x04, new Instruction(_ => INC_R(Register.B), "INC B", 0x04, 0));

        //        _instructions.Add(0x05, new Instruction(_ => DEC_R(Register.B), "DEC B", 0x05, 0));

        //        _instructions.Add(0x07, new Instruction(_ => RLCA(), "RLCA", 0x07, 0));

        //        _instructions.Add(0x0C, new Instruction(_ => INC_R(Register.C), "INC C", 0x04, 0));

        //        _instructions.Add(0x11, new Instruction(d => LD_RR_nn(RegisterPair.DE, d), "LD DE, nn", 0x11, 2));

        //        _instructions.Add(0x14, new Instruction(_ => INC_R(Register.D), "INC D", 0x14, 0));

        //        _instructions.Add(0x15, new Instruction(_ => DEC_R(Register.D), "DEC D", 0x15, 0));

        //        _instructions.Add(0x1C, new Instruction(_ => INC_R(Register.E), "INC E", 0x1C, 0));

        //        _instructions.Add(0x1D, new Instruction(_ => DEC_R(Register.E), "DEC E", 0x1D, 0));

        //        _instructions.Add(0x21, new Instruction(d => LD_RR_nn(RegisterPair.HL, d), "LD HL, nn", 0x21, 2));

        //        _instructions.Add(0x24, new Instruction(_ => INC_R(Register.H), "INC H", 0x24, 0));

        //        _instructions.Add(0x25, new Instruction(_ => DEC_R(Register.H), "DEC H", 0x25, 0));

        //        _instructions.Add(0x2C, new Instruction(_ => INC_R(Register.L), "INC L", 0x2C, 0));

        //        _instructions.Add(0x2D, new Instruction(_ => DEC_R(Register.L), "DEC L", 0x2D, 0));

        //        _instructions.Add(0x34, new Instruction(_ => INC_aRR(RegisterPair.HL), "INC (HL)", 0x34, 0));

        //        _instructions.Add(0x35, new Instruction(_ => DEC_aRR(RegisterPair.HL), "DEC (HL)", 0x35, 0));

        //        _instructions.Add(0x3C, new Instruction(_ => INC_R(Register.A), "INC A", 0x3C, 0));

        //        _instructions.Add(0x3D, new Instruction(_ => DEC_R(Register.A), "DEC A", 0x3D, 0));

        //        _instructions.Add(0x70, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.B), "LD (HL), B", 0x70, 0));

        //        _instructions.Add(0x71, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.C), "LD (HL), C", 0x71, 0));

        //        _instructions.Add(0x72, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.D), "LD (HL), D", 0x72, 0));

        //        _instructions.Add(0x73, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.E), "LD (HL), E", 0x73, 0));

        //        _instructions.Add(0x74, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.H), "LD (HL), H", 0x74, 0));

        //        _instructions.Add(0x75, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.L), "LD (HL), L", 0x75, 0));

        //        _instructions.Add(0x76, new Instruction(_ => HALT(), "HALT", 0x76, 0));

        //        _instructions.Add(0x77, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.A), "LD (HL), A", 0x77, 0));

        //        _instructions.Add(0xA8, new Instruction(_ => XOR_R_R(Register.A, Register.B), "XOR A, B", 0xA8, 0));

        //        _instructions.Add(0xA9, new Instruction(_ => XOR_R_R(Register.A, Register.C), "XOR A, C", 0xA9, 0));

        //        _instructions.Add(0xAA, new Instruction(_ => XOR_R_R(Register.A, Register.D), "XOR A, D", 0xAA, 0));

        //        _instructions.Add(0xAB, new Instruction(_ => XOR_R_R(Register.A, Register.E), "XOR A, E", 0xAB, 0));

        //        _instructions.Add(0xAC, new Instruction(_ => XOR_R_R(Register.A, Register.H), "XOR A, H", 0xAC, 0));

        //        _instructions.Add(0xAD, new Instruction(_ => XOR_R_R(Register.A, Register.L), "XOR A, L", 0xAD, 0));

        //        _instructions.Add(0xAF, new Instruction(_ => XOR_R_R(Register.A, Register.A), "XOR A, A", 0xAF, 0));

        //        for (var r = 0; r < 8; r++)
        //        {
        //#pragma warning disable CS8509
        //            var register = r switch
        //            {
        //                0 => Register.B,
        //                1 => Register.C,
        //                2 => Register.D,
        //                3 => Register.E,
        //                4 => Register.H,
        //                5 => Register.L,
        //                6 => (Register?) null,
        //                7 => Register.A
        //            };
        //#pragma warning restore CS8509

        //            if (r != 6)
        //            {
        //                _instructions.Add(0x40 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.B), $"LD {register}, B", 0x40 + r * 8, 0));

        //                _instructions.Add(0x41 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.C), $"LD {register}, C", 0x41 + r * 8, 0));

        //                _instructions.Add(0x42 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.D), $"LD {register}, D", 0x42 + r * 8, 0));

        //                _instructions.Add(0x43 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.E), $"LD {register}, E", 0x43 + r * 8, 0));

        //                _instructions.Add(0x44 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.H), $"LD {register}, H", 0x44 + r * 8, 0));

        //                _instructions.Add(0x45 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.L), $"LD {register}, L", 0x45 + r * 8, 0));

        //                _instructions.Add(0x46 + r * 8, new Instruction(_ => LD_R_aRR((Register) register!, RegisterPair.HL), $"LD {register}, (HL)", 0x46 + r * 8, 0));

        //                _instructions.Add(0x47 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.A), $"LD {register}, A", 0x47 + r * 8, 0));
        //            }
        //        }

        _instructions.Add(0xCB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0xCB, 0));

        _instructions.Add(0xDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0xDD, 0));

        _instructions.Add(0xFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0xFD, 0));
    }
}