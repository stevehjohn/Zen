// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global
namespace Zen.System.Modules.Audio.Engines.Bass;

[Flags]
public enum BassFlag
{
    BASS_DEFAULT = 0,
    BASS_SAMPLE_8BITS = 1,
    BASS_SAMPLE_MONO = 2,
    BASS_SAMCHAN_NEW = BASS_SAMPLE_8BITS, // 0x00000001
    BASS_SAMCHAN_STREAM = BASS_SAMPLE_MONO, // 0x00000002
    BASS_SAMPLE_LOOP = 4,
    BASS_SAMPLE_3D = 8,
    BASS_SAMPLE_SOFTWARE = 16, // 0x00000010
    BASS_SAMPLE_MUTEMAX = 32, // 0x00000020
    BASS_SAMPLE_VAM = 64, // 0x00000040
    BASS_SAMPLE_FX = 128, // 0x00000080
    BASS_SAMPLE_FLOAT = 256, // 0x00000100
    BASS_RECORD_PAUSE = 32768, // 0x00008000
    BASS_RECORD_ECHOCANCEL = 8192, // 0x00002000
    BASS_RECORD_AGC = 16384, // 0x00004000
    BASS_STREAM_PRESCAN = 131072, // 0x00020000
    BASS_STREAM_AUTOFREE = 262144, // 0x00040000
    BASS_STREAM_RESTRATE = 524288, // 0x00080000
    BASS_STREAM_BLOCK = 1048576, // 0x00100000
    BASS_STREAM_DECODE = 2097152, // 0x00200000
    BASS_STREAM_STATUS = 8388608, // 0x00800000
    BASS_SPEAKER_FRONT = 16777216, // 0x01000000
    BASS_SPEAKER_REAR = 33554432, // 0x02000000
    BASS_SPEAKER_CENLFE = BASS_SPEAKER_REAR | BASS_SPEAKER_FRONT, // 0x03000000
    BASS_SPEAKER_REAR2 = 67108864, // 0x04000000
    BASS_SPEAKER_SIDE = BASS_SPEAKER_REAR2, // 0x04000000
    BASS_SPEAKER_LEFT = 268435456, // 0x10000000
    BASS_SPEAKER_RIGHT = 536870912, // 0x20000000
    BASS_SPEAKER_FRONTLEFT = BASS_SPEAKER_LEFT | BASS_SPEAKER_FRONT, // 0x11000000
    BASS_SPEAKER_FRONTRIGHT = BASS_SPEAKER_RIGHT | BASS_SPEAKER_FRONT, // 0x21000000
    BASS_SPEAKER_REARLEFT = BASS_SPEAKER_LEFT | BASS_SPEAKER_REAR, // 0x12000000
    BASS_SPEAKER_REARRIGHT = BASS_SPEAKER_RIGHT | BASS_SPEAKER_REAR, // 0x22000000
    BASS_SPEAKER_CENTER = BASS_SPEAKER_REARLEFT | BASS_SPEAKER_FRONT, // 0x13000000
    BASS_SPEAKER_LFE = BASS_SPEAKER_REARRIGHT | BASS_SPEAKER_FRONT, // 0x23000000
    BASS_SPEAKER_SIDELEFT = BASS_SPEAKER_LEFT | BASS_SPEAKER_SIDE, // 0x14000000
    BASS_SPEAKER_SIDERIGHT = BASS_SPEAKER_RIGHT | BASS_SPEAKER_SIDE, // 0x24000000
    BASS_SPEAKER_REAR2LEFT = BASS_SPEAKER_SIDELEFT, // 0x14000000
    BASS_SPEAKER_REAR2RIGHT = BASS_SPEAKER_SIDERIGHT, // 0x24000000
    BASS_SPEAKER_PAIR1 = BASS_SPEAKER_FRONT, // 0x01000000
    BASS_SPEAKER_PAIR2 = BASS_SPEAKER_REAR, // 0x02000000
    BASS_SPEAKER_PAIR3 = BASS_SPEAKER_PAIR2 | BASS_SPEAKER_PAIR1, // 0x03000000
    BASS_SPEAKER_PAIR4 = BASS_SPEAKER_SIDE, // 0x04000000
    BASS_SPEAKER_PAIR5 = BASS_SPEAKER_PAIR4 | BASS_SPEAKER_PAIR1, // 0x05000000
    BASS_SPEAKER_PAIR6 = BASS_SPEAKER_PAIR4 | BASS_SPEAKER_PAIR2, // 0x06000000
    BASS_SPEAKER_PAIR7 = BASS_SPEAKER_PAIR6 | BASS_SPEAKER_PAIR1, // 0x07000000
    BASS_SPEAKER_PAIR8 = 134217728, // 0x08000000
    BASS_SPEAKER_PAIR9 = BASS_SPEAKER_PAIR8 | BASS_SPEAKER_PAIR1, // 0x09000000
    BASS_SPEAKER_PAIR10 = BASS_SPEAKER_PAIR8 | BASS_SPEAKER_PAIR2, // 0x0A000000
    BASS_SPEAKER_PAIR11 = BASS_SPEAKER_PAIR10 | BASS_SPEAKER_PAIR1, // 0x0B000000
    BASS_SPEAKER_PAIR12 = BASS_SPEAKER_PAIR8 | BASS_SPEAKER_PAIR4, // 0x0C000000
    BASS_SPEAKER_PAIR13 = BASS_SPEAKER_PAIR12 | BASS_SPEAKER_PAIR1, // 0x0D000000
    BASS_SPEAKER_PAIR14 = BASS_SPEAKER_PAIR12 | BASS_SPEAKER_PAIR2, // 0x0E000000
    BASS_SPEAKER_PAIR15 = BASS_SPEAKER_PAIR14 | BASS_SPEAKER_PAIR1, // 0x0F000000
    BASS_ASYNCFILE = 1073741824, // 0x40000000
    BASS_UNICODE = -2147483648, // 0x80000000
    BASS_SAMPLE_OVER_VOL = 65536, // 0x00010000
    BASS_SAMPLE_OVER_POS = BASS_STREAM_PRESCAN, // 0x00020000
    BASS_SAMPLE_OVER_DIST = BASS_SAMPLE_OVER_POS | BASS_SAMPLE_OVER_VOL, // 0x00030000
    BASS_WV_STEREO = 4194304, // 0x00400000
    BASS_AC3_DOWNMIX_2 = 512, // 0x00000200
    BASS_AC3_DOWNMIX_4 = 1024, // 0x00000400
    BASS_DSD_RAW = BASS_AC3_DOWNMIX_2, // 0x00000200
    BASS_DSD_DOP = BASS_AC3_DOWNMIX_4, // 0x00000400
    BASS_DSD_DOP_AA = 2048, // 0x00000800
    BASS_AC3_DOWNMIX_DOLBY = BASS_DSD_DOP | BASS_DSD_RAW, // 0x00000600
    BASS_AC3_DYNAMIC_RANGE = BASS_DSD_DOP_AA, // 0x00000800
    BASS_AAC_FRAME960 = 4096, // 0x00001000
    BASS_AAC_STEREO = BASS_WV_STEREO, // 0x00400000
    BASS_MIXER_END = BASS_SAMPLE_OVER_VOL, // 0x00010000
    BASS_MIXER_CHAN_PAUSE = BASS_SAMPLE_OVER_POS, // 0x00020000
    BASS_MIXER_NONSTOP = BASS_MIXER_CHAN_PAUSE, // 0x00020000
    BASS_MIXER_RESUME = BASS_AAC_FRAME960, // 0x00001000
    BASS_MIXER_CHAN_ABSOLUTE = BASS_MIXER_RESUME, // 0x00001000
    BASS_MIXER_POSEX = BASS_RECORD_ECHOCANCEL, // 0x00002000
    BASS_MIXER_NOSPEAKER = BASS_RECORD_AGC, // 0x00004000
    BASS_MIXER_CHAN_LIMIT = BASS_MIXER_NOSPEAKER, // 0x00004000
    BASS_MIXER_LIMIT = BASS_MIXER_CHAN_LIMIT, // 0x00004000
    BASS_MIXER_QUEUE = BASS_RECORD_PAUSE, // 0x00008000
    BASS_MIXER_CHAN_MATRIX = BASS_MIXER_END, // 0x00010000
    BASS_MIXER_MATRIX = BASS_MIXER_CHAN_MATRIX, // 0x00010000
    BASS_MIXER_CHAN_DOWNMIX = BASS_AAC_STEREO, // 0x00400000
    BASS_MIXER_CHAN_NORAMPIN = BASS_STREAM_STATUS, // 0x00800000
    BASS_MIXER_NORAMPIN = BASS_MIXER_CHAN_NORAMPIN, // 0x00800000
    BASS_SPLIT_SLAVE = BASS_MIXER_CHAN_ABSOLUTE, // 0x00001000
    BASS_MIXER_CHAN_BUFFER = BASS_MIXER_POSEX, // 0x00002000
    BASS_MIXER_BUFFER = BASS_MIXER_CHAN_BUFFER, // 0x00002000
    BASS_SPLIT_POS = BASS_MIXER_BUFFER, // 0x00002000
    BASS_CD_SUBCHANNEL = BASS_DSD_RAW, // 0x00000200
    BASS_CD_SUBCHANNEL_NOHW = BASS_DSD_DOP, // 0x00000400
    BASS_CD_C2ERRORS = BASS_AC3_DYNAMIC_RANGE, // 0x00000800
    BASS_MIDI_NODRUMPARAM = BASS_CD_SUBCHANNEL_NOHW, // 0x00000400
    BASS_MIDI_NOSYSRESET = BASS_CD_C2ERRORS, // 0x00000800
    BASS_MIDI_DECAYEND = BASS_SPLIT_SLAVE, // 0x00001000
    BASS_MIDI_NOFX = BASS_SPLIT_POS, // 0x00002000
    BASS_MIDI_DECAYSEEK = BASS_MIXER_LIMIT, // 0x00004000
    BASS_MIDI_NOCROP = BASS_MIXER_QUEUE, // 0x00008000
    BASS_MIDI_NOTEOFF1 = BASS_MIXER_MATRIX, // 0x00010000
    BASS_MIDI_ASYNC = BASS_MIXER_CHAN_DOWNMIX, // 0x00400000
    BASS_MIDI_SINCINTER = BASS_MIXER_NORAMPIN, // 0x00800000
    BASS_MIDI_FONT_MEM = BASS_MIDI_NOTEOFF1, // 0x00010000
    BASS_MIDI_FONT_MMAP = BASS_MIXER_NONSTOP, // 0x00020000
    BASS_MIDI_FONT_XGDRUMS = BASS_STREAM_AUTOFREE, // 0x00040000
    BASS_MIDI_FONT_NOFX = BASS_STREAM_RESTRATE, // 0x00080000
    BASS_MIDI_FONT_LINATTMOD = BASS_STREAM_BLOCK, // 0x00100000
    BASS_MIDI_FONT_LINDECVOL = BASS_STREAM_DECODE, // 0x00200000
    BASS_MIDI_FONT_NORAMPIN = BASS_MIDI_ASYNC, // 0x00400000
    BASS_MIDI_FONT_NOLIMITS = BASS_MIDI_SINCINTER, // 0x00800000

    /// <summary>
    /// BASSMIDI add-on: Treat the reverb/chorus levels in the soundfont as minimums. The higher of them and the MIDI levels (CC91/93) will be used instead of the sum of both.
    /// </summary>
    BASS_MIDI_FONT_MINFX = BASS_SPEAKER_PAIR1, // 0x01000000

    /// <summary>
    /// BASSMIDI add-on: Don't send a WAVE header to the encoder. If this flag is used then the sample format (mono 16-bit) must be passed to the encoder some other way, eg. via the command-line.
    /// </summary>
    BASS_MIDI_PACK_NOHEAD = BASS_SAMCHAN_NEW, // 0x00000001

    /// <summary>
    /// BASSMIDI add-on: Reduce 24-bit sample data to 16-bit before encoding.
    /// </summary>
    BASS_MIDI_PACK_16BIT = BASS_SAMCHAN_STREAM, // 0x00000002

    /// <summary>
    /// BASSMIDI add-on: Set encoding rate to 48000 Hz (else 44100 Hz).
    /// </summary>
    BASS_MIDI_PACK_48KHZ = BASS_SAMPLE_LOOP, // 0x00000004

    /// <summary>BASS_FX add-on: Free the source handle as well?</summary>
    BASS_FX_FREESOURCE = BASS_MIDI_FONT_MEM, // 0x00010000

    /// <summary>
    /// BASS_FX add-on: If in use, then you can do other stuff while detection's in process.
    /// </summary>
    BASS_FX_BPM_BKGRND = BASS_MIDI_PACK_NOHEAD, // 0x00000001

    /// <summary>
    /// BASS_FX add-on: If in use, then will auto multiply bpm by 2 (if BPM &lt; MinBPM*2)
    /// </summary>
    BASS_FX_BPM_MULT2 = BASS_MIDI_PACK_16BIT, // 0x00000002

    /// <summary>
    /// BASS_FX add-on (AddOn.Fx.BassFx.BASS_FX_TempoCreate): Uses a linear interpolation mode (simple).
    /// </summary>
    BASS_FX_TEMPO_ALGO_LINEAR = BASS_CD_SUBCHANNEL, // 0x00000200

    /// <summary>
    /// BASS_FX add-on (AddOn.Fx.BassFx.BASS_FX_TempoCreate): Uses a cubic interpolation mode (recommended, default).
    /// </summary>
    BASS_FX_TEMPO_ALGO_CUBIC = BASS_MIDI_NODRUMPARAM, // 0x00000400

    /// <summary>
    /// BASS_FX add-on (AddOn.Fx.BassFx.BASS_FX_TempoCreate): Uses a 8-tap band-limited Shannon interpolation (complex, but not much better than cubic).
    /// </summary>
    BASS_FX_TEMPO_ALGO_SHANNON = BASS_MIDI_NOSYSRESET, // 0x00000800

    /// <summary>
    /// Music: Use 32-bit floating-point sample data (see <a href="../Overview.html#FloatingPoint">Floating-Point Channels</a> for details). WDM drivers or the BASS_STREAM_DECODE flag are required to use this flag.
    /// </summary>
    BASS_MUSIC_FLOAT = BASS_SAMPLE_FLOAT, // 0x00000100

    /// <summary>
    /// Music: Decode/play the mod music in mono, reducing the CPU usage (if it was originally stereo).
    /// This flag is automatically applied if BASS_DEVICE_MONO was specified when calling <see cref="M:Un4seen.Bass.Bass.BASS_Init(System.Int32,System.Int32,Un4seen.Bass.BASSInit,System.IntPtr,System.IntPtr)" />.
    /// </summary>
    BASS_MUSIC_MONO = BASS_FX_BPM_MULT2, // 0x00000002

    /// <summary>
    /// Music: Loop the music. This flag can be toggled at any time using <see cref="M:Un4seen.Bass.Bass.BASS_ChannelFlags(System.Int32,Un4seen.Bass.BASSFlag,Un4seen.Bass.BASSFlag)" />.
    /// </summary>
    BASS_MUSIC_LOOP = BASS_MIDI_PACK_48KHZ, // 0x00000004

    /// <summary>
    /// Music: Use 3D functionality. This is ignored if BASS_DEVICE_3D wasn't specified when calling <see cref="M:Un4seen.Bass.Bass.BASS_Init(System.Int32,System.Int32,Un4seen.Bass.BASSInit,System.IntPtr,System.IntPtr)" />.
    /// <para>3D streams must be mono (chans=1). The SPEAKER flags can not be used together with this flag.</para>
    /// </summary>
    BASS_MUSIC_3D = BASS_SAMPLE_3D, // 0x00000008

    /// <summary>
    /// Music: Enable the old implementation of DirectX 8 effects. See the <a href="../Overview.html#DX8DMOEffects">DX8 effect implementations</a> section for details.
    /// <para>Use <see cref="M:Un4seen.Bass.Bass.BASS_ChannelSetFX(System.Int32,Un4seen.Bass.BASSFXType,System.Int32)" /> to add effects to the stream. Requires DirectX 8 or above.</para>
    /// </summary>
    BASS_MUSIC_FX = BASS_SAMPLE_FX, // 0x00000080

    /// <summary>
    /// Music: Automatically free the music when it ends. This allows you to play a music and forget about it, as BASS will automatically free the music's resources when it has reached the end or when <see cref="M:Un4seen.Bass.Bass.BASS_ChannelStop(System.Int32)" /> (or <see cref="M:Un4seen.Bass.Bass.BASS_Stop" />) is called.
    /// <para>Note that some musics never actually end on their own (ie. without you stopping them).</para>
    /// </summary>
    BASS_MUSIC_AUTOFREE = BASS_MIDI_FONT_XGDRUMS, // 0x00040000

    /// <summary>
    /// Music: Decode the music into sample data, without outputting it.
    /// <para>Use <see cref="M:Un4seen.Bass.Bass.BASS_ChannelGetData(System.Int32,System.IntPtr,System.Int32)" /> to retrieve decoded sample data.
    /// BASS_SAMPLE_SOFTWARE/3D/FX/AUTOFREE are ignored when using this flag, as are the SPEAKER flags.</para>
    /// </summary>
    BASS_MUSIC_DECODE = BASS_MIDI_FONT_LINDECVOL, // 0x00200000

    /// <summary>
    /// Music: Calculate the playback length of the music, and enable seeking in bytes. This slightly increases the time taken to load the music, depending on how long it is.
    /// <para>In the case of musics that loop, the length until the loop occurs is calculated. Use <see cref="M:Un4seen.Bass.Bass.BASS_ChannelGetLength(System.Int32,Un4seen.Bass.BASSMode)" /> to retrieve the length.</para>
    /// </summary>
    BASS_MUSIC_PRESCAN = BASS_MIDI_FONT_MMAP, // 0x00020000

    /// <summary>
    /// Music: Use "normal" ramping (as used in FastTracker 2).
    /// </summary>
    BASS_MUSIC_RAMP = BASS_FX_TEMPO_ALGO_LINEAR, // 0x00000200

    /// <summary>Music: Use "sensitive" ramping.</summary>
    BASS_MUSIC_RAMPS = BASS_FX_TEMPO_ALGO_CUBIC, // 0x00000400

    /// <summary>
    /// Music: Apply XMPlay's surround sound to the music (ignored in mono).
    /// </summary>
    BASS_MUSIC_SURROUND = BASS_FX_TEMPO_ALGO_SHANNON, // 0x00000800

    /// <summary>
    /// Music: Apply XMPlay's surround sound mode 2 to the music (ignored in mono).
    /// </summary>
    BASS_MUSIC_SURROUND2 = BASS_MIDI_DECAYEND, // 0x00001000

    /// <summary>Music: Apply FastTracker 2 panning to XM files.</summary>
    BASS_MUSIC_FT2PAN = BASS_MIDI_NOFX, // 0x00002000

    /// <summary>Music: Play .MOD file as FastTracker 2 would.</summary>
    BASS_MUSIC_FT2MOD = BASS_MUSIC_FT2PAN, // 0x00002000

    /// <summary>Music: Play .MOD file as ProTracker 1 would.</summary>
    BASS_MUSIC_PT1MOD = BASS_MIDI_DECAYSEEK, // 0x00004000

    /// <summary>
    /// Music: Use non-interpolated mixing. This generally reduces the sound quality, but can be good for chip-tunes.
    /// </summary>
    BASS_MUSIC_NONINTER = BASS_FX_FREESOURCE, // 0x00010000

    /// <summary>
    ///  Music: Sinc interpolated sample mixing.
    /// This increases the sound quality, but also requires quite a bit more processing. If neither this or the BASS_MUSIC_NONINTER flag is specified, linear interpolation is used.
    /// </summary>
    BASS_MUSIC_SINCINTER = BASS_MIDI_FONT_NOLIMITS, // 0x00800000

    /// <summary>
    /// Music: Stop all notes when seeking (using <see cref="M:Un4seen.Bass.Bass.BASS_ChannelSetPosition(System.Int32,System.Int64,Un4seen.Bass.BASSMode)" />).
    /// </summary>
    BASS_MUSIC_POSRESET = BASS_MIDI_NOCROP, // 0x00008000

    /// <summary>Music: Stop all notes and reset bpm/etc when seeking.</summary>
    BASS_MUSIC_POSRESETEX = BASS_MIDI_FONT_NORAMPIN, // 0x00400000

    /// <summary>
    /// Music: Stop the music when a backward jump effect is played. This stops musics that never reach the end from going into endless loops.
    /// <para>Some MOD musics are designed to jump all over the place, so this flag would cause those to be stopped prematurely.
    /// If this flag is used together with the BASS_SAMPLE_LOOP flag, then the music would not be stopped but any BASS_SYNC_END sync would be triggered.</para>
    /// </summary>
    BASS_MUSIC_STOPBACK = BASS_MIDI_FONT_NOFX, // 0x00080000

    /// <summary>
    /// Music: Don't load the samples. This reduces the time taken to load the music, notably with MO3 files, which is useful if you just want to get the name and length of the music without playing it.
    /// </summary>
    BASS_MUSIC_NOSAMPLE = BASS_MIDI_FONT_LINATTMOD, // 0x00100000
}