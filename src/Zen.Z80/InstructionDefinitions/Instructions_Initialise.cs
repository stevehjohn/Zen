using Zen.Z80.Processor;

namespace Zen.Z80.InstructionDefinitions;

public partial class Instructions
{
    private void Initialise()
    {
        _instructions.Add(0x01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));
    }
}