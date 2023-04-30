namespace Zen.Z80.Tests.Fuse.Models;

public enum EventType
{
    MemoryRead,
    MemoryWrite,
    MemoryContend,
    PortRead,
    PortWrite,
    PortContend
}