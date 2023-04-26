﻿// ReSharper disable InconsistentNaming
using Zen.Z80.Processor;

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    public void BIT_0_IX_d(byte bit, RegisterPair registerPair, byte[] parameters)
    {
        unchecked
        {
            var address = _state[registerPair];

            address = (ushort) (address + (sbyte) parameters[0]);

            // TODO: Get from RAM somehow...?
            var data = 0x00;

            var result = (byte) (data & bit);

            // Carry unaffected
            _state[Flag.AddSubtract] = false;
            _state[Flag.ParityOverflow] = result == 0;
            _state[Flag.X1] = (address & 0x0800) > 0;
            _state[Flag.HalfCarry] = false;
            _state[Flag.X2] = (address & 0x2000) > 0;
            _state[Flag.Zero] = result == 0;
            _state[Flag.Sign] = (sbyte) result < 0;
        }

        _state.SetMCycles(4, 4);
    }
}