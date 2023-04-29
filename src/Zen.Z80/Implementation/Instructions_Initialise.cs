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

        InitialiseEDInstructions();

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

    private void InitialiseArithmeticLogicInstructions(int prefix = 0x00)
    {
        var extraCycles = (byte) (prefix != 0x00 ? 4 : 0);

        for (var index = 0; index < 8; index++)
        {
            if (index == 6)
            {
                if (prefix == 0x00)
                {
                    _instructions.Add(prefix + 0x80 + index, new Instruction(_ => ADD_R_aRR(Register.A, RegisterPair.HL), "ADD A, (HL)", prefix + 0x80 + index, 0, extraCycles));

                    _instructions.Add(prefix + 0x88 + index, new Instruction(_ => ADC_R_aRR(Register.A, RegisterPair.HL), "ADC A, (HL)", prefix + 0x88 + index, 0, extraCycles));

                    _instructions.Add(prefix + 0x90 + index, new Instruction(_ => SUB_R_aRR(Register.A, RegisterPair.HL), "SUB A, (HL)", prefix + 0x90 + index, 0, extraCycles));

                    _instructions.Add(prefix + 0x98 + index, new Instruction(_ => SBC_R_aRR(Register.A, RegisterPair.HL), "SBC A, (HL)", prefix + 0x98 + index, 0, extraCycles));

                    _instructions.Add(prefix + 0xA0 + index, new Instruction(_ => AND_R_aRR(Register.A, RegisterPair.HL), "AND A, (HL)", prefix + 0xA0 + index, 0, extraCycles));

                    _instructions.Add(prefix + 0xA8 + index, new Instruction(_ => XOR_R_aRR(Register.A, RegisterPair.HL), "XOR A, (HL)", prefix + 0xA8 + index, 0, extraCycles));

                    _instructions.Add(prefix + 0xB0 + index, new Instruction(_ => OR_R_aRR(Register.A, RegisterPair.HL), "OR A, (HL)", prefix + 0xB0 + index, 0, extraCycles));

                    _instructions.Add(prefix + 0xB8 + index, new Instruction(_ => CP_R_aRR(Register.A, RegisterPair.HL), "CP A, (HL)", prefix + 0xB8 + index, 0, extraCycles));
                }
                else
                {
                    var pair = prefix == 0xDD00 ? RegisterPair.IX : RegisterPair.IY;

                    _instructions.Add(prefix + 0x80 + index, new Instruction(p => ADD_R_aRRd(Register.A, pair, p), $"ADD A, ({pair} + d)", prefix + 0x80 + index, 1));

                    _instructions.Add(prefix + 0x88 + index, new Instruction(p => ADC_R_aRRd(Register.A, pair, p), $"ADC A, ({pair} + d)", prefix + 0x88 + index, 1));

                    _instructions.Add(prefix + 0x90 + index, new Instruction(p => SUB_R_aRRd(Register.A, pair, p), $"SUB A, ({pair} + d)", prefix + 0x90 + index, 1));

                    _instructions.Add(prefix + 0x98 + index, new Instruction(p => SBC_R_aRRd(Register.A, pair, p), $"SBC A, ({pair} + d)", prefix + 0x98 + index, 1));

                    _instructions.Add(prefix + 0xA0 + index, new Instruction(p => AND_R_aRRd(Register.A, pair, p), $"AND A, ({pair} + d)", prefix + 0xA0 + index, 1));

                    _instructions.Add(prefix + 0xA8 + index, new Instruction(p => XOR_R_aRRd(Register.A, pair, p), $"XOR A, ({pair} + d)", prefix + 0xA8 + index, 1));

                    _instructions.Add(prefix + 0xB0 + index, new Instruction(p => OR_R_aRRd(Register.A, pair, p), $"OR A, ({pair} + d)", prefix + 0xB0 + index, 1));

                    _instructions.Add(prefix + 0xB8 + index, new Instruction(p => CP_R_aRRd(Register.A, pair, p), $"CP A, ({pair} + d)", prefix + 0xB8 + index, 1));
                }

                continue;
            }

            var rightRegister = index switch
            {
                0 => Register.B,
                1 => Register.C,
                2 => Register.D,
                3 => Register.E,
                4 => prefix == 0x00 ? Register.H : prefix == 0xDD00 ? Register.IXh : Register.IYh,
                5 => prefix == 0x00 ? Register.L : prefix == 0xDD00 ? Register.IXl : Register.IYl,
                7 => Register.A
            };

            _instructions.Add(prefix + 0x80 + index, new Instruction(_ => ADD_R_R(Register.A, rightRegister), $"ADD A, {rightRegister}", prefix + 0x80 + index, 0, extraCycles));

            _instructions.Add(prefix + 0x88 + index, new Instruction(_ => ADC_R_R(Register.A, rightRegister), $"ADC A, {rightRegister}", prefix + 0x88 + index, 0, extraCycles));

            _instructions.Add(prefix + 0x90 + index, new Instruction(_ => SUB_R_R(Register.A, rightRegister), $"SUB A, {rightRegister}", prefix + 0x90 + index, 0, extraCycles));

            _instructions.Add(prefix + 0x98 + index, new Instruction(_ => SBC_R_R(Register.A, rightRegister), $"SBC A, {rightRegister}", prefix + 0x98 + index, 0, extraCycles));

            _instructions.Add(prefix + 0xA0 + index, new Instruction(_ => AND_R_R(Register.A, rightRegister), $"AND A, {rightRegister}", prefix + 0xA0 + index, 0, extraCycles));

            _instructions.Add(prefix + 0xA8 + index, new Instruction(_ => XOR_R_R(Register.A, rightRegister), $"XOR A, {rightRegister}", prefix + 0xA8 + index, 0, extraCycles));

            _instructions.Add(prefix + 0xB0 + index, new Instruction(_ => OR_R_R(Register.A, rightRegister), $"OR A, {rightRegister}", prefix + 0xB0 + index, 0, extraCycles));

            _instructions.Add(prefix + 0xB8 + index, new Instruction(_ => CP_R_R(Register.A, rightRegister), $"CP A, {rightRegister}", prefix + 0xB8 + index, 0, extraCycles));
        }
    }
}