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

        for (var target = 0; target < 8; target++)
        {
#pragma warning disable CS8509
            var targetRegister = target switch
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

            _instructions.Add(0x40 + target * 8, new Instruction(_ => LD_R_R((Register) targetRegister!, Register.B), $"LD {targetRegister}, B", 0x40 + target * 8, 0));

            _instructions.Add(0x41 + target * 8, new Instruction(_ => LD_R_R((Register) targetRegister!, Register.C), $"LD {targetRegister}, C", 0x41 + target * 8, 0));

            _instructions.Add(0x42 + target * 8, new Instruction(_ => LD_R_R((Register) targetRegister!, Register.D), $"LD {targetRegister}, D", 0x42 + target * 8, 0));

            _instructions.Add(0x43 + target * 8, new Instruction(_ => LD_R_R((Register) targetRegister!, Register.E), $"LD {targetRegister}, E", 0x43 + target * 8, 0));

            _instructions.Add(0x44 + target * 8, new Instruction(_ => LD_R_R((Register) targetRegister!, Register.H), $"LD {targetRegister}, H", 0x44 + target * 8, 0));

            _instructions.Add(0x45 + target * 8, new Instruction(_ => LD_R_R((Register) targetRegister!, Register.L), $"LD {targetRegister}, L", 0x45 + target * 8, 0));

            _instructions.Add(0x46 + target * 8, new Instruction(_ => LD_R_aRR((Register) targetRegister!, RegisterPair.HL), $"LD {targetRegister}, (HL)", 0x46 + target * 8, 0));

            _instructions.Add(0x47 + target * 8, new Instruction(_ => LD_R_R((Register) targetRegister!, Register.A), $"LD {targetRegister}, A", 0x47 + target * 8, 0));
        }

        _instructions.Add(0xCB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0xCB, 0));

        _instructions.Add(0xDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0xDD, 0));

        _instructions.Add(0xFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0xFD, 0));
    }
}