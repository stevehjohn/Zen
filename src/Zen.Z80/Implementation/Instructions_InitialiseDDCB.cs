// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDCBInstructions()
    {
        _instructions.Add(0xDDCB00, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "RLC (IX + d), B", 0xDDCB00, 1));
    }
}