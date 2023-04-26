using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseBaseInstructions()
    {
        _instructions.Add(0x01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));

        _instructions.Add(0x11, new Instruction(d => LD_RR_nn(RegisterPair.DE, d), "LD DE, nn", 0x01, 2));

        _instructions.Add(0x21, new Instruction(d => LD_RR_nn(RegisterPair.HL, d), "LD HL, nn", 0x01, 2));
    }
}