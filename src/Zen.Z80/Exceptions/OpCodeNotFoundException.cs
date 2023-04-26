namespace Zen.Z80.Exceptions;

public class OpCodeNotFoundException : Exception
{
    public OpCodeNotFoundException(string message) : base(message)
    {
    }
}