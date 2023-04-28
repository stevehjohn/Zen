// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void Initialise()
    {
        InitialiseBaseInstructions();

        InitialiseCBInstructions();

        InitialiseDDInstructions();

        InitialiseDDCBFDCBInstructions();

        InitialiseFDInstructions();
    }
}