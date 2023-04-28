// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    // TODO: Check cycles of all...
    public void BIT_b_aRRd(byte bit, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & bit);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result == 0;
            _state[Flag.X1] = (address & 0x0800) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (address & 0x2000) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;

            _state.MemPtr = address;
        }

        _state.SetMCycles(4, 4, 3, 5, 4);
    }
    
    public void BIT_b_aRR(byte bit, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & bit);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result == 0;
            _state[Flag.X1] = (_state.MemPtr & 0x0800) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (_state.MemPtr & 0x2000) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = bit == 0x80 && result != 0;
        }

        _state.SetMCycles(4, 4, 4);
    }
    
    public void BIT_b_R(byte bit, Register source)
    {
        unchecked
        {
            var data = _state[source];

            var result = (byte) (data & bit);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result == 0;
            _state[Flag.X1] = (data & 0x08) > 0;
            _state[Flag.HalfCarry] = true;
            _state[Flag.X2] = (data & 0x20) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = bit == 0x80 && result != 0;
        }

        _state.SetMCycles(4, 4);
    }

    public void RES_b_aRRd(byte bit, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & ~bit);

            _interface.WriteToMemory(address, result);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    public void RES_b_aRRd_R(byte bit, RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort) (address + (sbyte) parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & ~bit);

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    public void RES_b_aRR(byte bit, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var result = (byte) (data & ~bit);

            _interface.WriteToMemory(address, result);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }
    
    public void RES_b_R(byte bit, Register source)
    {
        unchecked
        {
            var data = _state[source];

            var result = (byte) (data & ~bit);

            _state[source] = result;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4);
    }

    public void SET_b_aRR(byte bit, RegisterPair source)
    {
        unchecked
        {
            var address = _state[source];

            var data = _interface.ReadFromMemory(address);

            var result = (byte)(data | bit);

            _interface.WriteToMemory(address, result);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    public void SET_b_aRRd(byte bit, RegisterPair source, byte[] parameters)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort)(address + (sbyte)parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte)(data | bit);

            _interface.WriteToMemory(address, result);

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }

    public void SET_b_aRRd_R(byte bit, RegisterPair source, byte[] parameters, Register target)
    {
        unchecked
        {
            var address = _state[source];

            address = (ushort)(address + (sbyte)parameters[0]);

            var data = _interface.ReadFromMemory(address);

            var result = (byte)(data | bit);

            _interface.WriteToMemory(address, result);

            _state[target] = result;

            _state.MemPtr = address;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4, 3, 5, 4, 3);
    }
        
    public void SET_b_R(byte bit, Register source)
    {
        unchecked
        {
            var data = _state[source];

            var result = (byte) (data | bit);

            _state[source] = result;

            _state.Q = 0;
        }

        _state.SetMCycles(4, 4);
    }
}