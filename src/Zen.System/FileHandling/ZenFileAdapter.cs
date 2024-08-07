using System.Text.Json;
using Zen.System.Exceptions;
using Zen.System.FileHandling.Models;
using Zen.System.Infrastructure;
using Zen.System.Modules;
using Zen.Z80.Processor;

namespace Zen.System.FileHandling;

public class ZenFileAdapter
{
    private readonly Motherboard _motherboard;

    private readonly State _state;

    private readonly Ram _ram;

    public ZenFileAdapter(Motherboard motherboard)
    {
        _motherboard = motherboard;

        _state = _motherboard.State;

        _ram = _motherboard.Ram;
    }

    public string Load(string filename)
    {
        var json = File.ReadAllText(filename);

        var model = JsonSerializer.Deserialize<ZenFile>(json);

        if (model == null || model.State == null)
        {
            throw new InvalidZenFileException();
        }

        _state.Halted = model.State.Halted;

        _state.InterruptFlipFlop1 = model.State.InterruptFlipFlop1;

        _state.InterruptFlipFlop2 = model.State.InterruptFlipFlop2;

        _state.InterruptMode = model.State.InterruptMode;

        _state.MemPtr = model.State.MemPtr;

        _state.InstructionPrefix = model.State.InstructionPrefix;

        _state.ProgramCounter = model.State.ProgramCounter;

        _state.Q = model.State.Q;

        _state.StackPointer = model.State.StackPointer;

        _state[RegisterPair.AF] = model.Registers["AF"];
        _state[RegisterPair.BC] = model.Registers["BC"];
        _state[RegisterPair.DE] = model.Registers["DE"];
        _state[RegisterPair.HL] = model.Registers["HL"];

        _state[RegisterPair.AF_] = model.Registers["AF'"];
        _state[RegisterPair.BC_] = model.Registers["BC'"];
        _state[RegisterPair.DE_] = model.Registers["DE'"];
        _state[RegisterPair.HL_] = model.Registers["HL'"];

        _state[RegisterPair.IX] = model.Registers["IX"];
        _state[RegisterPair.IY] = model.Registers["IY"];

        _state[Register.I] = (byte) ((model.Registers["IR"] & 0xFF00) >> 8);
        _state[Register.R] = (byte) (model.Registers["IR"] & 0x00FF);

        for (var i = 0; i < 8; i++)
        {
            _ram.LoadIntoBank((byte) i, model.RamBanks[i]);
        }

        for (var i = 0; i < 4; i++)
        {
            _ram.SetBank((byte) i, (byte) model.PageConfiguration[i]);
        }

        _ram.LoadRom(model.Rom!);

        _motherboard.Last7FFD = model.Last7Ffd;

        _motherboard.Last1FFD = model.Last1Ffd;

        return model.RomTitle!;
    }

    public void Save(string filename, string romTitle, Model model)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filename)!);

        // ReSharper disable once UseObjectOrCollectionInitializer
        var data = new ZenFile
                   {
                       State = _state,
                       RomTitle = romTitle
                   };

        data.Model = model;

        data.Registers.Add("AF", _state[RegisterPair.AF]);
        data.Registers.Add("BC", _state[RegisterPair.BC]);
        data.Registers.Add("DE", _state[RegisterPair.DE]);
        data.Registers.Add("HL", _state[RegisterPair.HL]);

        data.Registers.Add("AF'", _state[RegisterPair.AF_]);
        data.Registers.Add("BC'", _state[RegisterPair.BC_]);
        data.Registers.Add("DE'", _state[RegisterPair.DE_]);
        data.Registers.Add("HL'", _state[RegisterPair.HL_]);

        data.Registers.Add("IX", _state[RegisterPair.IX]);
        data.Registers.Add("IY", _state[RegisterPair.IY]);

        data.Registers.Add("IR", (ushort) ((_state[Register.I] << 8) | _state[Register.R]));

        for (var i = 0; i < 8; i++)
        {
            data.RamBanks.Add(i, _ram.GetBank((byte) i));
        }

        for (var i = 0; i < 4; i++)
        {
            data.PageConfiguration.Add(i, _ram.GetBankMapping((byte) i));
        }

        data.Rom = _ram.Rom;

        data.Last7Ffd = _motherboard.Last7FFD;

        data.Last1Ffd = _motherboard.Last1FFD;

        var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                                                  {
                                                      WriteIndented = true
                                                  });

        File.WriteAllText(filename, json);
    }
}