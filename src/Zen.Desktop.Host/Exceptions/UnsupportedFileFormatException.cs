using System;

namespace Zen.Desktop.Host.Exceptions;

public class UnsupportedFileFormatException : Exception
{
    public UnsupportedFileFormatException(string extension) : base($"Extension {extension} is not supported.")
    {
    }
}