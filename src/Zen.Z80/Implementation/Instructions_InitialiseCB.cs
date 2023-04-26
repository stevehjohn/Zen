// ReSharper disable InconsistentNaming
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseCBInstructions()
    {
        _instructions.Add(0xCB00, new Instruction(_ => RLC_R(Register.B), "RLC B", 0xCB00, 0));
    }
}