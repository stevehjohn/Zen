using System.Text;

namespace Zen.Common.Extensions;

public static class ByteExtensions
{
    public static bool IsEvenParity(this byte value)
    {
        var bit = 1;

        var count = 0;

        for (var i = 0; i < 8; i++)
        {
            count += (value & bit) > 0 ? 1 : 0;

            bit <<= 1;
        }

        return count % 2 == 0;
    }

    public static string ToFlags(this byte value)
    {
        var builder = new StringBuilder();

        builder.Append((value & 0b0000_0001) > 0 ? 'C' : ' ');
        builder.Append((value & 0b0000_0010) > 0 ? 'N' : ' ');
        builder.Append((value & 0b0000_0100) > 0 ? 'P' : ' ');
        builder.Append((value & 0b0000_1000) > 0 ? '3' : ' ');
        builder.Append((value & 0b0001_0000) > 0 ? 'H' : ' ');
        builder.Append((value & 0b0010_0000) > 0 ? '5' : ' ');
        builder.Append((value & 0b0100_0000) > 0 ? 'Z' : ' ');
        builder.Append((value & 0b1000_0000) > 0 ? 'S' : ' ');

        return builder.ToString();
    }
}