﻿// #define UNATTENDED
#define UNDOCUMENTED
// #define EXACT
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Zen.Common.ConsoleHelpers;
using Zen.Common.Extensions;
using Zen.Z80.Exceptions;
using Zen.Z80.Processor;
using Zen.Z80.Test.JSMoo.Models;

namespace Zen.Z80.Test.JSMoo.Infrastructure;

[ExcludeFromCodeCoverage]
public class TestRunner
{
    private readonly Interface _interface = new();

    private readonly State _state = new();

    private readonly Core _processor;

    public TestRunner()
    {
        _processor = new Core(_interface, _state);
    }

    public void RunTests()
    {
        var files = Directory.EnumerateFiles("TestDefinitions", "*.json");

        var total = 0;

        var passed = 0;

        var notImplemented = 0;

        Console.CursorVisible = false;

        var stopwatch = Stopwatch.StartNew();

        var failedNames = new List<string>();

        var warnNames = new List<string>();
        
        foreach (var file in files)
        {
            //if (Path.GetFileNameWithoutExtension(file).CompareTo("bf ") < 0)
            //{
            //    continue;
            //}

            //if (Path.GetFileNameWithoutExtension(file).StartsWith("dd cb __ 40"))
            //{
            //    break;
            //}

            var tests = JsonSerializer.Deserialize<TestDefinition[]>(File.ReadAllText(file), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (tests == null)
            {
                continue;
            }

            foreach (var test in tests)
            {
                total++;

                var skipRemainder = false;

                var result = RunTest(test);

                switch (result.Result)
                {
                    case TestResult.Pass:
                        passed++;

                        if (result.Warn)
                        {
                            warnNames.Add($"{test.Name}: {result.Mnemonic ?? "UNKNOWN"}");
                        }

                        skipRemainder = true;

                        break;

                    case TestResult.Fail:
                        failedNames.Add($"{test.Name}: {result.Mnemonic ?? "UNKNOWN"}");

                        DumpTest(test);

#if UNATTENDED
                        skipRemainder = true;
#endif

                        break;

                    case TestResult.NotImplemented:
                        notImplemented++;

                        skipRemainder = true;

                        break;
                }

                if (skipRemainder)
                {
                    break;
                }
            }
        }

        var failed = total - (passed + notImplemented);

        stopwatch.Stop();

        FormattedConsole.WriteLine(string.Empty);

        FormattedConsole.WriteLine($"  &Cyan;Testing complete. Time elapsed&White;: &Yellow;{stopwatch.Elapsed.Hours:D2}:{stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}.{stopwatch.Elapsed.Milliseconds}");

        FormattedConsole.WriteLine($"\n  &Cyan;Tests Run&White;: &Yellow;{total:N0}    &Cyan;Tests Passed&White;: &Green;{passed:N0}    &Cyan;Not Implemented&White;: &Yellow;{notImplemented}");

        FormattedConsole.WriteLine($"\n  &Cyan;Tests Failed&White;: {(failed == 0 ? "&Green;" : "&Red;")}{failed:N0}    &Cyan;Percent Failed&White;: &Yellow;{(float) failed / total * 100:F2}%");

        FormattedConsole.WriteLine(string.Empty);

        if (failedNames.Count > 0 || warnNames.Count > 0)
        {
            FormattedConsole.WriteLine("  &Cyan;Press any key to see warn/failed test names...\n");

            Console.ReadKey();

            foreach (var name in warnNames)
            {
                FormattedConsole.Write($"&DarkGreen;{name}\n");
            }

            Console.WriteLine();

            foreach (var name in failedNames)
            {
                FormattedConsole.Write($"&Red;{name}\n");
            }

            Console.WriteLine();
        }

        Console.CursorVisible = true;
    }

    private (TestResult Result, string? Mnemonic, bool Warn) RunTest(TestDefinition test)
    {
        var result = ExecuteTest(test);

        FormattedConsole.Write($"  &Cyan;Test&White;: &Magenta;{test.Name,-18}  ");

        FormattedConsole.Write($"  &Cyan;RAM&White;: &Magenta;{test.Initial.Ram.Length,3}B  ");

        FormattedConsole.Write($"  &Cyan;Operations&White;: &Magenta;{result.Operations,6}  ");

        FormattedConsole.Write("  &Cyan;Result&White;: [ ");

        var testResult = TestResult.Fail;

        if (result.Passed)
        {
            if (result.Warn)
            {
                FormattedConsole.Write("&DarkGreen;WARN");
            }
            else
            {
                FormattedConsole.Write("&Green;PASS");
            }

            testResult = TestResult.Pass;
        }
        else
        {
            if (result.Exception != null)
            {
                if (result.Exception is OpCodeNotFoundException)
                {
                    FormattedConsole.Write("&Yellow;NIMP");

                    testResult = TestResult.NotImplemented;
                }
                else
                {
                    FormattedConsole.Write("&Red;EXCP");
                }
            }
            else
            {
                FormattedConsole.Write("&Red;FAIL");
            }
        }

        FormattedConsole.Write("&White; ]  ");

        FormattedConsole.WriteLine($"  &Cyan;Mnemonic&White;: &Yellow;{result.Mnemonic ?? "N/A"}");

        return (testResult, result.Mnemonic, result.Warn);
    }

    // TODO: Does this need to return State and it's now a member?
    private (bool Passed, int Operations, State State, Dictionary<int, byte> Ram, Exception? Exception, string? Mnemonic, bool Warn) ExecuteTest(TestDefinition test)
    {
        _state.Reset();

        var ram = new Dictionary<int, byte>();

        foreach (var pair in test.Initial.Ram)
        {
            ram[pair[0]] = (byte) pair[1];
        }

        _interface.AddressChanged = i =>
        {
            if (i.TransferType == TransferType.Read)
            {
                i.Data = ram[i.Address];
            }
            else
            {
                ram[i.Address] = i.Data;
            }
        };

        _state.ProgramCounter = (ushort) test.Initial.PC;
        _state.StackPointer = (ushort) test.Initial.SP;

        _state[Register.A] = test.Initial.A;
        _state[Register.B] = test.Initial.B;
        _state[Register.C] = test.Initial.C;
        _state[Register.D] = test.Initial.D;
        _state[Register.E] = test.Initial.E;
        _state[Register.F] = test.Initial.F;
        _state[Register.H] = test.Initial.H;
        _state[Register.L] = test.Initial.L;
        _state[RegisterPair.AF_] = test.Initial.AF_;
        _state[RegisterPair.BC_] = test.Initial.BC_;
        _state[RegisterPair.DE_] = test.Initial.DE_;
        _state[RegisterPair.HL_] = test.Initial.HL_;
        _state[Register.I] = test.Initial.I;
        _state[Register.R] = test.Initial.R;
        _state[RegisterPair.IX] = test.Initial.IX;
        _state[RegisterPair.IY] = test.Initial.IY;

        _state.MemPtr = test.Initial.WZ;
        _state.Q = test.Initial.Q;

        _state.InterruptFlipFlop1 = test.Initial.IFF1 > 0;
        _state.InterruptFlipFlop2 = test.Initial.IFF2 > 0;

        var operations = 0;

        string? firstMnemonic = null;

        try
        {
            while (true)
            {
                operations++;

                if (operations > 0xFFFF)
                {
                    break;
                }

                _processor.ExecuteCycle();

                if (firstMnemonic == null && ! _state.LastInstruction!.Mnemonic.StartsWith("PREFIX"))
                {
                    firstMnemonic = _state.LastInstruction.Mnemonic;
                }

                if (_state.ClockCycles >= (ulong) test.Cycles.Length)
                {
                    break;
                }
            }
        }
        catch (Exception exception)
        {
            if (firstMnemonic == null && _state.LastInstruction != null && ! _state.LastInstruction.Mnemonic.StartsWith("PREFIX"))
            {
                firstMnemonic = _state.LastInstruction.Mnemonic;
            }

            return (false, operations, _state, ram, exception, firstMnemonic, false);
        }

        var warn = _state.ClockCycles != (ulong) test.Cycles.Length;

        var pass = _state.ProgramCounter == test.Final.PC
                   && _state.StackPointer == test.Final.SP
                   && _state[Register.A] == test.Final.A
                   && _state[Register.B] == test.Final.B
                   && _state[Register.C] == test.Final.C
                   && _state[Register.D] == test.Final.D
                   && _state[Register.E] == test.Final.E
                   && (_state[Register.F] & 0b1101_0111) == (test.Final.F & 0b1101_0111)
                   && _state[Register.H] == test.Final.H
                   && _state[Register.L] == test.Final.L
                   && _state[Register.I] == test.Final.I
                   && _state[Register.R] == test.Final.R
                   && _state[RegisterPair.IX] == test.Final.IX
                   && _state[RegisterPair.IY] == test.Final.IY
                   && _state[RegisterPair.AF_] == test.Final.AF_
                   && _state[RegisterPair.BC_] == test.Final.BC_
                   && _state[RegisterPair.DE_] == test.Final.DE_
                   && _state[RegisterPair.HL_] == test.Final.HL_;

        //pass &= _state.InterruptFlipFlop1 == test.Final.IFF1 > 0
        //        && _state.InterruptFlipFlop2 == test.Final.IFF2 > 0;

#if UNDOCUMENTED
        pass &= _state[Register.F] == test.Final.F && _state.Q == test.Final.Q;
#endif

#if EXACT
        pass &= _state[Register.F] == test.Final.F
                && _state.Q == test.Final.Q
                && _state.MemPtr == test.Final.WZ;
#endif

        foreach (var pair in test.Final.Ram)
        {
            pass = pass && ram[pair[0]] == pair[1];
        }

        return (pass, operations, _state, ram, null, firstMnemonic, warn);
    }

    private void DumpTest(TestDefinition test)
    {
        var result = ExecuteTest(test);

        if (result.Exception != null)
        {
            FormattedConsole.WriteLine($"\n    &Cyan;Exception&White;: &Red;{result.Exception.GetType().Name}");
        }

        FormattedConsole.WriteLine("\n&Cyan;        Initial       Expected      Actual");

        FormattedConsole.WriteLine($"    &Cyan;PC&White;: &Green;0x{test.Initial.PC:X4}        0x{test.Final.PC:X4}        {(test.Final.PC == result.State.ProgramCounter ? "&Green;" : "&Red;")}0x{result.State.ProgramCounter:X4}");
        FormattedConsole.WriteLine($"    &Cyan;SP&White;: &Green;0x{test.Initial.SP:X4}        0x{test.Final.SP:X4}        {(test.Final.SP == result.State.StackPointer ? "&Green;" : "&Red;")}0x{result.State.StackPointer:X4}");

        FormattedConsole.WriteLine($"    &Cyan;A &White;: &Green;0x{test.Initial.A:X2}          0x{test.Final.A:X2}          {(test.Final.A == result.State[Register.A] ? "&Green;" : "&Red;")}0x{result.State[Register.A]:X2}");
        FormattedConsole.WriteLine($"    &Cyan;B &White;: &Green;0x{test.Initial.B:X2}          0x{test.Final.B:X2}          {(test.Final.B == result.State[Register.B] ? "&Green;" : "&Red;")}0x{result.State[Register.B]:X2}");
        FormattedConsole.WriteLine($"    &Cyan;C &White;: &Green;0x{test.Initial.C:X2}          0x{test.Final.C:X2}          {(test.Final.C == result.State[Register.C] ? "&Green;" : "&Red;")}0x{result.State[Register.C]:X2}");
        FormattedConsole.WriteLine($"    &Cyan;D &White;: &Green;0x{test.Initial.D:X2}          0x{test.Final.D:X2}          {(test.Final.D == result.State[Register.D] ? "&Green;" : "&Red;")}0x{result.State[Register.D]:X2}");
        FormattedConsole.WriteLine($"    &Cyan;E &White;: &Green;0x{test.Initial.E:X2}          0x{test.Final.E:X2}          {(test.Final.E == result.State[Register.E] ? "&Green;" : "&Red;")}0x{result.State[Register.E]:X2}");
        FormattedConsole.WriteLine($"    &Cyan;F &White;: &Green;0x{test.Initial.F:X2}          0x{test.Final.F:X2}          {(test.Final.F == result.State[Register.F] ? "&Green;" : "&Red;")}0x{result.State[Register.F]:X2}");
        FormattedConsole.WriteLine($"    &Cyan;H &White;: &Green;0x{test.Initial.H:X2}          0x{test.Final.H:X2}          {(test.Final.H == result.State[Register.H] ? "&Green;" : "&Red;")}0x{result.State[Register.H]:X2}");
        FormattedConsole.WriteLine($"    &Cyan;L &White;: &Green;0x{test.Initial.L:X2}          0x{test.Final.L:X2}          {(test.Final.L == result.State[Register.L] ? "&Green;" : "&Red;")}0x{result.State[Register.L]:X2}");

        FormattedConsole.WriteLine($"    &Cyan;I &White;: &Green;0x{test.Initial.I:X2}          0x{test.Final.I:X2}          {(test.Final.I == result.State[Register.I] ? "&Green;" : "&Red;")}0x{result.State[Register.I]:X2}");
        FormattedConsole.WriteLine($"    &Cyan;R &White;: &Green;0x{test.Initial.R:X2}          0x{test.Final.R:X2}          {(test.Final.R == result.State[Register.R] ? "&Green;" : "&Red;")}0x{result.State[Register.R]:X2}");

        FormattedConsole.WriteLine($"    &Cyan;Q &White;: &Green;0x{test.Initial.Q:X2}          0x{test.Final.Q:X2}          {(test.Final.Q == result.State.Q ? "&Green;" : "&Red;")}0x{result.State.Q:X2}");

        FormattedConsole.WriteLine($"    &Cyan;IX&White;: &Green;0x{test.Initial.IX:X4}        0x{test.Final.IX:X4}        {(test.Final.IX == result.State[RegisterPair.IX] ? "&Green;" : "&Red;")}0x{result.State[RegisterPair.IX]:X4}");
        FormattedConsole.WriteLine($"    &Cyan;IY&White;: &Green;0x{test.Initial.IY:X4}        0x{test.Final.IY:X4}        {(test.Final.IY == result.State[RegisterPair.IY] ? "&Green;" : "&Red;")}0x{result.State[RegisterPair.IY]:X4}");

        var initialFlags = test.Initial.F.ToFlags();

        var expectedFlags = test.Final.F.ToFlags();

        FormattedConsole.WriteLine(
            $"\n    &Cyan;F &White;: &Green;{initialFlags}      &Cyan;F &White;: &Green;{expectedFlags}      {(test.Final.F == result.State[Register.F] ? "&Green;" : "&Red;")}{result.State[Register.F].ToFlags()}");

        FormattedConsole.WriteLine(string.Empty);

#if ! UNATTENDED
        FormattedConsole.WriteLine("\n    &Cyan;Press any key to continue...\n");

        Console.ReadKey();
#endif
    }
}