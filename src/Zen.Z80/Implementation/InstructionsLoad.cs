// ReSharper disable InconsistentNaming
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void LD_RR_nn(RegisterPair target, byte[] parameters)
    {
        _state.LoadRegisterPair(target, parameters);

        _state.Q = 0;

        _state.SetMCycles(4, 3, 3);
    }
}