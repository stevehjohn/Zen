namespace Zen.System.Modules.Audio;

public class Constants
{
    public const int SampleRate = 48_000;

    public const int BufferSize = SampleRate / Common.Constants.SpectrumFramesPerSecond;

    public const float ChannelAmplitude = 0.33f;

    public const int AyFrequency = 1_773_500;
}