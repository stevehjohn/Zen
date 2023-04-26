using Zen.Z80.Implementation;

namespace Zen.Z80.Processor;

public class Core
{
    private readonly Interface _interface;

    private readonly State _state;

    private readonly Instructions _instructions;

    public Core()
    {
        _interface = new Interface();

        _state = new State();

        _instructions = new Instructions(_interface, _state);
    }

    public Core(Interface @interface, State state)
    {
        _interface = @interface;

        _state = state;

        _instructions = new Instructions(_interface, _state);
    }

    public void ExecuteCycle()
    {
        _interface.Address = _state.ProgramCounter;

        var instruction = _instructions[_interface.Data];

        _state.ProgramCounter++;

        instruction.Execute(new byte[] { 0x34, 0x12 });
    }
}