namespace Zen.Z80.Processor;

public class Core
{
    private readonly Interface _interface;

    private readonly State _state;

    private readonly Dictionary<int, Instruction> _instructions = new();

    public Core()
    {
        _interface = new Interface();

        _state = new State();
    }

    public Core(Interface @interface, State state)
    {
        _interface = @interface;

        _state = state;
    }
}