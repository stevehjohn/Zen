namespace Zen.System.Modules.Audio;

public class Constants
{
    public const int SampleRate = 44_100;

    public const int Channels = 3;

    public const int BufferSize = SampleRate / Common.Constants.FramesPerSecond;

    public const float ChannelAmplitude = 0.33f;

    public const int AyFrequency = 1_773_500;
}