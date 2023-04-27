// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Microsoft.Win32;
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
                }
                else
                {
                    _instructions.Add(opCode, new Instruction(_ => BIT_b_aRR(bitMask, RegisterPair.HL), $"BIT {bit}, HL", opCode, 0));
                }
            }
        }
    }
}