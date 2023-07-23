# Notes

ToDos, aide memoires, etc...

- MemPtr stuff.
- https://softspectrum48.weebly.com/notes/category/ay389108912
- Pause sound when menu shown.

## Useful References

- https://clrhome.org/table/
- http://ped.7gods.org/Z80N_table_ClrHome.html
- https://github.com/Agaxia/Z80Plus/blob/master/sources/Z80Plus.cpp
- https://github.com/raddad772/jsmoo/tree/main/misc/tests/GeneratedTests/z80
- https://github.com/redcode/Z80/wiki/Technical-literature
- https://github.com/redcode/ZXSpectrum/wiki
- https://github.com/redcode/Z80/wiki/Z80-Block-Flags-Test
- https://github.com/redcode/Z80/wiki/Tests
- https://discord.com/channels/654774470652723220/689220116801650811/885861410490433566
- https://github.com/hoglet67/Z80Decoder/wiki/Undocumented-Flags
- https://rk.nvg.ntnu.no/sinclair/faq/tech_z80.html#UNDOC

## Failing Tests

### ZexAll 2

CPD [R]
CPI [R]
LDD [R] (1)
LDI [R] (2)

### Z80 Flags

ADC16
SBC16
BIT n, (HL)
LDI
LDD
LDDR
CPI
CPD
IND
OUTI
OUTD

### ZexDoc

All pass.

### JSMoo

76 0000: HALT
DD 76 0000: HALT
ED A1 0011: CPI
ED A2 0157: INI
ED A3 0000: OUTI
ED AA 0000: IND
ED B1 0002: CPIR
ED B2 0000: INIR
ED B3 0000: OTIR
ED BA 0000: INDR
ED BB 0000: OTDR
FD 76 0000: HALT

### Fuse

76: HALT
db_1: IN A, n
db_2: IN A, n
db_3: IN A, n
db: IN A, n
ddfd00: NOP
ed40: IN B, (C)
ed48: IN C, (C)
ed50: IN D, (C)
ed58: IN E, (C)
ed60: IN H, (C)
ed68: IN L, (C)
ed70: IN (C)
ed78: IN A, (C)
eda2: INI
eda2_01: INI
eda3_02: OUTI
eda3_04: OUTI
eda3_06: OUTI
eda3_08: OUTI
edaa_01: IND
edaa_02: IND
edb1: CPIR
edb2: INIR
edb3: OTIR
edba: INDR
