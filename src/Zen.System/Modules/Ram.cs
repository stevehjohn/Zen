namespace Zen.System.Modules;

public class Ram
{
    private const int BankCount = 8;

    private const int BankSlots = 4;

    private const int BankSize = 0x4000;

    private const int RomSize = 0x4000;

    private readonly byte[][] _banks;

    private readonly byte[][] _mappings;

    private readonly byte[] _rom = new byte[RomSize];

    public bool ProtectRom { get; set; }

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

    public void SetBank(int slotNumber, int bankNumber)
    {
        _mappings[slotNumber] = _banks[bankNumber];
    }
}