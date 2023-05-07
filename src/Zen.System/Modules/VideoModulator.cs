using Zen.Common;

namespace Zen.System.Modules;

public class VideoModulator
{
    private const int PaperStart = 14_360;

    private const int StatesPerPaperLine = 128;

    private const int StatesPerScreenLine = 224;

    private const int ScreenPixelCount = Constants.ScreenWidthPixels * Constants.ScreenHeightPixels;

    private int _previousCycles;

    private readonly Ram _ram;

    // .. S           F B     P P P I I I
    // .. 7 6 5 4 3 2 1 0 7 6 5 4 3 2 1 0
    // S == Whether pixel is set.
    private readonly ushort[] _screen = new ushort[ScreenPixelCount];

    private readonly ushort[] _frame = new ushort[ScreenPixelCount];

    private readonly byte[] _vram = new byte[0x4000];

    public ushort[] ScreenFrame => _frame;

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
        if (cycles < PaperStart || cycles > PaperStart + StatesPerScreenLine * (Constants.ScreenHeightPixels + 1))
        {
            _previousCycles = PaperStart;

            return;
        }

        var start = _previousCycles - PaperStart;

        _previousCycles = cycles;

        var end = cycles - PaperStart;

        var y = 0;

        while (start < end)
        {
            y = start / StatesPerScreenLine;

            if (y >= Constants.ScreenHeightPixels)
            {
                break;
            }

            var xS = start % StatesPerScreenLine;

            start++;

            if (xS >= StatesPerPaperLine)
            {
                continue;
            }

            var pixel = y * Constants.ScreenWidthPixels + xS * 2;

            _screen[pixel] = GetPixel(pixel);

            _screen[pixel + 1] = GetPixel(pixel + 1);
        }

        if (y >= Constants.ScreenHeightPixels)
        {
            Array.Copy(_screen, 0, _frame, 0, ScreenPixelCount);
        }
    }

    private ushort GetPixel(int pixel)
    {
        var y = pixel / Constants.ScreenWidthPixels;

        var x = pixel % Constants.ScreenWidthPixels;

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