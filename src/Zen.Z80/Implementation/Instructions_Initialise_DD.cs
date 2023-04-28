// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseDDInstructions()
    {
        _instructions.Add(0xDD00, new Instruction(_ => NOP(), "NOP", 0xDD00, 0, 4));
        
        InitialiseLDByteInstructions(0xDD00);
        
        InitialiseArithmeticLogicInstructions(0xDD00);

        _instructions.Add(0xDDCB, new Instruction(_ => PREFIX(0xDDCB), "PREFIX 0xDDCB", 0xDDCB, 2));
    }
}