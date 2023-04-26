// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDCBInstructions()
    {
        _instructions.Add(0xDDCB40, new Instruction(d => BIT_0_IX_d(0x01, RegisterPair.BC, d), "BIT 0, (IX + d)", 0xDDCB40, 0));
    }
}