// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
namespace Zen.System.Modules.Audio.Engines.Bass;

public enum BassSync
{
    BASS_SYNC_POS = 0,
    BASS_SYNC_MUSICINST = 1,
    BASS_SYNC_END = 2,
    BASS_SYNC_MUSICFX = BASS_SYNC_END | BASS_SYNC_MUSICINST, // 0x00000003
    BASS_SYNC_META = 4,
    BASS_SYNC_SLIDE = BASS_SYNC_META | BASS_SYNC_MUSICINST, // 0x00000005
    BASS_SYNC_STALL = BASS_SYNC_META | BASS_SYNC_END, // 0x00000006
    BASS_SYNC_DOWNLOAD = BASS_SYNC_STALL | BASS_SYNC_MUSICINST, // 0x00000007
    BASS_SYNC_FREE = 8,
    BASS_SYNC_MUSICPOS = BASS_SYNC_FREE | BASS_SYNC_END, // 0x0000000A
    BASS_SYNC_SETPOS = BASS_SYNC_MUSICPOS | BASS_SYNC_MUSICINST, // 0x0000000B
    BASS_SYNC_OGG_CHANGE = BASS_SYNC_FREE | BASS_SYNC_META, // 0x0000000C
    BASS_SYNC_DEV_FAIL = BASS_SYNC_OGG_CHANGE | BASS_SYNC_END, // 0x0000000E
    BASS_SYNC_DEV_FORMAT = BASS_SYNC_DEV_FAIL | BASS_SYNC_MUSICINST, // 0x0000000F
    BASS_SYNC_THREAD = 536870912, // 0x20000000
    BASS_SYNC_MIXTIME = 1073741824, // 0x40000000
    BASS_SYNC_ONETIME = -2147483648, // 0x80000000
    BASS_SYNC_MIXER_ENVELOPE = 66048, // 0x00010200
    BASS_SYNC_MIXER_ENVELOPE_NODE = BASS_SYNC_MIXER_ENVELOPE | BASS_SYNC_MUSICINST, // 0x00010201
    BASS_SYNC_MIXER_QUEUE = BASS_SYNC_MIXER_ENVELOPE | BASS_SYNC_END, // 0x00010202
    BASS_SYNC_WMA_CHANGE = 65792, // 0x00010100
    BASS_SYNC_WMA_META = BASS_SYNC_WMA_CHANGE | BASS_SYNC_MUSICINST, // 0x00010101
    BASS_SYNC_CD_ERROR = 1000, // 0x000003E8
    BASS_SYNC_CD_SPEED = BASS_SYNC_CD_ERROR | BASS_SYNC_END, // 0x000003EA
    BASS_WINAMP_SYNC_BITRATE = 100, // 0x00000064
    BASS_SYNC_MIDI_MARKER = 65536, // 0x00010000
    BASS_SYNC_MIDI_CUE = BASS_SYNC_MIDI_MARKER | BASS_SYNC_MUSICINST, // 0x00010001
    BASS_SYNC_MIDI_LYRIC = BASS_SYNC_MIDI_MARKER | BASS_SYNC_END, // 0x00010002
    BASS_SYNC_MIDI_TEXT = BASS_SYNC_MIDI_LYRIC | BASS_SYNC_MUSICINST, // 0x00010003
    BASS_SYNC_MIDI_EVENT = BASS_SYNC_MIDI_MARKER | BASS_SYNC_META, // 0x00010004
    BASS_SYNC_MIDI_TICK = BASS_SYNC_MIDI_EVENT | BASS_SYNC_MUSICINST, // 0x00010005
    BASS_SYNC_MIDI_TIMESIG = BASS_SYNC_MIDI_EVENT | BASS_SYNC_END, // 0x00010006
    BASS_SYNC_MIDI_KEYSIG = BASS_SYNC_MIDI_TIMESIG | BASS_SYNC_MUSICINST, // 0x00010007
    BASS_SYNC_HLS_SEGMENT = 66304, // 0x00010300
    BASS_SYNC_HLS_SDT = BASS_SYNC_HLS_SEGMENT | BASS_SYNC_MUSICINST, // 0x00010301
    BASS_SYNC_HLS_EMSG = BASS_SYNC_HLS_SEGMENT | BASS_SYNC_END, // 0x00010302
}