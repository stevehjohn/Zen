namespace Zen.System.Modules.Audio;

public class MixerDac
{
    public byte ChannelAVolume
    {
        set => _channelAVolume = (byte) (value & 0x0F);
    }

    public byte ChannelBVolume
    {
        set => _channelBVolume = (byte) (value & 0x0F);
    }

    public byte ChannelCVolume
    {
        set => _channelCVolume = (byte) (value & 0x0F);
    }

    // ReSharper disable once ConvertToAutoProperty
    public EnvelopeGenerator EnvelopeGenerator => _envelopeGenerator;

    public bool ChannelAEnvelopeOn { get; set; }

    public bool ChannelBEnvelopeOn { get; set; }

    public bool ChannelCEnvelopeOn { get; set; }

    public bool ToneAOn { get; set; }

    public bool ToneBOn { get; set; }

    public bool ToneCOn { get; set; }

    public bool NoiseAOn { get; set; }

    public bool NoiseBOn { get; set; }

    public bool NoiseCOn { get; set; }

    private byte _channelAVolume;

    private byte _channelBVolume;

    private byte _channelCVolume;

    private readonly EnvelopeGenerator _envelopeGenerator = new();

    public void GetChannelSignals(float[] buffer, bool channelAValue, bool channelBValue, bool channelCValue, bool noiseValue)
    {
        channelAValue |= ! ToneAOn;
        channelBValue |= ! ToneBOn;
        channelCValue |= ! ToneCOn;

        channelAValue &= ! NoiseAOn | noiseValue;
        channelBValue &= ! NoiseBOn | noiseValue;
        channelCValue &= ! NoiseCOn | noiseValue;

        var envelopeVolume = EnvelopeGenerator.GetNextValue();

        buffer[0] = NormaliseVolume(ChannelAEnvelopeOn ? envelopeVolume : _channelAVolume) * (channelAValue ? Constants.ChannelAmplitude : 0);
        buffer[1] = NormaliseVolume(ChannelBEnvelopeOn ? envelopeVolume : _channelBVolume) * (channelBValue ? Constants.ChannelAmplitude : 0);
        buffer[2] = NormaliseVolume(ChannelCEnvelopeOn ? envelopeVolume : _channelCVolume) * (channelCValue ? Constants.ChannelAmplitude : 0);
    }

    private static float NormaliseVolume(byte volume)
    {
        var result = volume switch
        {
            1 => 0.0105f,
            2 => 0.0154f,
            3 => 0.0216f,
            4 => 0.0314f,
            5 => 0.0461f,
            6 => 0.0635f,
            7 => 0.1061f,
            8 => 0.1319f,
            9 => 0.2163f,
            10 => 0.2973f,
            11 => 0.3908f,
            12 => 0.5129f,
            13 => 0.6371f,
            14 => 0.8186f,
            15 => 1.0000f,
            _ => 0f
        };

        return result;
    }
}