using System.Runtime.InteropServices;
using Un4seen.Bass;
using Zen.Common.Infrastructure;

namespace Zen.System.Modules.Audio.Engines;

public class BassAudioEngine : IZenAudioEngine
{
    private static readonly AutoResetEvent ResetEvent = new(true);

    private int _sampleHandle = -1;

    private int _channel = -1;
    
    private bool _first = true;
    
    public BassAudioEngine()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) && ! File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so")))
        {
            switch (RuntimeInformation.ProcessArchitecture)
            {
                case Architecture.X64:
                    File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so.x64"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so"));

                    break;
                
                default:
                    File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so.arm64"), Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libbass.so"));
                    
                    break;
            }
        }
        
        Initialise();
    }

    public void Send(float[] data)
    {
        if (_sampleHandle == -1)
        {
            return;
        }
        
        if (! _first)
        {
            ResetEvent.WaitOne();
        }
        else
        {
            _first = false;
        }

        Bass.BASS_SampleSetData(_sampleHandle, data);
        
        _channel = Bass.BASS_SampleGetChannel(_sampleHandle, BASSFlag.BASS_SAMCHAN_STREAM);
        
        Bass.BASS_ChannelSetSync(_channel, BASSSync.BASS_SYNC_END | BASSSync.BASS_SYNC_ONETIME, Constants.DefaultBufferSize * 4, PlayComplete, IntPtr.Zero);
        
        Bass.BASS_ChannelPlay(_channel, false);
    }

    public void Reset()
    {
        Initialise();
    }

    public void Dispose()
    {
        Bass.BASS_Free();
    }

    private static void PlayComplete(int handle, int channel, int data, IntPtr user)
    {
        ResetEvent.Set();
    }

    private void Initialise()
    {
        try
        {
            Bass.BASS_Init(-1, Constants.SampleRate, BASSInit.BASS_DEVICE_MONO, IntPtr.Zero);
            
            _sampleHandle = Bass.BASS_SampleCreate(Constants.DefaultBufferSize * 4, Constants.SampleRate, 1, 1, BASSFlag.BASS_SAMPLE_FLOAT);
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(BassAudioEngine), exception);
        }
    }
}