namespace Zen.System.Modules.Audio;

public static class Constants
{
    public const int SampleRate = 48_000;

    public const int BufferSize = SampleRate / Common.Constants.SpectrumFramesPerSecond;

    public const float ChannelAmplitude = BeeperAmplitude / 3f;

    public const float BeeperAmplitude = 0.5f;

    public const int AyFrequency = 1_773_500;
}