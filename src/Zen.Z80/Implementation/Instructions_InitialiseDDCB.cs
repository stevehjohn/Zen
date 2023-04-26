// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDCBInstructions()
    {
        _instructions.Add(0xDD01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0xDD01, 2));

    }
}