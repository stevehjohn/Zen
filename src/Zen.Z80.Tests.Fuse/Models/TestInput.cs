using System.Globalization;

namespace Zen.Z80.Tests.Fuse.Models;

// ReSharper disable InconsistentNaming

public class TestInput
{
    public string Name { get; }

    public ProcessorState ProcessorState { get; }

    public static byte?[] Ram => _ram;

    private static readonly byte?[] _ram = new byte?[0xFFFF];

    public TestInput(string[] testData)
    {
        Name = testData[0];

        ProcessorState = new ProcessorState(testData[1..3]);

        for (var i = 0; i < 0xFFFF; i++)
        {
            _ram[i] = null;
        }

        var line = 3;

        while (testData[line] != "-1")
        {
            var parts = testData[line].Split(' ');

            var address = ushort.Parse(parts[0], NumberStyles.HexNumber);

            var part = 1;

            while (parts[part] != "-1")
            {
                Ram[address] = byte.Parse(parts[part], NumberStyles.HexNumber);

                address++;

                part++;
            }

            line++;
        }
    }
}