// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Z80.Processor;

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDCBFDCBInstructions()
    {
        var groups = new[] { 0xDDCB00, 0xFDCB00 };

        foreach (var group in groups)
        {
            var registerPair = group == 0xDDCB00 ? RegisterPair.IX : RegisterPair.IY;

            InitialiseBitInstructions(group, registerPair);

            InitialiseResSetInstructions(group, registerPair);

            InitialiseShiftInstructions(group, registerPair);
        }
    }

    private void InitialiseBitInstructions(int prefix, RegisterPair registerPair)
    {
        for (var bit = 0; bit < 8; bit++)
        {
            var bitMask = (byte) (1 << bit);

            for (var repeat = 0; repeat < 8; repeat++)
            {
                var opCode = prefix + 0x40 + bit * 8 + repeat;

                _instructions.Add(opCode, new Instruction(p => BIT_b_aRRd(bitMask, registerPair, p), $"BIT {bit}, ({registerPair} + d)", opCode, 2));
            }
        }
    }

    private void InitialiseResSetInstructions(int prefix, RegisterPair registerPair)
    {
        for (var bit = 0; bit < 8; bit++)
        {
            var bitMask = (byte) (1 << bit);

            for (var registerNumber = 0; registerNumber < 8; registerNumber++)
            {
                var opCode = prefix + bit * 8 + registerNumber;

                if (registerNumber != 6)
                {
                    var register = registerNumber switch
                    {
                        0 => Register.B,
                        1 => Register.C,
                        2 => Register.D,
                        3 => Register.E,
                        4 => Register.H,
                        5 => Register.L,
                        7 => Register.A
                    };

                    _instructions.Add(opCode + 0x80, new Instruction(p => RES_b_aRRd_R(bitMask, registerPair, p, register), $"RES {bit}, ({registerPair} + d), {register}", opCode + 0x80, 2));

                    _instructions.Add(opCode + 0xC0, new Instruction(p => SET_b_aRRd_R(bitMask, registerPair, p, register), $"SET {bit}, ({registerPair} + d), {register}", opCode + 0xC0, 2));
                }
                else
                {
                    _instructions.Add(opCode + 0x80, new Instruction(p => RES_b_aRRd(bitMask, registerPair, p), $"RES {bit}, ({registerPair} + d)", opCode + 0x80, 2));

                    _instructions.Add(opCode + 0xC0, new Instruction(p => SET_b_aRRd(bitMask, registerPair, p), $"SET {bit}, ({registerPair} + d)", opCode + 0xC0, 2));
                }
            }
        }
    }

    private void InitialiseShiftInstructions(int prefix, RegisterPair registerPair)
    {
        for (var registerNumber = 0; registerNumber < 8; registerNumber++)
        {
            var opCode = prefix + registerNumber;

            if (registerNumber != 6)
            {
                var register = registerNumber switch
                {
                    0 => Register.B,
                    1 => Register.C,
                    2 => Register.D,
                    3 => Register.E,
                    4 => Register.H,
                    5 => Register.L,
                    7 => Register.A
                };

                _instructions.Add(opCode, new Instruction(p => RLC_aRRd_R(registerPair, p, register), $"RLC ({registerPair} + d), {register}", opCode, 2));

                _instructions.Add(opCode + 0x08, new Instruction(p => RRC_aRRd_R(registerPair, p, register), $"RRC ({registerPair} + d), {register}", opCode + 0x08, 2));

                _instructions.Add(opCode + 0x10, new Instruction(p => RL_aRRd_R(registerPair, p, register), $"RL ({registerPair} + d), {register}", opCode + 0x10, 2));

                _instructions.Add(opCode + 0x18, new Instruction(p => RR_aRRd_R(registerPair, p, register), $"RR ({registerPair} + d), {register}", opCode + 0x18, 2));

                _instructions.Add(opCode + 0x20, new Instruction(p => SLA_aRRd_R(registerPair, p, register), $"SLA ({registerPair} + d), {register}", opCode + 0x20, 2));

                _instructions.Add(opCode + 0x28, new Instruction(p => SRA_aRRd_R(registerPair, p, register), $"SRA ({registerPair} + d), {register}", opCode + 0x28, 2));

                _instructions.Add(opCode + 0x30, new Instruction(p => SLL_aRRd_R(registerPair, p, register), $"SLL ({registerPair} + d), {register}", opCode + 0x30, 2));

                _instructions.Add(opCode + 0x38, new Instruction(p => SRL_aRRd_R(registerPair, p, register), $"SRL ({registerPair} + d), {register}", opCode + 0x38, 2));
            }
            else
            {
                _instructions.Add(opCode, new Instruction(p => RLC_aRRd(registerPair, p), $"RLC ({registerPair} + d)", opCode, 2));

                _instructions.Add(opCode + 0x08, new Instruction(p => RRC_aRRd(registerPair, p), $"RRC ({registerPair} + d)", opCode + 0x08, 2));

                _instructions.Add(opCode + 0x10, new Instruction(p => RL_aRRd(registerPair, p), $"RL ({registerPair} + d)", opCode + 0x10, 2));

                _instructions.Add(opCode + 0x18, new Instruction(p => RR_aRRd(registerPair, p), $"RR ({registerPair} + d)", opCode + 0x18, 2));

                _instructions.Add(opCode + 0x20, new Instruction(p => SLA_aRRd(registerPair, p), $"SLA ({registerPair} + d)", opCode + 0x20, 2));

                _instructions.Add(opCode + 0x28, new Instruction(p => SRA_aRRd(registerPair, p), $"SRA ({registerPair} + d)", opCode + 0x28, 2));

                _instructions.Add(opCode + 0x30, new Instruction(p => SLL_aRRd(registerPair, p), $"SLL ({registerPair} + d)", opCode + 0x30, 2));

                _instructions.Add(opCode + 0x38, new Instruction(p => SRL_aRRd(registerPair, p), $"SRL ({registerPair} + d)", opCode + 0x38, 2));
            }
        }
    }
}