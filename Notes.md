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

DD CB __ 18 0001: RR (IX + d), B
DD CB __ 19 0000: RR (IX + d), C
DD CB __ 1A 0002: RR (IX + d), D
DD CB __ 1B 0000: RR (IX + d), E
DD CB __ 1C 0000: RR (IX + d), H
DD CB __ 1D 0000: RR (IX + d), L
DD CB __ 1F 0000: RR (IX + d), A
DD CB __ 20 0000: SLA (IX + d), B
DD CB __ 21 0000: SLA (IX + d), C
DD CB __ 22 0000: SLA (IX + d), D
DD CB __ 23 0000: SLA (IX + d), E
DD CB __ 24 0000: SLA (IX + d), H
DD CB __ 25 0001: SLA (IX + d), L
DD CB __ 26 0002: SLA (IX + d)
DD CB __ 27 0000: SLA (IX + d), A
DD CB __ 28 0000: SRA (IX + d), B
DD CB __ 29 0000: SRA (IX + d), C
DD CB __ 2A 0000: SRA (IX + d), D
DD CB __ 2B 0000: SRA (IX + d), E
DD CB __ 2C 0000: SRA (IX + d), H
DD CB __ 2D 0001: SRA (IX + d), L
DD CB __ 2E 0000: SRA (IX + d)
DD CB __ 2F 0000: SRA (IX + d), A
DD CB __ 30 0000: SLL (IX + d), B
DD CB __ 31 0000: SLL (IX + d), C
DD CB __ 32 0000: SLL (IX + d), D
DD CB __ 33 0000: SLL (IX + d), E
DD CB __ 34 0000: SLL (IX + d), H
DD CB __ 35 0000: SLL (IX + d), L
DD CB __ 36 0000: SLL (IX + d)
DD CB __ 37 0000: SLL (IX + d), A
DD CB __ 38 0000: SRL (IX + d), B
DD CB __ 39 0000: SRL (IX + d), C
DD CB __ 3A 0000: SRL (IX + d), D
DD CB __ 3B 0002: SRL (IX + d), E
DD CB __ 3C 0000: SRL (IX + d), H
DD CB __ 3D 0000: SRL (IX + d), L
DD CB __ 3E 0000: SRL (IX + d)
DD CB __ 3F 0001: SRL (IX + d), A
