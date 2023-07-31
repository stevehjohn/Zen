namespace Zen.System.Modules.Audio;

// ReSharper disable  IdentifierTypo
public static class Lfsr
{
    private static uint _seed = 1;

    public static bool GetNextValue()
    {
        if ((_seed & 1) > 0)
        {
            _seed ^= 0x24000;
        }

        _seed >>= 1;

        return (_seed & 0x01) > 0;
    }
}