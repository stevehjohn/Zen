using Zen.Z80.Processor;

namespace Zen.Z80.Instructions;

public partial class Instructions
{
    private readonly Interface _interface;

    private readonly State _state;

    public Instructions(Interface @interface, State state)
    {
        _interface = @interface;

        _state = state;
    }
}