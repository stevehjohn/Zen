// ReSharper disable InconsistentNaming
namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void NOP()
    {
        _state.SetMCycles(4);
    }

    private void PREFIX(ushort parameters)
    {
        _state.InstructionPrefix = parameters;

        _state.SetMCycles(0);
    }
}