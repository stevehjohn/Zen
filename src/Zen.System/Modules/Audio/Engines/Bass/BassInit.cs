// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace Zen.System.Modules.Audio.Engines.Bass;

[Flags]
public enum BassInit
{
    BASS_DEVICE_DEFAULT = 0,
    /// <summary>Use mono, else stereo.</summary>
    BASS_DEVICE_MONO = 2,
    /// <summary>Limit output to 16-bit.</summary>
    BASS_DEVICE_16BITS = 8,
    /// <summary>Reinitialize</summary>
    BASS_DEVICE_REINIT = 128, // 0x00000080
    /// <summary>Calculate device latency (BASS_INFO struct).</summary>
    BASS_DEVICE_LATENCY = 256, // 0x00000100
    /// <summary>
    /// Use the Windows control panel setting to detect the number of speakers.
    /// <para>Only use this option if BASS doesn't detect the correct number of supported speakers automatically and you want to force BASS to use the number of speakers as configured in the windows control panel.</para>
    /// </summary>
    BASS_DEVICE_CPSPEAKERS = 1024, // 0x00000400
    /// <summary>
    /// Force enabling of <a href="../Overview.html#SpeakerAssignement">speaker assignment</a> (always 8 speakers will be used regardless if the soundcard supports them).
    /// <para>Only use this option if BASS doesn't detect the correct number of supported speakers automatically and you want to force BASS to use 8 speakers.</para>
    /// </summary>
    BASS_DEVICE_SPEAKERS = 2048, // 0x00000800
    /// <summary>Ignore speaker arrangement</summary>
    BASS_DEVICE_NOSPEAKER = 4096, // 0x00001000
    /// <summary>
    /// Linux-only: Initialize the device using the ALSA "dmix" plugin, else initialize the device for exclusive access.
    /// </summary>
    BASS_DEVIDE_DMIX = 8192, // 0x00002000
    /// <summary>
    /// Set the device's output rate to freq, otherwise leave it as it is.
    /// </summary>
    BASS_DEVICE_FREQ = 16384, // 0x00004000
    /// <summary>
    /// Limits the output to stereo (only really affects Linux; it's ignored on Windows and OSX).
    /// </summary>
    BASS_DEVICE_STEREO = 32768, // 0x00008000
    /// <summary>Use hog/exclusive mode.</summary>
    BASS_DEVICE_HOG = 65536, // 0x00010000
    /// <summary>
    /// Initialize the device to use AudioTrack output instead of OpenSL ES.
    /// If OpenSL ES is not available (pre-2.3 Android), then this will be applied automatically.
    /// </summary>
    BASS_DEVICE_AUDIOTRACK = 131072, // 0x00020000
    /// <summary>use DirectSound output</summary>
    BASS_DEVICE_DSOUND = 262144, // 0x00040000
    /// <summary>Disable hardware/fastpath output</summary>
    BASS_DEVICE_SOFTWARE = 524288, // 0x00080000
}