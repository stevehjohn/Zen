namespace Zen.Z80.Processor;

public class State
{
    private const int RegisterCount = 22;

    private readonly byte[] _registers = new byte[RegisterCount];

    private readonly byte[] _lastMCycles = new byte[6];

    public ushort ProgramCounter { get; set; }

    public ushort StackPointer { get; set; }

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

    public void SetFlag(Flag flag)
    {
        this[Register.F] |= (byte) flag;
    }

    public void ResetFlag(Flag flag)
    {
        this[Register.F] &= (byte) ~(byte) flag;
    }

    public void LoadRegisterPair(RegisterPair registerPair, byte[] data)
    {
        var positions = (int) registerPair;

        _registers[(positions & 0xFF00) >> 8] = data[1];
        _registers[positions & 0x00FF] = data[0];
    }

    public void SetMCycles(byte m1)
    {
        _lastMCycles[0] = m1;
        _lastMCycles[1] = 0;
        _lastMCycles[2] = 0;
        _lastMCycles[3] = 0;
        _lastMCycles[4] = 0;
        _lastMCycles[5] = 0;
    }

    public void SetMCycles(byte m1, byte m2)
    {
        _lastMCycles[0] = m1;
        _lastMCycles[1] = m2;
        _lastMCycles[2] = 0;
        _lastMCycles[3] = 0;
        _lastMCycles[4] = 0;
        _lastMCycles[5] = 0;
    }

    public void SetMCycles(byte m1, byte m2, byte m3)
    {
        _lastMCycles[0] = m1;
        _lastMCycles[1] = m2;
        _lastMCycles[2] = m3;
        _lastMCycles[3] = 0;
        _lastMCycles[4] = 0;
        _lastMCycles[5] = 0;
    }
}