# Notes

ToDos, aide memoires, etc...

- For CB, DD & FD instructions add 4 extra t-states when it is a duplicate of a base instruction.
- Come up with good notation in method names for 'address of'.
- Try and be consistent with parameter names... source, target etc...
- Helper method for memory read/writes to ensure TransferType/Mreq is correct.
- Test exact timings on JSMoo tests.

## JSMoo Tests Failing

None.

## JSMoo Tests Failing - Including Undocumented

None.

WARNs:

DD 86 0000: ADD A, (IX + d)
DD 8E 0000: ADC A, (IX + d)
DD 96 0000: SUB A, (IX + d)
DD 9E 0000: SBC A, (IX + d)
DD A6 0000: AND A, (IX + d)
DD AE 0000: XOR A, (IX + d)
DD B6 0000: OR A, (IX + d)
DD BE 0000: CP A, (IX + d)
FD 86 0000: ADD A, (IY + d)
FD 8E 0000: ADC A, (IY + d)
FD 96 0000: SUB A, (IY + d)
FD 9E 0000: SBC A, (IY + d)
FD A6 0000: AND A, (IY + d)
FD AE 0000: XOR A, (IY + d)
FD B6 0000: OR A, (IY + d)
FD BE 0000: CP A, (IY + d)
