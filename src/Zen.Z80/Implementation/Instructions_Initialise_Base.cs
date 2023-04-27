using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseBaseInstructions()
    {
        _instructions.Add(0x00, new Instruction(_ => NOP(), "NOP", 0x00, 0));

        _instructions.Add(0x01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));

        _instructions.Add(0x07, new Instruction(_ => RLC_R(Register.A), "RLC A", 0x07, 0));

        _instructions.Add(0x11, new Instruction(d => LD_RR_nn(RegisterPair.DE, d), "LD DE, nn", 0x11, 2));

        _instructions.Add(0x21, new Instruction(d => LD_RR_nn(RegisterPair.HL, d), "LD HL, nn", 0x21, 2));

        for (var r = 0; r < 8; r++)
        {
#pragma warning disable CS8509
            var register = r switch
            {
                0 => Register.B,
                1 => Register.C,
                2 => Register.D,
                3 => Register.E,
                4 => Register.H,
                5 => Register.L,
                6 => (Register?) null,
                7 => Register.A
            };
#pragma warning restore CS8509

            if (r == 6)
            {
                _instructions.Add(0x40 + r * 8, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.B), "LD (HL), B", 0x40 + r * 8, 0));

                _instructions.Add(0x41 + r * 8, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.C), "LD (HL), C", 0x41 + r * 8, 0));

                _instructions.Add(0x42 + r * 8, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.D), "LD (HL), D", 0x42 + r * 8, 0));

                _instructions.Add(0x43 + r * 8, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.E), "LD (HL), E", 0x43 + r * 8, 0));

                _instructions.Add(0x44 + r * 8, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.H), "LD (HL), H", 0x44 + r * 8, 0));

                _instructions.Add(0x45 + r * 8, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.L), "LD (HL), L", 0x45 + r * 8, 0));

                _instructions.Add(0x46 + r * 8, new Instruction(_ => HALT(), "HALT", 0x46 + r * 8, 0));

                _instructions.Add(0x47 + r * 8, new Instruction(_ => LD_aRR_R(RegisterPair.HL, Register.A), "LD (HL), A", 0x47 + r * 8, 0));
            }
            else
            {
                _instructions.Add(0x04 + r * 8, new Instruction(_ => INC_R((Register) register!), $"INC {register}", 0x04 + r * 8, 0));

                _instructions.Add(0x05 + r * 8, new Instruction(_ => DEC_R((Register) register!), $"DEC {register}", 0x05 + r * 8, 0));

                _instructions.Add(0x40 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.B), $"LD {register}, B", 0x40 + r * 8, 0));

                _instructions.Add(0x41 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.C), $"LD {register}, C", 0x41 + r * 8, 0));

                _instructions.Add(0x42 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.D), $"LD {register}, D", 0x42 + r * 8, 0));

                _instructions.Add(0x43 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.E), $"LD {register}, E", 0x43 + r * 8, 0));

                _instructions.Add(0x44 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.H), $"LD {register}, H", 0x44 + r * 8, 0));

                _instructions.Add(0x45 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.L), $"LD {register}, L", 0x45 + r * 8, 0));

                _instructions.Add(0x46 + r * 8, new Instruction(_ => LD_R_aRR((Register) register!, RegisterPair.HL), $"LD {register}, (HL)", 0x46 + r * 8, 0));

                _instructions.Add(0x47 + r * 8, new Instruction(_ => LD_R_R((Register) register!, Register.A), $"LD {register}, A", 0x47 + r * 8, 0));
            }
        }

        _instructions.Add(0xCB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0xCB, 0));

        _instructions.Add(0xDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0xDD, 0));

        _instructions.Add(0xFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0xFD, 0));
    }
}