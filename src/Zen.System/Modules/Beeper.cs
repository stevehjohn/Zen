using Bufdio;
using Bufdio.Engines;
using Zen.Common;

namespace Zen.System.Modules;

public class Beeper : IDisposable
{
    private bool _bit3State;

    private bool _bit4State;

    private ulong? _lastCycle;

    private readonly IAudioEngine _engine;

    private readonly Queue<(float Frequency, int Amplitude, int Duration)> _queue = new();

    private readonly float[] _buffer;

    private Task? _beeperThread;

    public Beeper()
    {
        if (Environment.OSVersion.Platform == PlatformID.Unix)
        {
            BufdioLib.InitializePortAudio(Path.Combine("Libraries", "libportaudio.dylib"));
        }
        else
        {
            BufdioLib.InitializePortAudio(Path.Combine("Libraries", "libportaudio"));
        }

        _engine = new PortAudioEngine(new AudioEngineOptions(1, Audio.Constants.SampleRate));

        _buffer = new float[Audio.Constants.SampleRate / Constants.FramesPerSecond];
    }

    public void Start()
    {
        _beeperThread = Task.Run(PlayFrame);
    }

    public void UlaAddressed(byte value, ulong cycle)
    {
        var amplitude = 0;

        var bit3State = (value & 0b0000_1000) > 0;

        if (_bit3State != bit3State)
        {
            amplitude = 1;

            _bit3State = bit3State;
        }

        var bit4State = (value & 0b0000_1000) > 0;

        if (_bit4State != bit4State)
        {
            amplitude = 2;

            _bit4State = bit4State;
        }

        if (_lastCycle != null)
        {
            var duration = cycle - _lastCycle;
            
            var frequency = Constants.TStatesPerSecond / (float) (cycle - _lastCycle) / 2;

            _queue.Enqueue((frequency, amplitude, (int) duration));
        }

        _lastCycle = cycle;
    }

    private void PlayFrame()
    {
        while (true)
        {
            _engine.Send(_buffer);
        }
    }

    public void Dispose()
    {
        _engine.Dispose();
    }
}