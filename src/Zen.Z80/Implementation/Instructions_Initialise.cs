// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void Initialise()
    {
        InitialiseBaseInstructions();

        InitialiseCBInstructions();

        InitialiseDDInstructions();

        InitialiseDDCBFDCBInstructions();

        InitialiseFDInstructions();
    }

    private void InitialiseLDByteInstructions(int prefix = 0x00)
    {
        var extraCycles = (byte) (prefix != 0x00 ? 4 : 0);

        for (var left = 0; left < 8; left++)
        {
            if (left == 6)
            {
                for (var right = 0; right < 8; right++)
                {
                    var opCode = prefix + 0x40 + left * 8 + right;

                    if (right == 6)
                    {
                        _instructions.Add(opCode, new Instruction(_ => HALT(), "HALT", opCode, 0, extraCycles));

                        continue;
                    }

                    var rightRegister = right switch
                    {
                        0 => Register.B,
                        1 => Register.C,
                        2 => Register.D,
                        3 => Register.E,
                        4 => Register.H,
                        5 => Register.L,
                        7 => Register.A
                    };

                    switch (prefix)
                    {
                        case 0xDD00:
                            _instructions.Add(opCode, new Instruction(p => LD_aRRd_R(RegisterPair.IX, p, rightRegister), $"LD (IX + d), {rightRegister}", opCode, 1));

                            break;

                        case 0xFD00:
                            _instructions.Add(opCode, new Instruction(p => LD_aRRd_R(RegisterPair.IY, p, rightRegister), $"LD (IY + d), {rightRegister}", opCode, 1));

                            break;

                        default:
                            _instructions.Add(opCode, new Instruction(_ => LD_aRR_R(RegisterPair.HL, rightRegister), $"LD (HL), {rightRegister}", opCode, 0));

                            break;
                    }
                }

                continue;
            }

            var leftRegister = left switch
            {
                0 => Register.B,
                1 => Register.C,
                2 => Register.D,
                3 => Register.E,
                4 => prefix == 0x00 ? Register.H : prefix == 0xDD00 ? Register.IXh : Register.IYh,
                5 => prefix == 0x00 ? Register.L : prefix == 0xDD00 ? Register.IXl : Register.IYl,
                7 => Register.A
            };

            for (var right = 0; right < 8; right++)
            {
                var opCode = prefix + 0x40 + left * 8 + right;

                if (right == 6)
                {
                    if (prefix != 0x00)
                    {
                        var pair = prefix == 0xDD00 ? RegisterPair.IX : RegisterPair.IY;

                        switch (leftRegister)
                        {
                            case Register.IXh:
                            case Register.IYh:
                                _instructions.Add(opCode, new Instruction(p => LD_R_aRRd(Register.H, pair, p), $"LD H, ({pair} + d)", opCode, 1));

                                break;

                            case Register.IXl:
                            case Register.IYl:
                                _instructions.Add(opCode, new Instruction(p => LD_R_aRRd(Register.L, pair, p), $"LD L, ({pair} + d)", opCode, 1));

                                break;

                            default:
                                _instructions.Add(opCode, new Instruction(p => LD_R_aRRd(leftRegister, pair, p), $"LD {leftRegister}, ({pair} + d)", opCode, 1));

                                break;
                        }
                    }
                    else
                    {
                        _instructions.Add(opCode, new Instruction(_ => LD_R_aRR(leftRegister, RegisterPair.HL), $"LD {leftRegister}, (HL)", opCode, 0));
                    }

                    continue;
                }

                var rightRegister = right switch
                {
                    0 => Register.B,
                    1 => Register.C,
                    2 => Register.D,
                    3 => Register.E,
                    4 => prefix == 0x00 ? Register.H : prefix == 0xDD00 ? Register.IXh : Register.IYh,
                    5 => prefix == 0x00 ? Register.L : prefix == 0xDD00 ? Register.IXl : Register.IYl,
                    7 => Register.A
                };

                _instructions.Add(opCode, new Instruction(_ => LD_R_R(leftRegister, rightRegister), $"LD {leftRegister}, {rightRegister}", opCode, 0, extraCycles));
            }
        }
    }
}