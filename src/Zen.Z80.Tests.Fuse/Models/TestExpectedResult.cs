namespace Zen.Z80.Tests.Fuse.Models;

public class TestExpectedResult
{
    public ProcessorState ProcessorState { get; }

    public TestExpectedResult(string[] testData)
    {
        var line = 1;

        while (char.IsWhiteSpace(testData[line][0]))
        {
            line++;
        }

        ProcessorState = new ProcessorState(testData[line..]);
    }
}