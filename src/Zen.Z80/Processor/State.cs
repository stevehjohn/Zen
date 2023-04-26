namespace Zen.Z80.Processor;

public class State
{
    private const int RegisterCount = 22;

    private readonly byte[] _registers = new byte[RegisterCount];

    public ushort ProgramCounter { get; set; }

    public ushort StackPointer { get; set; }

    public byte Flags => _registers[(byte) Register.F];

    public bool this[Flag flag] => (this[Register.F] & (byte) flag) > 0;

    public byte this[Register register]
    {
        get => _registers[(byte) register];
        set => _registers[(byte) register] = value;
    }

    public ushort this[RegisterPair registerPair]
    {
        get
        {
            var positions = (int) registerPair;

            return (ushort) ((_registers[(positions & 0xFF00) >> 8] << 8) | _registers[positions & 0x00FF]);
        }
        set 
        {
            var positions = (int) registerPair;

            _registers[(positions & 0xFF00) >> 8] = (byte) ((value & 0xFF00) >> 8);
            _registers[positions & 0x00FF] = (byte) (value & 0x00FF);
        }
    }

    public void LoadRegisterPair(RegisterPair registerPair, byte[] data)
    {
        var positions = (int) registerPair;

        _registers[(positions & 0xFF00) >> 8] = data[1];
        _registers[positions & 0x00FF] = data[0];
    }
}