// ReSharper disable StringLiteralTypo
// ReSharper disable InconsistentNaming

using Zen.Z80.Processor;

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseBaseInstructions()
    {
        _instructions.Add(0x00, new Instruction(_ => NOP(), "NOP", 0x00, 0));

        InitialiseLDByteInstructions();

        InitialiseArithmeticLogicInstructions();

        InitialiseRETInstructions();

        _instructions.Add(0x01, new Instruction(d => LD_RR_nn(RegisterPair.BC, d), "LD BC, nn", 0x01, 2));

        _instructions.Add(0xCB, new Instruction(_ => PREFIX(0xCB), "PREFIX 0xCB", 0xCB, 0));

        _instructions.Add(0xDD, new Instruction(_ => PREFIX(0xDD), "PREFIX 0xDD", 0xDD, 0));

        _instructions.Add(0xFD, new Instruction(_ => PREFIX(0xFD), "PREFIX 0xFD", 0xFD, 0));
    }

    private void InitialiseLDByteInstructions()
    {
        for (var left = 0; left < 8; left++)
        {
            if (left == 6)
            {
                for (var right = 0; right < 8; right++)
                {
                    var opCode = 0x40 + left * 8 + right;

                    if (right == 6)
                    {
                        _instructions.Add(opCode, new Instruction(_ => HALT(), "HALT", opCode, 0));

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

                    _instructions.Add(opCode, new Instruction(_ => LD_aRR_R(RegisterPair.HL, rightRegister), $"LD (HL), {rightRegister}", opCode, 0));
                }

                continue;
            }

            var leftRegister = left switch
            {
                0 => Register.B,
                1 => Register.C,
                2 => Register.D,
                3 => Register.E,
                4 => Register.H,
                5 => Register.L,
                7 => Register.A
            };

            for (var right = 0; right < 8; right++)
            {
                var opCode = 0x40 + left * 8 + right;

                if (right == 6)
                {
                    _instructions.Add(opCode, new Instruction(_ => LD_R_aRR(leftRegister, RegisterPair.HL), $"LD {leftRegister}, (HL)", opCode, 0));

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

                _instructions.Add(opCode, new Instruction(_ => LD_R_R(leftRegister, rightRegister), $"LD {leftRegister}, {rightRegister}", opCode, 0));
            }
        }
    }

    private void InitialiseArithmeticLogicInstructions()
    {
        for (var index = 0; index < 8; index++)
        {
            if (index == 6)
            {
                _instructions.Add(0x80 + index, new Instruction(_ => ADD_R_aRR(Register.A, RegisterPair.HL), "ADD A, (HL)", 0x80 + index, 0));

                _instructions.Add(0x88 + index, new Instruction(_ => ADC_R_aRR(Register.A, RegisterPair.HL), "ADC A, (HL)", 0x88 + index, 0));

                _instructions.Add(0x90 + index, new Instruction(_ => SUB_R_aRR(Register.A, RegisterPair.HL), "SUB A, (HL)", 0x90 + index, 0));

                _instructions.Add(0x98 + index, new Instruction(_ => SBC_R_aRR(Register.A, RegisterPair.HL), "SBC A, (HL)", 0x98 + index, 0));

                _instructions.Add(0xA0 + index, new Instruction(_ => AND_R_aRR(Register.A, RegisterPair.HL), "AND A, (HL)", 0xA0 + index, 0));

                _instructions.Add(0xA8 + index, new Instruction(_ => XOR_R_aRR(Register.A, RegisterPair.HL), "XOR A, (HL)", 0xA8 + index, 0));

                _instructions.Add(0xB0 + index, new Instruction(_ => OR_R_aRR(Register.A, RegisterPair.HL), "OR A, (HL)", 0xB0 + index, 0));

                _instructions.Add(0xB8 + index, new Instruction(_ => CP_R_aRR(Register.A, RegisterPair.HL), "CP A, (HL)", 0xB8 + index, 0));

                continue;
            }

            var rightRegister = index switch
            {
                0 => Register.B,
                1 => Register.C,
                2 => Register.D,
                3 => Register.E,
                4 => Register.H,
                5 => Register.L,
                7 => Register.A
            };

            _instructions.Add(0x80 + index, new Instruction(_ => ADD_R_R(Register.A, rightRegister), $"ADD A, {rightRegister}", 0x80 + index, 0));

            _instructions.Add(0x88 + index, new Instruction(_ => ADC_R_R(Register.A, rightRegister), $"ADC A, {rightRegister}", 0x88 + index, 0));

            _instructions.Add(0x90 + index, new Instruction(_ => SUB_R_R(Register.A, rightRegister), $"SUB A, {rightRegister}", 0x90 + index, 0));

            _instructions.Add(0x98 + index, new Instruction(_ => SBC_R_R(Register.A, rightRegister), $"SBC A, {rightRegister}", 0x98 + index, 0));

            _instructions.Add(0xA0 + index, new Instruction(_ => AND_R_R(Register.A, rightRegister), $"AND A, {rightRegister}", 0xA0 + index, 0));

            _instructions.Add(0xA8 + index, new Instruction(_ => XOR_R_R(Register.A, rightRegister), $"XOR A, {rightRegister}", 0xA8 + index, 0));

            _instructions.Add(0xB0 + index, new Instruction(_ => OR_R_R(Register.A, rightRegister), $"OR A, {rightRegister}", 0xB0 + index, 0));

            _instructions.Add(0xB8 + index, new Instruction(_ => CP_R_R(Register.A, rightRegister), $"CP A, {rightRegister}", 0xB8 + index, 0));
        }
    }

    private void InitialiseRETInstructions()
    {
        _instructions.Add(0xC0, new Instruction(_ => RET(Flag.Zero, true), "RET NZ", 0xC0, 0));

        _instructions.Add(0xD0, new Instruction(_ => RET(Flag.Carry, true), "RET NC", 0xD0, 0));

        _instructions.Add(0xE0, new Instruction(_ => RET(Flag.ParityOverflow, true), "RET PO", 0xE0, 0));

        _instructions.Add(0xF0, new Instruction(_ => RET(Flag.Sign, true), "RET NS", 0xF0, 0));

        _instructions.Add(0xC8, new Instruction(_ => RET(Flag.Zero), "RET Z", 0xC8, 0));

        _instructions.Add(0xD8, new Instruction(_ => RET(Flag.Carry), "RET C", 0xD8, 0));

        _instructions.Add(0xE8, new Instruction(_ => RET(Flag.ParityOverflow), "RET PE", 0xE8, 0));

        _instructions.Add(0xF8, new Instruction(_ => RET(Flag.Sign), "RET S", 0xF8, 0));
    }
}