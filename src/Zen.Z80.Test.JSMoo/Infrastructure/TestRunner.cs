#define UNATTENDED
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Zen.Z80.Exceptions;
using Zen.Z80.Processor;
using Zen.Z80.Test.JSMoo.Models;
using ZXE.Common.ConsoleHelpers;

namespace Zen.Z80.Test.JSMoo.Infrastructure;

[ExcludeFromCodeCoverage]
public class TestRunner
{
    public static void RunTests()
    {
        var files = Directory.EnumerateFiles("TestDefinitions", "*.json");

        var total = 0;

        var passed = 0;

        var notImplemented = 0;

        Console.CursorVisible = false;

        var stopwatch = Stopwatch.StartNew();

        var failedNames = new List<string>();

        var notImplementedNames = new List<string>();

        foreach (var file in files)
        {
            if (Path.GetFileName(file).CompareTo("dd cb ") < 0)
            {
                continue;
            }

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

                        break;

                    case TestResult.Fail:
                        failedNames.Add($"{test.Name}: {result.Mnemonic ?? "UNKNOWN"}");

                        // DumpTest(test);

#if UNATTENDED
                        skipRemainder = true;
#endif

                        break;

                    case TestResult.NotImplemented:
                        notImplemented++;

                        skipRemainder = true;

                        notImplementedNames.Add(test.Name);

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

        if (failedNames.Count > 0 || notImplementedNames.Count > 0)
        {
            FormattedConsole.WriteLine("  &Cyan;Press any key to see failed and not implemented test names...\n");

            Console.ReadKey();

            foreach (var name in failedNames)
            {
                FormattedConsole.Write($"&Red;{name}\n");
            }

            Console.WriteLine("\n");

            foreach (var name in notImplementedNames)
            {
                FormattedConsole.Write($"&Yellow;{name}\n");
            }

            Console.WriteLine();
        }

        Console.CursorVisible = true;
    }

    private static (TestResult Result, string? Mnemonic) RunTest(TestDefinition test)
    {
        var result = ExecuteTest(test);

        FormattedConsole.Write($"  &Cyan;Test&White;: &Magenta;{test.Name,-18}  ");

        FormattedConsole.Write($"  &Cyan;RAM&White;: &Magenta;{test.Initial.Ram.Length,3}B  ");

        FormattedConsole.Write($"  &Cyan;Operations&White;: &Magenta;{result.Operations,6}  ");

        FormattedConsole.Write("  &Cyan;Result&White;: [ ");

        var testResult = TestResult.Fail;

        if (result.Passed)
        {
            FormattedConsole.Write("&Green;PASS");

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

        FormattedConsole.Write("&White; ]");

        FormattedConsole.WriteLine(string.Empty);

        return (testResult, result.Mnemonic);
    }

    private static (bool Passed, int Operations, State State, Dictionary<int, byte> Ram, Exception? Exception, string? Mnemonic) ExecuteTest(TestDefinition test)
    {
        var ram = new Dictionary<int, byte>();

        var @interface = new Interface();

        var state = new State();

        var processor = new Core(@interface, state);

        foreach (var pair in test.Initial.Ram)
        {
            ram[pair[0]] = (byte) pair[1];
        }

        @interface.AddressChanged = i => i.Data = ram[i.Address];

        state.ProgramCounter = (ushort) test.Initial.PC;
        state.StackPointer = (ushort) test.Initial.SP;

        state[Register.A] = test.Initial.A;
        state[Register.B] = test.Initial.B;
        state[Register.C] = test.Initial.C;
        state[Register.D] = test.Initial.D;
        state[Register.E] = test.Initial.E;
        state[Register.F] = test.Initial.F;
        state[Register.H] = test.Initial.H;
        state[Register.L] = test.Initial.L;
        state[Register.A_] = test.Initial.A;
        state[Register.B_] = test.Initial.B;
        state[Register.C_] = test.Initial.C;
        state[Register.D_] = test.Initial.D;
        state[Register.E_] = test.Initial.E;
        state[Register.F_] = test.Initial.F;
        state[Register.H_] = test.Initial.H;
        state[Register.L_] = test.Initial.L;
        state[Register.I] = test.Initial.I;
        state[Register.R] = test.Initial.R;
        state[RegisterPair.IX] = test.Initial.IX;
        state[RegisterPair.IY] = test.Initial.IY;

        state.MemPtr = test.Initial.WZ;
        state.Q = test.Initial.Q;

        // TODO
        //state.InterruptFlipFlop1 = test.Initial.IFF1 > 0;
        //state.InterruptFlipFlop2 = test.Initial.IFF2 > 0;

        var operations = 0;

        string? firstMnemonic = null;

        try
        {
            var cycles = 0;

            while (true)
            {
                operations++;

                if (operations > 0xFFFF)
                {
                    break;
                }

                processor.ExecuteCycle();

                if (firstMnemonic == null && ! state.LastInstruction!.Mnemonic.StartsWith("PREFIX"))
                {
                    firstMnemonic = state.LastInstruction.Mnemonic;
                }

                if (state.ClockCycles >= (ulong) test.Cycles.Length)
                {
                    break;
                }
            }
        }
        catch (Exception exception)
        {
            return (false, operations, state, ram, exception, firstMnemonic);
        }

        var pass = state.ProgramCounter == test.Final.PC
                   && state.StackPointer == test.Final.SP
                   && state[Register.A] == test.Final.A
                   && state[Register.B] == test.Final.B
                   && state[Register.C] == test.Final.C
                   && state[Register.D] == test.Final.D
                   && state[Register.E] == test.Final.E
                   // TODO: Test undocumented flags.
                   && (state[Register.F] & 0b1101_0111) == (test.Final.F & 0b1101_0111)
                   && state[Register.H] == test.Final.H
                   && state[Register.L] == test.Final.L
                   && state[Register.I] == test.Final.I
                   && state[Register.R] == test.Final.R
                   && state[RegisterPair.IX] == test.Final.IX
                   && state[RegisterPair.IY] == test.Final.IY;
        // TODO: Alternate registers? Q, MemPtr etc...?

        foreach (var pair in test.Final.Ram)
        {
            pass = pass && ram[pair[0]] == pair[1];
        }

        if (! pass)
        {
        }

        return (pass, operations, state, ram, null, firstMnemonic);
    }
}