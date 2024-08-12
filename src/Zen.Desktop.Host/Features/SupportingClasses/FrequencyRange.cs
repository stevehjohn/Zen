namespace Zen.Desktop.Host.Features.SupportingClasses;

public class FrequencyRange
{
    public float Low { get; }
    public float High { get; }

    public FrequencyRange(float low, float high)
    {
        Low = low;
        High = high;
    }
}