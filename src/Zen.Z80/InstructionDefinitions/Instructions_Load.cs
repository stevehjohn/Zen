using Zen.Z80.Processor;

namespace Zen.Z80.InstructionDefinitions;

public partial class Instructions
{
    public int LD_RR_nn(RegisterPair registerPair, byte[] parameters)
    {
        _state.LoadRegisterPair(registerPair, parameters);

        return 10;
    }
}