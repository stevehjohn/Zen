// ReSharper disable InconsistentNaming
namespace Zen.Z80.Processor;

public enum RegisterPair
{
    IX  = 0x1213,
    IY  = 0x1415,
    AF =  0x0001,
    BC  = 0x0203,
    DE  = 0x0405,
    HL  = 0x0607,
    AF_ = 0x0809,
    BC_ = 0x0A0B,
    DE_ = 0x0C0D,
    HL_ = 0x0E0F
}