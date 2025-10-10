namespace Zen.System.Modules.Audio.Engines;

public interface IZenAudioEngine : IDisposable
{
    void Send(float[] data);
    
    void Reset();
}