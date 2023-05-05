using Zen.System.Modules;
using Zen.Utilities.Files.Interfaces;
using Zen.Z80.Processor;

namespace Zen.Utilities.Files;

public class SnaFileLoader : IFileLoader
{
    private readonly State _state;

    private readonly Ram _ram;

    public SnaFileLoader(State state, Ram ram)
    {
        _state = state;

        _ram = ram;
    }

    public void Load(string filename)
    {
        var data = File.ReadAllBytes(filename);

        LoadRam(data);

        LoadRegisters(data);
    }

    private void LoadRam(byte[] data)
    {
        var dataToLoad = data[0x1B..];

        _ram.Load(dataToLoad, 0x4000);
    }

    private void LoadRegisters(byte[] data)
    {
        _state[Register.I] = data[0];

        _state[RegisterPair.HL_] = (ushort) (data[0x02] << 8 | data[0x01]);

        _state[RegisterPair.DE_] = (ushort) (data[0x04] << 8 | data[0x03]);

        _state[RegisterPair.BC_] = (ushort) (data[0x06] << 8 | data[0x05]);

        _state[RegisterPair.AF_] = (ushort) (data[0x08] << 8 | data[0x07]);

        _state[RegisterPair.HL] = (ushort) (data[0x0A] << 8 | data[0x09]);

        _state[RegisterPair.DE] = (ushort) (data[0x0C] << 8 | data[0x0B]);

        _state[RegisterPair.BC] = (ushort) (data[0x0E] << 8 | data[0x0D]);

        _state[RegisterPair.IY] = (ushort) (data[0x10] << 8 | data[0x0F]);

        _state[RegisterPair.IX] = (ushort) (data[0x12] << 8 | data[0x11]);

        _state.InterruptFlipFlop1 = (data[0x13] & 0x04) > 0;

        _state.InterruptFlipFlop2 = (data[0x13] & 0x04) > 0;

        _state[Register.R] = data[0x14];

        _state[RegisterPair.AF] = (ushort) ((data[0x16] << 8) | data[0x15]);

        _state.StackPointer = (ushort) (data[0x18] << 8 | data[0x17]);

        _state.InterruptMode = (InterruptMode) data[0x19];

        _state.InstructionPrefix = 0;
        
        var value = (ushort) _ram[_state.StackPointer];
        _state.StackPointer++;

        value |= (ushort) (_ram[_state.StackPointer] << 8);
        _state.StackPointer++;

        _state.ProgramCounter = value;
    }
}