using Zen.Common;

namespace Zen.System.Modules;

public class VideoModulator
{
    private const int PaperStart = 14_335;

    private const int StatesPerPaperLine = 128;

    private const int StatesPerScreenLine = 224;

    private const int FlashFrames = 20;

    private const int ScreenPixelCount = Constants.ScreenWidthPixels * Constants.ScreenHeightPixels;

    private int _previousCycles;

    private ulong _frameCount;

    private bool _flash;

    private readonly Ram _ram;

    // B         C C C
    // 7 6 5 4 3 2 1 0
    private readonly byte[] _screen = new byte[ScreenPixelCount];

    private readonly byte[] _frame = new byte[ScreenPixelCount];

    private readonly byte[] _vram = new byte[0x4000];

    public byte[] ScreenFrame => _frame;

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

            unchecked
            {
                _frameCount++;
            }

            if (_frameCount % FlashFrames == 0)
            {
                _flash = ! _flash;
            }
        }
    }

    private byte GetPixel(int pixel)
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

        var paper = (byte) ((attributes & 0b0011_1000) >> 3);

        var ink = (byte) (attributes & 0b0000_0111);

        byte result;

        if ((attributes & 0b1000_0000) > 0 && _flash)
        {
            result = set ? paper : ink;
        }
        else
        {
            result = set ? ink : paper;
        }

        result |= (byte) ((attributes & 0b0100_0000) << 1);

        return result;
    }
}