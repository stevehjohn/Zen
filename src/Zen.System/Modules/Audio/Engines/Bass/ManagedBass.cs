using System.Runtime.InteropServices;

namespace Zen.System.Modules.Audio.Engines.Bass;

public static class ManagedBass
{
    [DllImport("Libraries/libbass")]
    [return: MarshalAs((UnmanagedType) 2)]
    public static extern bool BASS_Init(int device, int freq, BassInit flags, IntPtr win);

    [DllImport("Libraries/libbass")]
    public static extern int BASS_SampleCreate(int length, int freq, int chans, int max, BassFlag flags);

    [DllImport("Libraries/libbass")]
    [return: MarshalAs((UnmanagedType) 2)]
    public static extern bool BASS_SampleSetData(int handle, float[] buffer);
    
    [DllImport("Libraries/libbass", CharSet = CharSet.Unicode)]
    public static extern int BASS_SampleGetChannel(int handle, BassFlag flags);
    
    [DllImport("Libraries/libbass")]
    public static extern int BASS_ChannelSetSync(int handle, BassSync type, long param, SyncProc proc, IntPtr user);
    
    [DllImport("Libraries/libbass")]
    [return: MarshalAs((UnmanagedType) 2)]
    public static extern bool BASS_ChannelPlay(int handle, [MarshalAs((UnmanagedType) 2)] bool restart);
    
    [DllImport("Libraries/libbass")]
    [return: MarshalAs((UnmanagedType) 2)]
    public static extern bool BASS_Free();

    [DllImport("Libraries/libbass")]
    public static extern int BASS_ErrorGetCode();

    public delegate void SyncProc(int handle, int channel, int data, IntPtr user);
}