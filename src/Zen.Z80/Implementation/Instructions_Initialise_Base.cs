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
        }
    }
}