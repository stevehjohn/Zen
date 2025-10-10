using System.Numerics;

namespace Zen.Common.Extensions;

public static class UshortExtensions
{
    public static bool IsEvenParity(this ushort value)
    {
        return (BitOperations.PopCount(value) & 1) == 0;
    }
}