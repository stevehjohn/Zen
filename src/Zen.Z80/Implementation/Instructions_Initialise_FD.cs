// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseFDInstructions()
    {
        _instructions.Add(0xFD00, new Instruction(_ => NOP(), "NOP", 0xFD00, 0, 4));
                
        InitialiseLDByteInstructions(0xFD00);
                
        InitialiseArithmeticLogicInstructions(0xFD00);

        _instructions.Add(0xFDCB, new Instruction(_ => PREFIX(0xFDCB), "PREFIX 0xFDCB", 0xFDCB, 2));
    }
}