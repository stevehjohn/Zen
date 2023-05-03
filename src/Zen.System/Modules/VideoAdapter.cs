using Zen.Common;

namespace Zen.System.Modules;

public class VideoAdapter
{
    private const int PaperStart = 14_336;

    private const int StatesPerPaperLine = 128;

    private const int ScreenPixelCount = Constants.ScreenWidthPixels * Constants.ScreenHeightPixels;

    private int _previousLinePosition;

    private int _pixel;

    private readonly Ram _ram;

    // B         C C C
    // 7 6 5 4 3 2 1 0
    private readonly byte[] _screen = new byte[ScreenPixelCount];

    private readonly byte[] _frame = new byte[ScreenPixelCount];

    public byte[] ScreenFrame => _frame;

    public VideoAdapter(Ram ram)
    {
        _ram = ram;
    }

    public void MCycleComplete(int cycles)
    {
        if (cycles < 14_336 || _pixel >= ScreenPixelCount)
        {
            return;
        }

        var linePosition = (cycles - PaperStart) % 224;

        if (linePosition > StatesPerPaperLine)
        {
            _previousLinePosition = 0;

            return;
        }

        var pixels = (linePosition - _previousLinePosition) * 2;

        _previousLinePosition = linePosition;

        for (var p = 0; p < pixels; p++)
        {
            _screen[_pixel] = GetPixel();

            _pixel++;

            if (_pixel == ScreenPixelCount)
            {
                Array.Copy(_screen, 0, _frame, 0, ScreenPixelCount);

                break;
            }
        }
    }

    public void Reset()
    {
        _pixel = 0;
    }

    private byte GetPixel()
    {
        var y = _pixel / Constants.ScreenWidthPixels;

        var x = _pixel % Constants.ScreenWidthPixels;

        var xB = x / 8;

        var xO = 1 << (7 - x % 8);

        var address = 0;

        address |= (y & 0b0000_0111) << 8;

        address |= (y & 0b1100_0000) << 5;

        address |= (y & 0b0011_1000) << 2;

        address |= xB;

        var set = (_ram.WorkingScreenRam[address] & xO) > 0;

        var colourAddress = 0x1800;

        var offset = xB + y / 8 * 32;

        colourAddress += offset;

        var attributes = _ram.WorkingScreenRam[colourAddress];

        var result = (byte) (set ? attributes & 0b0000_0111 : (attributes & 0b0011_1000) >> 3);

        result |= (byte) ((attributes & 0b0100_0000) << 1);

        return result;
    }
}