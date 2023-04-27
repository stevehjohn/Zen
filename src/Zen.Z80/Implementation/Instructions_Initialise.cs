namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void Initialise()
    {
        InitialiseBaseInstructions();

        InitialiseDDInstructions();

        InitialiseDDCBFDCBInstructions();

        //InitialiseCBGroupInstructions();

        //InitialiseFDInstructions();
    }
}