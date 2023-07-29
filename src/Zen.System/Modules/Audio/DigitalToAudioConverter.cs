namespace Zen.System.Modules.Audio;

public class DigitalToAudioConverter
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

    private byte _channelAVolume;

    private byte _channelBVolume;

    private byte _channelCVolume;

    public float GetChannelASignal(bool channelValue)
    {
        return NormaliseVolume(_channelAVolume) * (channelValue ? Constants.ChannelAmplitude : 0);
    }

    public float GetChannelBSignal(bool channelValue)
    {
        return NormaliseVolume(_channelBVolume) * (channelValue ? Constants.ChannelAmplitude : 0);
    }

    public float GetChannelCSignal(bool channelValue)
    {
        return NormaliseVolume(_channelCVolume) * (channelValue ? Constants.ChannelAmplitude : 0);
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