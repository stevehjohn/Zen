using Un4seen.Bass;
using Zen.Common.Infrastructure;

namespace Zen.System.Modules.Audio;

public class AudioEngine : IDisposable
{
    private readonly int _sampleHandle;

    private readonly AutoResetEvent _resetEvent;
    
    public AudioEngine()
    {
        _resetEvent = new AutoResetEvent(true);
        
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

        Bass.BASS_ChannelSetSync(channel, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_MIXTIME, 0, PlayComplete, IntPtr.Zero);
        
        Bass.BASS_ChannelPlay(channel, false);

        _resetEvent.WaitOne();
    }

    private void PlayComplete(int handle, int channel, int data, IntPtr user)
    {
        _resetEvent.Set();
    }

    public void Dispose()
    {
        Bass.BASS_Free();
    }
}