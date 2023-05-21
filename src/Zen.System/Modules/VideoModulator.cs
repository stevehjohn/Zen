using Zen.Common;

namespace Zen.System.Modules;

public class VideoModulator
{
    private const int PaperRegionAdjusted = Constants.PaperRegionStart; // Dunno why, but - 40 fixes El Stompo.

    private const int ScreenStart = PaperRegionAdjusted - Constants.StatesPerScreenLine * Constants.BorderPixels;

    private const int ScreenEnd = ScreenStart + Constants.StatesPerScreenLine * (Constants.ScreenHeightPixels + 1);

    private const int StatesPerHBorder = Constants.BorderPixels / 2;

    private int _previousCycles;

    private readonly Ram _ram;

    // .. S           F B     P P P I I I
    // .. 7 6 5 4 3 2 1 0 7 6 5 4 3 2 1 0
    // S == Whether pixel is set.
    private readonly ushort[] _screen = new ushort[Constants.ScreenPixelCount];

    private readonly ushort[] _frame = new ushort[Constants.ScreenPixelCount];

    private readonly byte[] _vram = new byte[Constants.RamBankSize];

    public ushort[] ScreenFrame => _frame;

    public byte Border { get; set; }

    public VideoModulator(Ram ram)
    {
        _ram = ram;
    }

    public void StartFrame()
    {
        Array.Copy(_ram.WorkingScreenRam, 0, _vram, 0, 0x4000);
    }

    public void ApplyRamChange(int address, byte data)
    {
        _vram[address & 0b0011_1111_1111_1111] = data;
    }

    public void CycleComplete(int cycles)
    {
        if (cycles < ScreenStart || cycles > ScreenEnd)
        {
            _previousCycles = ScreenStart;

            return;
        }

        var start = _previousCycles - ScreenStart;

        _previousCycles = cycles;

        var end = cycles - ScreenStart;

        var y = 0;

        while (start < end)
        {
            y = start / Constants.StatesPerScreenLine;

            var xS = start % Constants.StatesPerScreenLine;

            start++;

            var pixel = y * Constants.ScreenWidthPixels + xS * 2;

            if (pixel >= Constants.ScreenPixelCount)
            {
                break;
            }

            if (y < Constants.BorderPixels || y >= Constants.PaperHeightPixels + Constants.BorderPixels
                                           || xS < StatesPerHBorder
                                           || xS >= Constants.StatesPerPaperLine + StatesPerHBorder)
            {
                _screen[pixel] = (byte) (Border << 3);

                _screen[pixel + 1] = (byte) (Border << 3);

                continue;
            }

            var ramPixel = (y - Constants.BorderPixels) * Constants.PaperWidthPixels + (xS - Constants.BorderPixels / 2) * 2;

            _screen[pixel] = GetPixel(ramPixel);

            _screen[pixel + 1] = GetPixel(ramPixel + 1);
        }

        if (y >= Constants.ScreenHeightPixels)
        {
            Array.Copy(_screen, 0, _frame, 0, Constants.ScreenPixelCount);
        }
    }

    private ushort GetPixel(int pixel)
    {
        var y = pixel / Constants.PaperWidthPixels;

        var x = pixel % Constants.PaperWidthPixels;

        var xB = x / 8;

        var xO = 1 << (7 - x % 8);

        var address = 0;

        address |= (y & 0b0000_0111) << 8;

        address |= (y & 0b1100_0000) << 5;

        address |= (y & 0b0011_1000) << 2;

        address |= xB;

        var set = (_vram[address] & xO) > 0;

        var colourAddress = 0x1800;

        var offset = xB + y / 8 * 32;

        colourAddress += offset;

        var attributes = _vram[colourAddress];

        var result = (ushort) (attributes & 0b0011_1111);

        result |= (ushort) ((attributes & 0b1100_0000) << 2);

        if (set)
        {
            result |= 0b1000_0000_0000_0000;
        }

        return result;
    }
}