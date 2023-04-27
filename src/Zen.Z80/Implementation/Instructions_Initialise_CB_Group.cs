// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

using Zen.Z80.Processor;

#pragma warning disable CS8509

namespace Zen.Z80.Implementation;

public partial class Instructions
{
    private void InitialiseCBGroupInstructions()
    {
        //var groups = new ushort[] { 0xCB, 0xDDCB, 0xFDCB };

        //foreach (var group in groups)
        //{
        //    for (var index = 0; index < 8; index++)
        //    {
        //        if (index != 6)
        //        {
        //            var register = index switch
        //            {
        //                0 => Register.B,
        //                1 => Register.C,
        //                2 => Register.D,
        //                3 => Register.E,
        //                4 => Register.H,
        //                5 => Register.L,
        //                7 => Register.A
        //            };

        //            if (group == 0xCB)
        //            {
        //                InitialiseCBRInstructions(index, register);
        //            }
        //            else
        //            {
        //                _instructions.Add(group + index, new Instruction(_ => RLC_R(register), $"RLC {register}", group + index, 0));
        //            }
        //        }
        //    }
        //}
    }
}