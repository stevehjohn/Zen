using System.Text;

namespace Zen.Utilities.Infrastructure;

public class OpCodeImplementationInspector
{
    private readonly string _initialisationCode = LoadInitialisationCode();

    private readonly string _methodCode = LoadMethodCode();

    public string GetOpCodeImplementation(int opCode)
    {
        var method = GetCalledMethod(opCode);

        var code = GetMethodCode(method);

        return code;
    }

    private static string LoadInitialisationCode()
    {
        var files = Directory.EnumerateFiles("../../../../../src/Zen.Z80/Implementation", "Instructions_Initialise_*");

        var data = new StringBuilder();

        foreach (var file in files)
        {
            data.Append(File.ReadAllText(file));

            data.AppendLine();
        }

        return data.ToString();
    }

    private static string LoadMethodCode()
    {
        var files = Directory.EnumerateFiles("../../../../../src/Zen.Z80/Implementation", "Instructions*");

        var data = new StringBuilder();

        foreach (var file in files)
        {
            if (! char.IsAsciiLetter(Path.GetFileName(file).Replace("Instructions", string.Empty)[0]))
            {
                continue;
            }

            data.Append(File.ReadAllText(file));

            data.AppendLine();
        }

        return data.ToString();
    }

    private string GetCalledMethod(int opCode)
    {
        var initialisationIndex = _initialisationCode.IndexOf($"0x{opCode:X6}", StringComparison.InvariantCultureIgnoreCase);

        var lambdaIndex = _initialisationCode.IndexOf("=>", initialisationIndex, StringComparison.InvariantCultureIgnoreCase);

        var methodNameEnd = _initialisationCode.IndexOf("(", lambdaIndex, StringComparison.InvariantCultureIgnoreCase);

        var methodString = _initialisationCode.Substring(lambdaIndex, methodNameEnd - lambdaIndex);

        methodString = methodString.Replace("=>", string.Empty);

        return methodString.Trim();
    }

    private string GetMethodCode(string methodName)
    {
        var index = _methodCode.IndexOf(methodName, StringComparison.InvariantCulture);

        var braceCount = -1;

        var code = new StringBuilder();
        
        while (braceCount != 0)
        {
            var character = _methodCode[index];

            if (character == '{')
            {
                if (braceCount == -1)
                {
                    braceCount = 1;
                }
                else
                {
                    braceCount++;
                }
            }
            else if (character == '}')
            {
                braceCount--;
            }

            code.Append(character);

            index++;
        }

        code.Insert(0, "    public void ");

        var lines = code.ToString().Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

        code.Clear();

        foreach (var line in lines)
        {
            if (line.StartsWith(' '))
            {
                code.AppendLine(line[4..]);
            }
            else
            {
                code.AppendLine(line);
            }
        }

        return code.ToString();
    }
}