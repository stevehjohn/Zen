// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void LD_RR_nn(RegisterPair registerPair, byte[] parameters)
    {
        _state.LoadRegisterPair(registerPair, parameters);

        _state.SetMCycles(4, 3, 3);
    }
}