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

            for (var register = 0; register < 8; register++)
            {
                _instructions.Add(0xDDCB00 + register + bit * 8, new Instruction(d => RLC_IX_d_R((byte) (1 << innerBit), RegisterPair.IX, d), $"RLC (IX + d), {}", 0xDDCB40 + register + bit * 8, 0));

                _instructions.Add(0xDDCB40 + register + bit * 8, new Instruction(d => BIT_b_IX_d((byte) (1 << innerBit), RegisterPair.IX, d), $"BIT {bit}, (IX + d)", 0xDDCB40 + register + bit * 8, 0));

                //_instructions.Add(0xDDCB40 + repeat + bit * 8, new Instruction(d => BIT_b_IX_d((byte) (1 << innerBit), RegisterPair.IX, d), $"RES {bit}, (IX + d)", 0xDDCB40 + repeat + bit * 8, 0));
            }
        }
    }
}