﻿using Zen.Common;
using Zen.System.Infrastructure;
using Zen.System.Interfaces;
using Zen.System.Modules;
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

    private AyAudio? _ayAudio;

    private bool _pagingDisabled;

    // ReSharper disable once InconsistentNaming
    public byte Last7FFD { get; set; }

    // ReSharper disable once InconsistentNaming
    public byte Last1FFD { get; set; }

    public Ram Ram => _ram;

    public Model Model => _model;

    public State State => _state;

    public VideoModulator VideoAdapter => _videoModulator;

    public bool Fast
    {
        set => _worker.Fast = value;
    }

    public bool Sound
    {
        get => _ayAudio != null;
        set
        {
            if (value)
            {
                if (_ayAudio == null)
                {
                    _ayAudio = new AyAudio();
                    
                    _ayAudio.Start();
                }
            }
            else
            {
                if (_ayAudio != null)
                {
                    _ayAudio.Dispose();

                    _ayAudio = null;
                }
            }
        }
    }

    public Motherboard(Model model)
    {
        _model = model;

        _interface = new(this, this);

        _state = new();

        _core = new Core(_interface, _state);

        _ldBytesHook = new LdBytesHook();

        _core.AddHook(_ldBytesHook);

        _core.AddHook(new PoAnyHook());

        _ram = new() { ProtectRom = true };

        if (model == Model.Spectrum48K)
        {
            _pagingDisabled = true;
        }

        _ram.LoadRom(LoadRom(0));

        _videoModulator = new VideoModulator(_ram);

        _worker = new(_interface, _videoModulator, Constants.FramesPerSecond)
                  {
                      OnTick = OnTick
                  };

        if (_model != Model.Spectrum48K)
        {
            _ayAudio = new AyAudio();

            _ayAudio.Start();
        }
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
        foreach (var peripheral in _peripherals)
        {
            var result = peripheral.GetPortState(port);

            if (result != null)
            {
                return (byte) result;
            }
        }

        return 0xFF;
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

    private byte[] OnTick()
    {
        _core.ExecuteCycle();

        return _state.LastMCycles;
    }

    private void PortDataChanged(ushort port, byte data)
    {
        if ((port & 0x01) == 0)
        {
            _videoModulator.Border = (byte) (data & 0b0000_0111);
        }

        if (_ayAudio != null)
        {
            if ((port & 0xC002) == 0xC000)
            {
                _ayAudio.SelectRegister(data);
            }
            
            if ((port & 0x8002) == 0x8000)
            {
                _ayAudio.SetRegister(data);
            }
        }

        if (_pagingDisabled)
        {
            return;
        }

        if ((port & 0x01) != 0)
        {
            var paging = (port & 0b1000_0000_0000_0010) == 0;

            if (_model == Model.SpectrumPlus3)
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

            if (_model != Model.SpectrumPlus3)
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
        if (port == 0x1F && (data & 0x01) > 0)
        {
            ConfigureSpecialPaging(data & 0b0110 >> 1);
        }

        if (port == 0x7F)
        {
            _ram.SetBank(3, (byte) (data & 0b0000_0111));

            _ram.ScreenBank = (byte) ((data & 0b0000_1000) > 0 ? 2 : 1);
        }

        if (port == 0x7F)
        {
            Last7FFD = data;
        }

        if (port == 0x1F)
        {
            Last1FFD = data;
        }

        var romNumber = (Last7FFD & 0b0001_0000) >> 4 | (Last1FFD & 0b0000_0100) >> 1;

        _ram.LoadRom(LoadRom(romNumber));
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
            Model.SpectrumPlus3 => "ZX Spectrum +3",
            // TODO: Proper exception?
            _ => throw new Exception("Invalid model")
        };

        if (! _romCache.ContainsKey(romNumber))
        {
            _romCache.Add(romNumber, File.ReadAllBytes($"../../../../../ROM Images/{folder}/image-{romNumber}.rom"));
        }

        return _romCache[romNumber];
    }

    public void Dispose()
    {
        _worker.Dispose();

        _ayAudio?.Dispose();
    }
}