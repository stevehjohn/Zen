using Un4seen.Bass;
using Zen.Common.Infrastructure;

namespace Zen.System.Modules.Audio;

public class AudioEngine : IDisposable
{
    private readonly int _sampleHandle;

    public AudioEngine()
    {
        try
        {
            Bass.BASS_Init(-1, Constants.SampleRate, BASSInit.BASS_DEVICE_MONO, 0);
            
            _sampleHandle = Bass.BASS_SampleCreate(Constants.DefaultBufferSize, Constants.SampleRate, 1, 1, BASSFlag.BASS_SAMPLE_FLOAT);
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(AudioEngine), exception);
        }
    }

    public void Send(float[] data)
    {
        Bass.BASS_SampleSetData(_sampleHandle, data);

        var channel = Bass.BASS_SampleGetChannel(_sampleHandle, BASSFlag.BASS_SAMCHAN_STREAM);

        Bass.BASS_ChannelPlay(channel, true);
    }

    public void Dispose()
    {
        Bass.BASS_Free();
    }
}