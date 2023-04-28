namespace Zen.Common.Extensions;

public static class ByteArrayExtensions
{
    public static ushort ReadLittleEndian(this byte[] source)
    {
        if (source.Length != 2)
        {
            throw new ArgumentException("Parameter 'source' must be byte[2].");
        }

        return (ushort) ((source[1] << 8) | source[0]);
    }
}