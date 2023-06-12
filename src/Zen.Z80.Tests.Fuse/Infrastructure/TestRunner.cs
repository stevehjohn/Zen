#define UNATTENDED

using Moq;
using System.Diagnostics;
using Zen.Common.ConsoleHelpers;
using Zen.Common.Extensions;
using Zen.Z80.Exceptions;
using Zen.Z80.Interfaces;
using Zen.Z80.Processor;
using Zen.Z80.Tests.Fuse.Models;

namespace Zen.Z80.Tests.Fuse.Infrastructure;

public class TestRunner
{
    private readonly Mock<IPortConnector> _portConnector;

    private readonly Mock<IRamConnector> _ramConnector;

    private readonly Interface _interface;

    private readonly State _state;

    public TestRunner()
    {
        _portConnector = new Mock<IPortConnector>();

        _ramConnector = new Mock<IRamConnector>();

        _interface = new Interface(_portConnector.Object, _ramConnector.Object);

        _state = new();
    }

    public void RunTests()
    {
        var input = File.ReadAllLines(Path.Combine("TestDefinitions", "input.fuse"));

        var test = new List<string>();

        var testCount = 0;

        var passed = 0;

        var stopwatch = Stopwatch.StartNew();

        Console.CursorVisible = false;

        var failedTestNames = new List<string>();

        for (var i = 0; i < input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(input[i]))
            {
                if (test.Count > 0)
                {
                    testCount++;

                    var testInput = new TestInput(test.ToArray());

                    var result = RunTest(testInput);

                    if (result.Passed)
                    {
                        passed++;
                    }
                    else
                    {
                        failedTestNames.Add($"{testInput.Name}: {result.Instruction}");
                    }

                    test.Clear();
                }

                continue;
            }

            test.Add(input[i]);
        }

        FormattedConsole.WriteLine($"\n  &Cyan;Testing complete&White;. &Cyan;Elapsed&White;: &Yellow;{stopwatch.Elapsed.Minutes:D2}:{stopwatch.Elapsed.Seconds:D2}.{stopwatch.Elapsed.Milliseconds:D3}\n");

        FormattedConsole.WriteLine($"  &Cyan;Executed&White;: &Yellow;{testCount:N0}    &Cyan;Passed&White;: &Green;{passed:N0}   &Cyan;Failed&White;: &Red;{testCount - passed:N0}");

        if (failedTestNames.Count > 0)
        {
            FormattedConsole.WriteLine("\n  &Cyan;Press any key to see failed test names...\n");

            Console.ReadKey();

            foreach (var name in failedTestNames)
            {
                FormattedConsole.WriteLine($"&Red;{name}");
            }

            Console.WriteLine("\n");
        }

        FormattedConsole.WriteLine("&Green;");

        Console.CursorVisible = true;
    }

    private (bool Passed, string Instruction) RunTest(TestInput input)
    {
        FormattedConsole.Write($"\n  &Cyan;Test&White;: &Yellow;{input.Name}");

        var processor = new Core(_interface, _state);

        SetProcessorState(input.ProcessorState);

        var ram = new Dictionary<int, byte>();

        PopulateRam(ram, input);
        
        _ramConnector.Setup(c => c.ReadRam(It.IsAny<ushort>())).Returns<ushort>(address => ram[address]);

        _ramConnector.Setup(c => c.WriteRam(It.IsAny<ushort>(), It.IsAny<byte>())).Callback<ushort, byte>((a, b) => ram[a] = b);

        _portConnector.Setup(c => c.CpuPortRead(It.IsAny<ushort>())).Returns<ushort>(p => (byte) (((p & 0xFF) * 2) & 0xFF));

        var tStates = 0;

        var expectedResult = LoadExpectedResult(input.Name);

        // https://github.com/floooh/chips-test/issues/27#issuecomment-1451670089
        //if (instructionIndex == 0xDB)
        //{
        //    ports.WriteByte((ushort) ((_state[Register.A] << 8) + ram[1]), _state[Register.A]);
        //}

        try
        {
            while (tStates < expectedResult.ProcessorState.TStates)
            {
                processor.ExecuteCycle();

                tStates += (int) _state.ClockCycles;
            }
        }
        catch (OpCodeNotFoundException)
        {
            FormattedConsole.WriteLine("NOT IMPLEMENTED");
        }
        catch (Exception exception)
        {
            FormattedConsole.WriteLine(exception.Message);
        }

        return (OutputResult(expectedResult), _state.LastInstruction?.Mnemonic ?? "UNKNOWN");
    }

    private bool OutputResult(TestExpectedResult expectedResult)
    {
        if ((_state[RegisterPair.AF] & 0b1111_1111_1101_0111) == (expectedResult.ProcessorState.AF & 0b1111_1111_1101_0111)
            && _state[RegisterPair.BC] == expectedResult.ProcessorState.BC
            && _state[RegisterPair.DE] == expectedResult.ProcessorState.DE
            && _state[RegisterPair.HL] == expectedResult.ProcessorState.HL
            && _state[RegisterPair.AF_] == expectedResult.ProcessorState.AF_
            && _state[RegisterPair.BC_] == expectedResult.ProcessorState.BC_
            && _state[RegisterPair.DE_] == expectedResult.ProcessorState.DE_
            && _state[RegisterPair.HL_] == expectedResult.ProcessorState.HL_
            && _state[RegisterPair.IX] == expectedResult.ProcessorState.IX
            && _state[RegisterPair.IY] == expectedResult.ProcessorState.IY
            && _state.ProgramCounter == expectedResult.ProcessorState.PC
            && _state.StackPointer == expectedResult.ProcessorState.SP
            && _state[Register.I] == expectedResult.ProcessorState.I
            && _state[Register.R] == expectedResult.ProcessorState.R)
            // TODO: IFFs, mode and HALT
        {
            FormattedConsole.WriteLine(" &White;[ &Green;PASS&White; ]");

            return true;
        }

        FormattedConsole.WriteLine(" &White;[ &Red;FAIL&White; ]");

        FormattedConsole.WriteLine(string.Empty);

        FormattedConsole.WriteLine("         &Cyan;Expected         Actual");

        FormattedConsole.WriteLine(
            $"    &Cyan;AF &White;: &Green;0x{expectedResult.ProcessorState.AF:X4}      &Cyan;AF &White;: {(expectedResult.ProcessorState.AF == _state[RegisterPair.AF] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.AF]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;BC &White;: &Green;0x{expectedResult.ProcessorState.BC:X4}      &Cyan;BC &White;: {(expectedResult.ProcessorState.BC == _state[RegisterPair.BC] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.BC]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;DE &White;: &Green;0x{expectedResult.ProcessorState.DE:X4}      &Cyan;DE &White;: {(expectedResult.ProcessorState.DE == _state[RegisterPair.DE] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.DE]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;HL &White;: &Green;0x{expectedResult.ProcessorState.HL:X4}      &Cyan;HL &White;: {(expectedResult.ProcessorState.HL == _state[RegisterPair.HL] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.HL]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;AF'&White;: &Green;0x{expectedResult.ProcessorState.AF_:X4}      &Cyan;AF'&White;: {(expectedResult.ProcessorState.AF_ == _state[RegisterPair.AF_] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.AF_]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;BC'&White;: &Green;0x{expectedResult.ProcessorState.BC_:X4}      &Cyan;BC'&White;: {(expectedResult.ProcessorState.BC_ == _state[RegisterPair.BC_] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.BC_]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;DE'&White;: &Green;0x{expectedResult.ProcessorState.DE_:X4}      &Cyan;DE'&White;: {(expectedResult.ProcessorState.DE_ == _state[RegisterPair.DE_] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.DE_]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;HL'&White;: &Green;0x{expectedResult.ProcessorState.HL_:X4}      &Cyan;HL'&White;: {(expectedResult.ProcessorState.HL_ == _state[RegisterPair.HL_] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.HL_]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;IX &White;: &Green;0x{expectedResult.ProcessorState.IX:X4}      &Cyan;IX &White;: {(expectedResult.ProcessorState.IX == _state[RegisterPair.IX] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.IX]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;IY &White;: &Green;0x{expectedResult.ProcessorState.IY:X4}      &Cyan;IY &White;: {(expectedResult.ProcessorState.IY == _state[RegisterPair.IY] ? "&Green;" : "&Red;")}0x{_state[RegisterPair.IY]:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;PC &White;: &Green;0x{expectedResult.ProcessorState.PC:X4}      &Cyan;PC &White;: {(expectedResult.ProcessorState.PC == _state.ProgramCounter ? "&Green;" : "&Red;")}0x{_state.ProgramCounter:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;SP &White;: &Green;0x{expectedResult.ProcessorState.SP:X4}      &Cyan;SP &White;: {(expectedResult.ProcessorState.SP == _state.StackPointer ? "&Green;" : "&Red;")}0x{_state.StackPointer:X4}");
        FormattedConsole.WriteLine(
            $"    &Cyan;I  &White;: &Green;0x{expectedResult.ProcessorState.I:X2}        &Cyan;I  &White;: {(expectedResult.ProcessorState.I == _state[Register.I] ? "&Green;" : "&Red;")}0x{_state[Register.I]:X2}");
        FormattedConsole.WriteLine(
            $"    &Cyan;R  &White;: &Green;0x{expectedResult.ProcessorState.R:X2}        &Cyan;R  &White;: {(expectedResult.ProcessorState.R == _state[Register.R] ? "&Green;" : "&Red;")}0x{_state[Register.R]:X2}");

        var expectedFlags = (byte) (expectedResult.ProcessorState.AF & 0xFF);

        FormattedConsole.WriteLine(
            $"\n    &Cyan;F  &White;: &Green;{expectedFlags.ToFlags()}    {((byte) (expectedResult.ProcessorState.AF & 0xFF) == _state[Register.F] ? "&Green;" : "&Red;")}{_state[Register.F].ToFlags()}");

        FormattedConsole.WriteLine(string.Empty);

        FormattedConsole.Write($"    &Cyan;IFF1&White;: {(expectedResult.ProcessorState.IFF1 == _state.InterruptFlipFlop1 ? "&Green;" : "&Red;")}{_state.InterruptFlipFlop1.ToString().ToLower()}");
        FormattedConsole.Write($"      &Cyan;IFF2&White;: {(expectedResult.ProcessorState.IFF2 == _state.InterruptFlipFlop2 ? "&Green;" : "&Red;")}{_state.InterruptFlipFlop2.ToString().ToLower()}");
        FormattedConsole.Write($"      &Cyan;Mode&White;: {(expectedResult.ProcessorState.InterruptMode == (int) _state.InterruptMode ? "&Green;" : "&Red;")}{(int) _state.InterruptMode}");
        //FormattedConsole.WriteLine($"      &Cyan;HALT&White;: {(expectedResult.ProcessorState.Halted == _state.Halted ? "&Green;" : "&Red;")}{processor.State.Halted.ToString().ToLower()}\n");

        // TODO: Verify RAM and Bus activity.

        //foreach (var line in tracer.GetTrace())//.Take(30))
        //{
        //    FormattedConsole.WriteLine($"      {line}");
        //}

        FormattedConsole.WriteLine(string.Empty);

#if ! UNATTENDED
        FormattedConsole.WriteLine("\n    &Cyan;Press any key to continue...\n");

        Console.ReadKey();
#endif

        return false;
    }

    private static TestExpectedResult LoadExpectedResult(string testName)
    {
        var results = File.ReadAllLines(Path.Combine("TestDefinitions", "expected.fuse"));

        var line = 0;

        int? startLine = null;

        while (line < results.Length)
        {
            if (results[line] == testName)
            {
                startLine = line;
            }

            while (! string.IsNullOrWhiteSpace(results[line]) && line < results.Length)
            {
                line++;
            }

            if (startLine.HasValue)
            {
                return new TestExpectedResult(results[startLine.Value..line]);
            }

            line++;
        }

        // TODO: Proper exception?
        throw new Exception($"Could not find test expected result for {testName}.");
    }

    private void SetProcessorState(ProcessorState processorState)
    {
        _state[RegisterPair.AF] = processorState.AF;

        _state[RegisterPair.BC] = processorState.BC;

        _state[RegisterPair.DE] = processorState.DE;

        _state[RegisterPair.HL] = processorState.HL;

        _state[RegisterPair.AF_] = processorState.AF_;

        _state[RegisterPair.BC_] = processorState.BC_;

        _state[RegisterPair.DE_] = processorState.DE_;

        _state[RegisterPair.HL_] = processorState.HL_;

        _state[RegisterPair.IX] = processorState.IX;

        _state[RegisterPair.IY] = processorState.IY;

        _state.ProgramCounter = processorState.PC;

        _state.StackPointer = processorState.SP;

        _state[Register.I] = processorState.I;

        _state[Register.R] = processorState.R;

        _state.InterruptFlipFlop1 = processorState.IFF1;

        _state.InterruptFlipFlop2 = processorState.IFF2;

        _state.InterruptMode = (InterruptMode) processorState.InterruptMode;

        _state.Halted = processorState.Halted;
    }

    private static void PopulateRam(Dictionary<int, byte> ram, TestInput input)
    {
        for (var i = 0; i < 0xFFFF; i++)
        {
            if (TestInput.Ram[i].HasValue)
            {
                ram[i] = TestInput.Ram[i]!.Value;
            }
        }
    }
}