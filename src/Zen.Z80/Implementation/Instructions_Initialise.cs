// ReSharper disable InconsistentNaming

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void Initialise()
    {
        Initialise0x00();

        Initialise0xCB();

        Initialise0xDD();

        Initialise0xDDCB();

        Initialise0xED();

        Initialise0xFD();

        Initialise0xFDCB();
    }
}