// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDCBInstructions()
    {
        for (var bit = 0; bit < 8; bit++)
        {
            var innerBit = bit;

            for (var registerNumber = 0; registerNumber < 8; registerNumber++)
            {
                if (registerNumber == 6)
                {
                }
                else
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

                    _instructions.Add(0xDDCB00 + registerNumber + bit * 8, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d, register), $"RLC (IX + d), {register}", 0xDDCB40 + registerNumber + bit * 8, 0));
                }

                _instructions.Add(0xDDCB40 + registerNumber + bit * 8, new Instruction(d => BIT_b_IX_d((byte) (1 << innerBit), RegisterPair.IX, d), $"BIT {bit}, (IX + d)", 0xDDCB40 + registerNumber + bit * 8, 0));
            }
        }
    }
}