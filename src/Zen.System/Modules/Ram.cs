namespace Zen.System.Modules;

public class Ram
{
    private const int BankCount = 8;

    private const int BankSlots = 4;

    private const int BankSize = 0x4000;

    private const int RomSize = 0x4000;

    private readonly byte[][] _banks;

    private readonly byte[][] _mappings;

    private readonly byte[] _slots = new byte[BankSlots];

    private readonly byte[] _rom = new byte[RomSize];

    private byte _romNumber;

    public bool ProtectRom { get; set; }

    public byte RomNumber => _romNumber;

    public byte[] ScreenRam => _banks[5];

    public Ram()
    {
        _banks = new byte[BankCount][];

        for (var bank = 0; bank < BankCount; bank++)
        {
            _banks[bank] = new byte[BankSize];
        }

        _mappings = new byte[BankSlots][];

        _mappings[0] = _rom;
        _mappings[1] = _banks[5];
        _mappings[2] = _banks[2];
        _mappings[3] = _banks[0];
    }

    public byte this[ushort address]
    {
        get => _mappings[(address & 0b1100_0000_0000_0000) >> 14][address & 0b0011_1111_1111_1111];
        set
        {
            if (address < 0x4000 && ProtectRom)
            {
                return;
            }

            _mappings[(address & 0b1100_0000_0000_0000) >> 14][address & 0b0011_1111_1111_1111] = value;
        }
    }

    public void SetBank(byte slotNumber, byte bankNumber)
    {
        _mappings[slotNumber] = _banks[bankNumber];

        _slots[slotNumber] = bankNumber;
    }

    public void LoadRom(byte[] data, byte romNumber)
    {
        _romNumber = romNumber;

        Array.Copy(data, 0, _rom, 0, data.Length);
    }

    public void LoadIntoBank(byte bankNumber, byte[] data)
    {
        Array.Copy(data, 0, _banks[bankNumber], 0, data.Length);
    }
}