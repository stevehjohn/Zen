// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDCBInstructions()
    {
        _instructions.Add(0xDDCB00, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d, Register.B), "RLC (IX + d), B", 0xDDCB40, 0));

        _instructions.Add(0xDDCB01, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d, Register.C), "RLC (IX + d), C", 0xDDCB41, 0));

        _instructions.Add(0xDDCB02, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d, Register.D), "RLC (IX + d), D", 0xDDCB42, 0));

        _instructions.Add(0xDDCB03, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d, Register.E), "RLC (IX + d), E", 0xDDCB43, 0));

        _instructions.Add(0xDDCB04, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d, Register.H), "RLC (IX + d), H", 0xDDCB44, 0));

        _instructions.Add(0xDDCB05, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d, Register.L), "RLC (IX + d), L", 0xDDCB45, 0));

        _instructions.Add(0xDDCB06, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d), "RLC (IX + d)", 0xDDCB46, 0));

        _instructions.Add(0xDDCB07, new Instruction(d => RLC_IX_d_R(RegisterPair.IX, d, Register.A), "RLC (IX + d), A", 0xDDCB47, 0));
     
        for (var bit = 0; bit < 8; bit++)
        {
            var innerBit = bit;

            for (var registerNumber = 0; registerNumber < 8; registerNumber++)
            {
                // TODO: Create exception for default case?
                var register = registerNumber switch
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

                _instructions.Add(0xDDCB40 + registerNumber + bit * 8, new Instruction(d => BIT_b_IX_d((byte) (1 << innerBit), RegisterPair.IX, d), $"BIT {bit}, (IX + d)", 0xDDCB40 + registerNumber + bit * 8, 0));
            }
        }
    }
}