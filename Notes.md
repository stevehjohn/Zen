# Notes

ToDos, aide memoires, etc...

- Try and be consistent with parameter names... source, target etc...
- https://discord.com/channels/654774470652723220/689220116801650811/885861410490433566
- Sort out flashing.

## JSMoo Tests Failing

76 0000: HALT
DD 76 0000: HALT
ED A2 0157: INI
ED A3 0000: OUTI
ED AA 0000: IND
ED AB 0000: OUTD
ED B2 0000: INIR
ED B3 0000: OTIR
ED BA 0000: INDR
ED BB 0002: OTDR
FD 76 0000: HALT

## Raxoft Tests Failing

ADC HL, RR
LDIR->NOP'
LDDR->NOP'


## JSMoo Tests Failing - Including Undocumented

TODO.

## Woody Flags Tests Failing

ADC16
SBC16
RLD/RRD

## Fuse Tests Failing

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
eda2_02: INI
eda2_03: INI
eda3_02: OUTI
eda3_04: OUTI
eda3_06: OUTI
eda3_08: OUTI
edaa_01: IND
edaa_02: IND
edab: OUTD
edab_01: OUTD
edb2: INIR
edb3: OTIR
edba: INDR
edbb: OTDR