using System.Runtime.InteropServices;

namespace Zen.System.Modules.Audio.Engines.Bass;

public static class ManagedBass
{
    [DllImport("bass")]
    [return: MarshalAs((UnmanagedType) 2)]
    public static extern bool BASS_Init(int device, int freq, BassInit flags, IntPtr win);

    [DllImport("bass")]
    public static extern int BASS_SampleCreate(int length, int freq, int chans, int max, BassFlag flags);

    [DllImport("bass")]
    [return: MarshalAs((UnmanagedType) 2)]
    public static extern bool BASS_SampleSetData(int handle, float[] buffer);
    
    [DllImport("bass", CharSet = CharSet.Unicode)]
    public static extern int BASS_SampleGetChannel(int handle, BassFlag flags);
    
    [DllImport("bass")]
    public static extern int BASS_ChannelSetSync(int handle, BassSync type, long param, SyncProc proc, IntPtr user);
    
    [DllImport("bass")]
    [return: MarshalAs((UnmanagedType) 2)]
    public static extern bool BASS_ChannelPlay(int handle, [MarshalAs((UnmanagedType) 2)] bool restart);
    
    [DllImport("bass")]
    [return: MarshalAs((UnmanagedType) 2)]
    public static extern bool BASS_Free();

    public delegate void SyncProc(int handle, int channel, int data, IntPtr user);
}