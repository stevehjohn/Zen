﻿using Zen.Common.Infrastructure;
using Zen.Z80.Implementation;
using Zen.Z80.Interfaces;

namespace Zen.Z80.Processor;

public class Core
{
    private readonly Interface _interface;

    private readonly State _state;

    private readonly Instructions _instructions;

    private readonly List<IProcessorHook> _hooks = new();

    private readonly byte[] _parameters = new byte[2];

    private IProcessorHook? _currentHook;

    public Core(Interface @interface, State state)
    {
        _interface = @interface;

        _state = state;

        _instructions = new Instructions(_interface, _state);

        File.Delete("Zen.log");
    }

    public void ExecuteCycle()
    {
        _state.IgnoreNextInterrupt = false;

        if (_currentHook == null)
        {
            foreach (var hook in _hooks)
            {
                if (hook.Activate(_state))
                {
                    _currentHook = hook;

                    return;
                }
            }
        }
        else
        {
            if (_currentHook.ExecuteCycle(_state, _interface))
            {
                _currentHook = null;
            }

            return;
        }

        foreach (var hook in _hooks)
        {
            hook.PassiveCycle(_state, _interface);
        }

        Instruction? instruction;

        if (_state.Halted)
        {
            HandleInterrupts();

            instruction = _instructions[0x00];

            instruction.Execute(Array.Empty<byte>());

            Counters.Instance.IncrementCounter(Counter.Instructions);

            Counters.Instance.IncrementCounter(Counter.Hertz, (int) _state.ClockCycles);

            return;
        }

        var data = _interface.ReadFromMemory(_state.ProgramCounter);

        var opcode = _state.InstructionPrefix << 8 | data;

        instruction = _instructions[opcode];

        if (! instruction.IsPrefix)
        {
            _state.LastInstruction = instruction;
        }

        _state.InstructionPrefix = 0;

        _state.ProgramCounter++;

        if (instruction.ParameterLength > 0)
        {
            for (var p = 0; p < instruction.ParameterLength; p++)
            {
                data = _interface.ReadFromMemory(_state.ProgramCounter);

                _state.ProgramCounter++;

                _parameters[p] = data;
            }
        }

        UpdateR(instruction);

        instruction.Execute(_parameters);

        Counters.Instance.IncrementCounter(Counter.Hertz, (int) _state.ClockCycles);

        Counters.Instance.IncrementCounter(Counter.Instructions);

        if (_state.InstructionPrefix > 0xFF)
        {
            opcode = _state.InstructionPrefix << 8 | _parameters[1];

            instruction = _instructions[opcode];

            UpdateR(instruction);

            instruction.Execute(_parameters[..1]);

            Counters.Instance.IncrementCounter(Counter.Hertz, (int) _state.ClockCycles);

            Counters.Instance.IncrementCounter(Counter.Instructions);

            _state.LastInstruction = instruction;

            _state.InstructionPrefix = 0;
        }

        if (! instruction.IsPrefix && _state.InstructionPrefix == 0 && ! _state.IgnoreNextInterrupt)
        {
            HandleInterrupts();
        }
    }

    public void AddHook(IProcessorHook hook)
    {
        _hooks.Add(hook);
    }

    private void UpdateR(Instruction instruction)
    {
        if (instruction.IsPrefix)
        {
            return;
        }

        var increment = 1;

        if (instruction.OpCode > 0xFF)
        {
            increment = 2;
        }

        var value = (byte) (_state[Register.R] & 0x7F);

        var topBit = _state[Register.R] & 0x80;

        value = (byte) (value + increment);

        _state[Register.R] = value;

        if (topBit > 0)
        {
            _state[Register.R] |= 0x80;
        }
        else
        {
            _state[Register.R] &= 0x7F;
        }
    }

    private void HandleInterrupts()
    {
        if (_interface.INT)
        {
            HandleMaskableInterrupt();
        }
    }

    private void HandleMaskableInterrupt()
    {
        _state.Halted = false;

        if (! _state.InterruptFlipFlop1)
        {
            return;
        }

        _state.InterruptFlipFlop1 = _state.InterruptFlipFlop2 = false;

        switch (_state.InterruptMode)
        {
            case InterruptMode.IM0:
                // TODO: Get instruction opcode from bus.
                PushProgramCounter();

                _state.ProgramCounter = 0x38;

                _state.Q = 0;

                break;

            case InterruptMode.IM1:
                PushProgramCounter();

                _state.ProgramCounter = 0x0038;

                break;

            case InterruptMode.IM2:
                PushProgramCounter();

                // TODO: Get 0xFF from bus.
                var address = (ushort) ((_state[Register.I] << 8) | 0xFF);

                _state.ProgramCounter = (ushort) (_interface.ReadFromMemory(address) | (_interface.ReadFromMemory((ushort) (address + 1)) << 8));

                break;
        }
    }

    private void PushProgramCounter()
    {
        _state.StackPointer--;

        _interface.WriteToMemory(_state.StackPointer, (byte) ((_state.ProgramCounter & 0xFF00) >> 8));

        _state.StackPointer--;

        _interface.WriteToMemory(_state.StackPointer, (byte) (_state.ProgramCounter & 0x00FFFF));
    }
}