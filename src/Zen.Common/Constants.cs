namespace Zen.Common;

public static class Constants
{
    /* Screen related */
    public const int ScreenWidthPixels = 288;

    public const int ScreenHeightPixels = 224;

    public const int PaperWidthPixels = 256;

    public const int PaperHeightPixels = 192;

    public const int BorderPixels = 16;
    
    public const int ScreenPixelCount = Constants.ScreenWidthPixels * Constants.ScreenHeightPixels;

    /* Timing */
    public const int FrameCycles = 69_888;

    public const int StatesPerScreenLine = 224;

    public const int StatesPerPaperLine = 128;

    public const int PaperRegionStart = 14_336;

    /* RAM */
    public const int ScreenRamSize = 0x1B00;

    public const int RamBankSize = 0x4000;

    public const int RomSize = 0x4000;
}