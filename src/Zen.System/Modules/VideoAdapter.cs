using Zen.Common;

namespace Zen.System.Modules;

public class VideoAdapter
{
    private const int PaperStart = 14_336;

    private const int StatesPerPaperLine = 128;

    private const int ScreenPixelCount = Constants.ScreenWidthPixels * Constants.ScreenHeightPixels;

    private int _previousLinePosition;

    private int _pixel;

    // F   B     C C C
    // 7 6 5 4 3 2 1 0
    private readonly byte[] _screen = new byte[ScreenPixelCount];

    public void MCycleComplete(int cycles)
    {
        if (cycles < 14_336 || _pixel >= ScreenPixelCount)
        {
            return;
        }

        var linePosition = (cycles - PaperStart) % 224;

        if (linePosition > StatesPerPaperLine)
        {
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
                break;
            }
        }
    }

    public void Reset()
    {
        _previousLinePosition = 0;
    }

    private byte GetPixel()
    {
        // packedValue = (uint) ((int) alpha << 24 | (int) b << 16 | (int) g << 8) | (uint) r;

        var y = _pixel / Constants.ScreenWidthPixels;

        var x = _pixel % Constants.ScreenWidthPixels;

        var xB = x / 8;

        var address = 0;

        address |= (y & 0b0000_0111) << 8;

        address |= (y & 0b1100_0000) << 5;

        address |= (y & 0b0011_1000) << 2;

        address |= xB;

        return 0xFF;
    }
}