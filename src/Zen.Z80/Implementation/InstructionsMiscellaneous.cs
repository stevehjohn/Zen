// ReSharper disable InconsistentNaming

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void NOP()
    {
        _state.SetMCycles(4);
    }
}