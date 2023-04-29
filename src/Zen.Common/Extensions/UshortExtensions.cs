namespace Zen.Common.Extensions;

public static class UshortExtensions
{
    public static bool IsEvenParity(this ushort value)
    {
        var bit = 1;

        var count = 0;

        for (var i = 0; i < 16; i++)
        {
            count += (value & bit) > 0 ? 1 : 0;

            bit <<= 1;
        }

        return count % 2 == 0;
    }
}