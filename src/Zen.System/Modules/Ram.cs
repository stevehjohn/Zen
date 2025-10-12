using Zen.Common;

namespace Zen.System.Modules;

public class Ram
{
    private const int BankCount = 8;

    private const int BankSlots = 4;

    private readonly byte[][] _banks;

    private readonly byte[][] _mappings;

    private readonly byte[] _slots = new byte[BankSlots];

    private readonly byte[] _rom = new byte[Constants.RomSize];

    private byte _screenBank = 5;

    public bool ProtectRom { get; init; }

    public bool UseShadowScreenBank
    {
        set => _screenBank = (byte) (value ? 7 : 5);
        get => _screenBank == 7;
    }

    public byte[] WorkingScreenRam => _banks[_screenBank];

    public byte[] Rom
    {
        get
        {
            var rom = new byte[Constants.RomSize];
            
            Array.Copy(_rom, rom, Constants.RomSize);
            
            return rom;
        }
    }

    public Ram()
    {
        _banks = new byte[BankCount][];

        for (var bank = 0; bank < BankCount; bank++)
        {
            _banks[bank] = new byte[Constants.RamBankSize];
        }

        _mappings = new byte[BankSlots][];

        _mappings[0] = _rom;
        _mappings[1] = _banks[5];
        _mappings[2] = _banks[2];
        _mappings[3] = _banks[0];

        _slots[0] = 8;
        _slots[1] = 5;
        _slots[2] = 2;
        _slots[3] = 0;

        CreateVRamNoise();
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
        if (bankNumber < 8)
        {
            _mappings[slotNumber] = _banks[bankNumber];
        }
        else
        {
            _mappings[slotNumber] = _rom;
        }

        _slots[slotNumber] = bankNumber;
    }

    public void Load(byte[] data, int destination)
    {
        for (var i = 0; i < data.Length; i++)
        {
            this[(ushort) (destination + i)] = data[i];
        }
    }

    public void LoadRom(byte[] data)
    {
        Array.Copy(data, 0, _rom, 0, data.Length);
    }

    public void LoadIntoBank(byte bankNumber, byte[] data)
    {
        Array.Copy(data, 0, _banks[bankNumber], 0, data.Length);
    }

    public byte[] GetBank(byte bankNumber)
    {
        return _banks[bankNumber];
    }

    public byte GetBankMapping(byte slotNumber)
    {
        return _slots[slotNumber];
    }

    private void CreateVRamNoise()
    {
        var random = new Random(Guid.NewGuid().GetHashCode());

        for (var b = 0; b < BankCount; b++)
        {
            for (ushort i = 0; i < Constants.RamBankSize; i++)
            {
                _banks[b][i] = (byte) random.Next(256);
            }
        }
    }
}