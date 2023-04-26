using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    // ReSharper disable once InconsistentNaming
    private void InitialiseFDInstructions()
    {
        _instructions.Add(0xFD01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));

        _instructions.Add(0xFD11, new Instruction(d => LD_RR_nn(RegisterPair.DE, d), "LD DE, nn", 0x01, 2));

        _instructions.Add(0xFD21, new Instruction(d => LD_RR_nn(RegisterPair.IY, d), "LD IY, nn", 0x01, 2));
    }
}