namespace Zen.System.Modules;

public class Ports
{
    public required Action<ushort, byte> PortDataChanged { private get; set; }

    private readonly byte?[] _data = new byte?[65_536];

    public byte this[ushort port, bool suppressEvent = false]
    {
        get => GetPortData(port);

        set => SetPortData(port, value, suppressEvent);
    }

    private byte GetPortData(ushort port)
    {
        // Kempston.
        if ((port & 0xFF) is 0x1F or 0xDF)
        {
            return 0x00;
        }

        // Keyboard.
        if ((port & 0x01) == 0)
        {
            var value = (byte) 0b1011_1111;

            var high = (port & 0xFF00) >> 8;

            if ((high & 0b0000_0001) == 0) value &= _data[0b1111_1110_1111_1110] ?? 0xFF;
            if ((high & 0b0000_0010) == 0) value &= _data[0b1111_1101_1111_1110] ?? 0xFF;
            if ((high & 0b0000_0100) == 0) value &= _data[0b1111_1011_1111_1110] ?? 0xFF;
            if ((high & 0b0000_1000) == 0) value &= _data[0b1111_0111_1111_1110] ?? 0xFF;
            if ((high & 0b0001_0000) == 0) value &= _data[0b1110_1111_1111_1110] ?? 0xFF;
            if ((high & 0b0010_0000) == 0) value &= _data[0b1101_1111_1111_1110] ?? 0xFF;
            if ((high & 0b0100_0000) == 0) value &= _data[0b1011_1111_1111_1110] ?? 0xFF;
            if ((high & 0b1000_0000) == 0) value &= _data[0b0111_1111_1111_1110] ?? 0xFF;

            return value;
        }

        // Disk drive (+2A/3 only).
        if (port == 0x2FFD)
        {
            return 0b10000000;
        }

        return _data[port] ?? 0xFF;
    }

    // TODO: suppressEvent is a hack.
    // Have motherboard expose a property that "peripherals" can attach to and be polled for state.
    private void SetPortData(ushort port, byte data, bool suppressEvent = false)
    {
        _data[port] = data;

        if (! suppressEvent)
        {
            PortDataChanged(port, data);
        }
    }
}