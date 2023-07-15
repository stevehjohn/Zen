namespace Zen.System.Modules.Audio;

public class Constants
{
    public const int SampleRate = 44100;

    public const int Channels = 3;

    public const int BufferSize = SampleRate / Common.Constants.FramesPerSecond;

    public const float Amplitude = 0.32f;

    public const float AyFrequency = 1773500f;
}