using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private int LD_RR_nn(RegisterPair registerPair, byte[] parameters)
    {
        _state.LoadRegisterPair(registerPair, parameters);

        return 10;
    }
}