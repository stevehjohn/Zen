using System;
using Zen.Common;
using Zen.System.Infrastructure;

namespace Zen.System.Modules;

public class VideoModulator
{
    private readonly int _screenStart;

    private readonly int _screenEnd;

    private readonly int _statesPerScreenLine;

    private const int StatesPerHBorder = Constants.BorderPixels / 2;

    private readonly Ram _ram;

    private int _previousCycles;

    private bool _renderedFrame;

    // .. S           F B     P P P I I I
    // .. 7 6 5 4 3 2 1 0 7 6 5 4 3 2 1 0
    // S == Whether pixel is set.
    private readonly ushort[] _screen = new ushort[Constants.ScreenPixelCount];

    private readonly ushort[] _frame = new ushort[Constants.ScreenPixelCount];

    public ushort[] ScreenFrame => _frame;

    public byte Border { get; set; }

    public byte FloatingBusValue { get; private set; }

    public VideoModulator(Model model, Ram ram)
    {
        _ram = ram;

        switch (model)
        {
            case Model.SpectrumPlus2A:
            case Model.SpectrumPlus3:
                _screenStart = Constants.PaperRegionStartPlus - Constants.StatesPerScreenLinePlus * Constants.BorderPixels - Constants.StatesPerScreenLinePlus;
                _screenEnd = _screenStart + Constants.StatesPerScreenLinePlus * (Constants.ScreenHeightPixels + 1);
                _statesPerScreenLine = Constants.StatesPerScreenLinePlus;

                break;

            default:
                _screenStart = Constants.PaperRegionStart - Constants.StatesPerScreenLine * Constants.BorderPixels;
                _screenEnd = _screenStart + Constants.StatesPerScreenLine * (Constants.ScreenHeightPixels + 1);
                _statesPerScreenLine = Constants.StatesPerScreenLine;

                break;
        }
    }

    public void CycleComplete(int cycles)
    {
        if (cycles < _screenStart || cycles > _screenEnd)
        {
            FloatingBusValue = 0xFF;

            _previousCycles = _screenStart;

            _renderedFrame = false;

            return;
        }

        var start = _previousCycles - _screenStart;

        _previousCycles = cycles;

        var end = cycles - _screenStart;

        var y = 0;

        while (start < end)
        {
            y = start / _statesPerScreenLine;

            var xS = start % _statesPerScreenLine;

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

                FloatingBusValue = 0xFF;

                continue;
            }

            var ramPixel = (y - Constants.BorderPixels) * Constants.PaperWidthPixels + (xS - Constants.BorderPixels / 2) * 2;

            _screen[pixel] = GetPixel(ramPixel);

            _screen[pixel + 1] = GetPixel(ramPixel + 1);

            FloatingBusValue = _ram.WorkingScreenRam[(ushort) (ramPixel + 1) & 0b0011_1111_1111_1111];
        }

        if (y >= Constants.ScreenHeightPixels && ! _renderedFrame)
        {
            Array.Copy(_screen, 0, _frame, 0, Constants.ScreenPixelCount);

            _renderedFrame = true;
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

        var set = (_ram.WorkingScreenRam[address] & xO) > 0;

        var colourAddress = 0x1800;

        var offset = xB + y / 8 * 32;

        colourAddress += offset;

        var attributes = _ram.WorkingScreenRam[colourAddress];

        var result = (ushort) (attributes & 0b0011_1111);

        result |= (ushort) ((attributes & 0b1100_0000) << 2);

        if (set)
        {
            result |= 0b1000_0000_0000_0000;
        }

        return result;
    }
}