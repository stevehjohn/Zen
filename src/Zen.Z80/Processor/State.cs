namespace Zen.Z80.Processor;

public class State
{
    private const int RegisterCount = 22;

    private readonly byte[] _registers = new byte[RegisterCount];

    public ushort ProgramCounter { get; set; }

    public ushort StackPointer { get; set; }

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
}