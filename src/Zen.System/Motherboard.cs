using Zen.System.Exceptions;
using Zen.System.Infrastructure;
using Zen.System.Interfaces;
using Zen.System.Modules;
using Zen.System.Modules.Audio.Engines;
using Zen.System.ProcessorHooks;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;
using Worker = Zen.System.Modules.Worker;

namespace Zen.System;

public class Motherboard : IPortConnector, IRamConnector, IDisposable
{
    private readonly Model _model;

    private readonly Core _core;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly Interface _interface;

    private readonly State _state;

    private readonly Ram _ram;

    private readonly List<IPeripheral> _peripherals = new();

    private readonly VideoModulator _videoModulator;

    private readonly Worker _worker;

    private readonly Dictionary<int, byte[]> _romCache = new();

    private readonly LdBytesHook _ldBytesHook;

    private readonly AyAudio _ayAudio;

    private bool _pagingDisabled;

    private bool _sound;

    private int _currentFrameCycle;

    public int SelectedRom
    {
        get
        {
            if (_model is Model.SpectrumPlus2A or Model.SpectrumPlus3)
            {
                if ((Last1FFD & 0x01) != 0)
                {
                    return -1;
                }

                return ((Last7FFD >> 4) & 0x01) | ((Last1FFD >> 1) & 0x02);
            }

            return (Last7FFD >> 4) & 0x01;
        }
    }

    // ReSharper disable once InconsistentNaming
    public byte Last7FFD { get; set; }

    // ReSharper disable once InconsistentNaming
    public byte Last1FFD { get; set; }

    public Ram Ram => _ram;

    public Model Model => _model;

    public State State => _state;

    public int FrameCycles => _worker.FrameCycles;

    public VideoModulator VideoModulator => _videoModulator;

    public AyAudio AyAudio => _ayAudio;

    public bool Fast
    {
        set => _worker.Fast = value;
    }

    public bool Slow
    {
        set => _worker.Slow = value;
    }

    public Worker Worker => _worker;

    public bool Sound
    {
        get => _sound;
        set
        {
            _ayAudio.Silent = ! value;

            _sound = value;
        }
    }

    public IZenAudioEngine AudioEngine
    {
        set => _ayAudio.AudioEngine = value;
        get => _ayAudio.AudioEngine;
    }

    public Motherboard(Model model, IZenAudioEngine engine)
    {
        _model = model;

        _interface = new(this, this);

        _state = new();

        _core = new Core(_interface, _state);

        _ldBytesHook = new LdBytesHook();

        _core.AddHook(_ldBytesHook);

        _ram = new() { ProtectRom = model == Model.Spectrum48K };

        if (model == Model.Spectrum48K)
        {
            _pagingDisabled = true;
        }

        _ram.LoadRom(LoadRom(0));

        _videoModulator = new VideoModulator(model, _ram);

        _ayAudio = new AyAudio(model, engine);

        _ayAudio.Start();
        
        _worker?.Dispose();

        _worker = new(model, _interface, _videoModulator, _ayAudio)
        {
            OnTick = OnTick
        };
    }

    public void AddPeripheral(IPeripheral peripheral)
    {
        _peripherals.Add(peripheral);
    }

    public void StageFile(string filename)
    {
        _ldBytesHook.StageFile(filename);
    }

    public byte CpuPortRead(ushort port)
    {
        if ((port & 0xC002) == 0xC000)
        {
            return _ayAudio.GetRegister();
        }

        if ((port & 0x8002) == 0x8000)
        {
            return _ayAudio.GetRegister();
        }

        foreach (var peripheral in _peripherals)
        {
            var result = peripheral.GetPortState(port);

            if (result != null)
            {
                return (byte) result;
            }
        }

        return _videoModulator.FloatingBusValue;
    }

    public byte ReadRam(ushort address)
    {
        return _ram[address];
    }

    public void WriteRam(ushort address, byte data)
    {
        if (address >= 0x4000 && address < 0x5B00)
        {
            _worker.VRamUpdated(address, data);
        }

        _ram[address] = data;
    }

    public void CpuPortWrite(ushort port, byte data)
    {
        PortDataChanged(port, data);
    }

    public void Start()
    {
        _worker.Start();
    }

    public void Pause()
    {
        _worker.Pause();
    }

    public void Resume()
    {
        _worker.Resume();
    }

    public void Reset()
    {
        // TODO.
    }

    public void StateLoaded()
    {
        _videoModulator.Border = _state.BorderColour;
    }

    public void ScanComplete()
    {
        _worker.ScanComplete();
    }

    private byte[] OnTick(int frameCycle)
    {
        _currentFrameCycle = frameCycle;

        _core.ExecuteCycle();

        return _state.LastMCycles;
    }

    private void PortDataChanged(ushort port, byte data)
    {
        if ((port & 0x01) == 0)
        {
            _videoModulator.Border = (byte) (data & 0b0000_0111);

            _state.BorderColour = _videoModulator.Border;

            _ayAudio.UlaAddressed(_currentFrameCycle, data);
        }

        if ((port & 0xC002) == 0xC000)
        {
            _ayAudio.SelectRegister(_currentFrameCycle, data);
        }

        if ((port & 0x8002) == 0x8000)
        {
            _ayAudio.SetRegister(_currentFrameCycle, data);
        }

        if (_pagingDisabled || _model == Model.Spectrum48K)
        {
            return;
        }

        if ((port & 0x01) != 0)
        {
            var paging = (port & 0b1000_0000_0000_0010) == 0;

            if (_model is Model.SpectrumPlus2A or Model.SpectrumPlus3)
            {
                paging &= (port & 0b0100_0000_0000_0000) > 0;
            }

            if (paging)
            {
                if ((data & 0b00100000) > 0)
                {
                    _pagingDisabled = true;
                }

                PageCall(0x7F, data);
            }

            if (_model is not (Model.SpectrumPlus2A or Model.SpectrumPlus3))
            {
                return;
            }

            paging = (port & 0b1110_0000_0000_0010) == 0;

            paging &= (port & 0b0001_0000_0000_0000) > 0;

            if (paging)
            {
                PageCall(0x1F, data);
            }
        }
    }

    private void PageCall(byte port, byte data)
    {
        switch (port)
        {
            case 0x7F:
                Last7FFD = data;
                break;

            case 0x1F:
                Last1FFD = data;
                break;
        }

        if (port == 0x7F)
        {
            _ram.SetBank(3, (byte) (Last7FFD & 0b0000_0111));

            _ram.UseShadowScreenBank = (Last7FFD & 0b0000_1000) != 0;

            if ((Last7FFD & 0b0010_0000) != 0)
            {
                _pagingDisabled = true;
            }
        }

        switch (_model)
        {
            case Model.Spectrum48K:
                return;
            
            case Model.Spectrum128:
            case Model.SpectrumPlus2:
                _ram.LoadRom(LoadRom((Last7FFD >> 4) & 0x01));
                
                break;
            
            default:
                var specialPaging = (Last1FFD & 0x01) != 0;

                if (! specialPaging)
                {
                    _ram.SetBank(1, 5);
                    _ram.SetBank(2, 2);
                    
                    var romNumber = ((Last7FFD >> 4) & 0x01) | ((Last1FFD >> 1) & 0x02);

                    _ram.LoadRom(LoadRom(romNumber));
                    
                    return;
                }

                var map = (Last1FFD >> 1) & 0b11;

                ConfigureSpecialPaging(map);

                break;
        }
    }

    private void ConfigureSpecialPaging(int configurationId)
    {
        switch (configurationId)
        {
            case 0:
                _ram.SetBank(3, 3);
                _ram.SetBank(2, 2);
                _ram.SetBank(1, 1);
                _ram.SetBank(0, 0);

                break;

            case 1:
                _ram.SetBank(3, 7);
                _ram.SetBank(2, 6);
                _ram.SetBank(1, 5);
                _ram.SetBank(0, 4);

                break;

            case 2:
                _ram.SetBank(3, 3);
                _ram.SetBank(2, 6);
                _ram.SetBank(1, 5);
                _ram.SetBank(0, 4);

                break;

            case 3:
                _ram.SetBank(3, 3);
                _ram.SetBank(2, 6);
                _ram.SetBank(1, 7);
                _ram.SetBank(0, 4);

                break;
        }
    }

    private byte[] LoadRom(int romNumber)
    {
        var folder = Model switch
        {
            Model.Spectrum48K => "ZX Spectrum 48K",
            Model.Spectrum128 => "ZX Spectrum 128",
            Model.SpectrumPlus2 => "ZX Spectrum +2",
            Model.SpectrumPlus2A => "ZX Spectrum +3",
            Model.SpectrumPlus3 => "ZX Spectrum +3",
            _ => throw new InvalidModelException()
        };

        if (! _romCache.ContainsKey(romNumber))
        {
            _romCache.Add(romNumber, File.ReadAllBytes($"{AppDomain.CurrentDomain.BaseDirectory}/Rom Images/{folder}/image-{romNumber}.rom"));
        }

        return _romCache[romNumber];
    }

    public void Dispose()
    {
        _worker.Dispose();

        _ayAudio.Dispose();
    }
}