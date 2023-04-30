namespace Zen.Z80.Tests.Fuse.Models;

public class TestExpectedResult
{
    public TestResultStep[] Steps { get; }

    public ProcessorState ProcessorState { get; }

    public TestExpectedResult(string[] testData)
    {
        var steps = new List<TestResultStep>();

        var line = 1;

        while (char.IsWhiteSpace(testData[line][0]))
        {
            steps.Add(new TestResultStep(testData[line]));

            line++;
        }

        Steps = steps.ToArray();

        ProcessorState = new ProcessorState(testData[line..]);
    }
}