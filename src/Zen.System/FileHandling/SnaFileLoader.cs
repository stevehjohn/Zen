using Zen.Common.Extensions;
using Zen.System.FileHandling.Interfaces;
using Zen.System.Modules;
using Zen.Z80.Processor;

namespace Zen.System.FileHandling;

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

        var is128 = data.Length > 49179;

        LoadRegisters(data, ! is128);

        if (is128)
        {
            Load128Specifics(data);
        }
    }

    private void Load128Specifics(byte[] data)
    {
        _state.ProgramCounter = data[49179..49181].ReadLittleEndian();

        var page = data[49181];

        _ram.SetBank(3, (byte) (page & 0b0000_0111));

        if ((page & 0x16) > 0)
        {
            var rom = File.ReadAllBytes("../../../../../Rom Images/ZX Spectrum 128/image-1.rom");

            _ram.LoadRom(rom);
        }

        for (var p = 0; p < 8; p++)
        {
            if (p == 5 || p == 2 || p == page)
            {
                continue;
            }

            var start = 49183 + p * 16384;

            if (start > data.Length)
            {
                break;
            }

            _ram.LoadIntoBank((byte) p, data[start..(start + 16384)]);
        }
    }

    private void LoadRam(byte[] data)
    {
        var dataToLoad = data[0x001B..0xC01B];

        _ram.Load(dataToLoad, 0x4000);
    }

    private void LoadRegisters(byte[] data, bool ret)
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

        if (ret)
        {
            var value = (ushort) _ram[_state.StackPointer];
            _state.StackPointer++;

            value |= (ushort) (_ram[_state.StackPointer] << 8);
            _state.StackPointer++;

            _state.ProgramCounter = value;
        }
    }
}