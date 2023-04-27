// ReSharper disable InconsistentNaming
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseFDInstructions()
    {
        _instructions.Add(0xFD00, new Instruction(_ => NOP(), "NOP", 0xFD00, 0, 4));

        _instructions.Add(0xFD01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0xFD01, 2, 4));

        _instructions.Add(0xFD11, new Instruction(d => LD_RR_nn(RegisterPair.DE, d), "LD DE, nn", 0xFD11, 2, 4));

        _instructions.Add(0xFD21, new Instruction(d => LD_RR_nn(RegisterPair.IY, d), "LD IY, nn", 0xFD21, 2, 4));

        _instructions.Add(0xFDCB, new Instruction(_ => PREFIX(0xFDCB), "PREFIX 0xFDCB", 0xFDCB, 2));
    }
}