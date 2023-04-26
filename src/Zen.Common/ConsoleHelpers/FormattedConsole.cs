using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ZXE.Common.ConsoleHelpers;

[ExcludeFromCodeCoverage]
public static class FormattedConsole
{
    public static void Write(string output)
    {
        var builder = new StringBuilder();

        for (var i = 0; i < output.Length; i++)
        {
            if (output[i] == '&')
            {
                var end = output.IndexOf(';', i);

                if (end > 0)
                {
                    Console.Write(builder);

                    builder.Clear();

                    var colour = output[(i + 1)..end];

                    var colourEnum = Enum.Parse<ConsoleColor>(colour);

                    Console.ForegroundColor = colourEnum;

                    i = end;

                    continue;
                }
            }

            builder.Append(output[i]);
        }

        Console.Write(builder);
    }

    public static void WriteLine(string output)
    {
        Write($"{output}\n");
    }
}