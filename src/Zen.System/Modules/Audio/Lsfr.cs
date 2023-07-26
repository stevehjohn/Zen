namespace Zen.System.Modules.Audio;

// ReSharper disable  IdentifierTypo
public static class Lsfr
{
    private static uint _seed = 1;

    public static byte Value { get; private set; }

    public static void GenerateNextValue()
    {
        if ((_seed & 1) > 0)
        {
            _seed ^= 0x24000;
        }

        _seed >>= 1;

        Value = (byte) (_seed & 0x01);
    }
}