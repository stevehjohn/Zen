namespace Zen.Desktop.Host.Features;

public partial class SpectrumAnalyser
{
    private class FrequencyRange
    {
        public float Low { get; }
        public float High { get; }

        public FrequencyRange(float low, float high)
        {
            Low = low;
            High = high;
        }
    }
}