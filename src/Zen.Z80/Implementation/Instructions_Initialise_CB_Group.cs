// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Z80.Processor;

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseCBGroupInstructions()
    {
        InitialiseCBBitResSetInstructions();
    }

    private void InitialiseCBBitResSetInstructions()
    {
        for (var bit = 0; bit < 8; bit++)
        {
            var bitMask = (byte) (1 << bit);

            for (var registerNumber = 0; registerNumber < 8; registerNumber++)
            {
                var opCode = 0xCB40 + bit * 8 + registerNumber;

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

                    _instructions.Add(opCode, new Instruction(_ => BIT_b_R(bitMask, register), $"BIT {bit}, {register}", opCode, 0));

                    _instructions.Add(opCode + 0x40, new Instruction(_ => RES_b_R(bitMask, register), $"RES {bit}, {register}", opCode + 0x40, 0));

                    _instructions.Add(opCode + 0x80, new Instruction(_ => SET_b_R(bitMask, register), $"SET {bit}, {register}", opCode + 0x80, 0));
                }
                else
                {
                    _instructions.Add(opCode, new Instruction(_ => BIT_b_aRR(bitMask, RegisterPair.HL), $"BIT {bit}, (HL)", opCode, 0));

                    _instructions.Add(opCode + 0x40, new Instruction(_ => RES_b_aRR(bitMask, RegisterPair.HL), $"RES {bit}, (HL)", opCode + 0x40, 0));

                    _instructions.Add(opCode + 0x80, new Instruction(_ => SET_b_aRR(bitMask, RegisterPair.HL), $"SET {bit}, (HL)", opCode + 0x80, 0));
                }
            }
        }
    }
}