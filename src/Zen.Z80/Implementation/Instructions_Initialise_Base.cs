using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseBaseInstructions()
    {
        _instructions.Add(0x00, new Instruction(_ => NOP(), "NOP", 0x00, 0));

        _instructions.Add(0x01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));

        _instructions.Add(0x11, new Instruction(d => LD_RR_nn(RegisterPair.DE, d), "LD DE, nn", 0x11, 2));

        _instructions.Add(0x21, new Instruction(d => LD_RR_nn(RegisterPair.HL, d), "LD HL, nn", 0x21, 2));

        _instructions.Add(0xCB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0xCB, 0));

        _instructions.Add(0xDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0xDD, 0));

        _instructions.Add(0xFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0xFD, 0));
    }
}