// ReSharper disable InconsistentNaming

namespace Zen.Common;

public static class Constants
{
    /* Screen related */
    public const int ScreenWidthPixels = 288;

    public const int ScreenHeightPixels = 224;

    public const int PaperWidthPixels = 256;

    public const int PaperHeightPixels = 192;

    public const int BorderPixels = 16;
    
    public const int ScreenPixelCount = ScreenWidthPixels * ScreenHeightPixels;

    /* Timing */
    public const int FrameCycles = 69_888;

    public const int StatesPerScreenLine = 224;

    public const int PaperRegionStart = 14_336;

    public const int InterruptStart = 24;

    public const int InterruptEnd = 56;

    public const int FrameCyclesPlus = 70_908;

    public const int StatesPerScreenLinePlus = 228;

    public const int PaperRegionStartPlus = 14_592;

    public const int InterruptStartPlus = 32;

    public const int InterruptEndPlus = 64;

    public const int StatesPerPaperLine = 128;

    public const int DisplayStartState = (16 + 48 - BorderPixels) * StatesPerScreenLine;

    public const int DisplayEndState = DisplayStartState + (ScreenHeightPixels + BorderPixels * 2 + 1) * StatesPerScreenLine;

    public const int DisplayStartStatePlus = (16 + 48 - BorderPixels) * StatesPerScreenLinePlus;

    public const int DisplayEndStatePlus = DisplayStartStatePlus + (ScreenHeightPixels + BorderPixels * 2 + 1) * StatesPerScreenLinePlus;

    /* RAM */
    public const int RamBankSize = 0x4000;

    public const int RomSize = 0x4000;

    /* Spectrum */
    public const int SpectrumFramesPerSecond = 50;

    /* Wave Visualiser */
    public const int WaveVisualisationPanelWidth = 200;

    /* Spectrum Analyser Visualiser 8*/
    public const int SpectrumVisualisationPanelWidth = 200;
    
    /* Counters Visualiser */
    public const int CountersPanelHeight = 24;
    
    /* Video RAM Visualiser */
    public const int VideoRamVisualisationPanelWidth = ScreenWidthPixels;

    public const int SlowScanFactor = 4;
}