# Notes

ToDos, aide memoires, etc...

- https://discord.com/channels/654774470652723220/689220116801650811/885861410490433566

Tidy this up:

Host -> KeyboardMapper -> Interface.WriteToPort -> Motherboard.WritePort -> Ports -> Motherboard.PortDataChanged

## ZexDoc

All pass.

## JSMoo

76 0000: HALT
DB 0000: IN A, n
DD 76 0000: HALT
DD DB 0000: IN A, n
ED 40 0000: IN B, (C)
ED 48 0000: IN C, (C)
ED 50 0000: IN D, (C)
ED 58 0000: IN E, (C)
ED 60 0000: IN H, (C)
ED 68 0000: IN L, (C)
ED 70 0000: IN (C)
ED 78 0000: IN A, (C)
ED A2 0000: INI
ED A3 0000: OUTI
ED AA 0000: IND
ED AB 0000: OUTD
ED B2 0000: INIR
ED B3 0000: OTIR
ED BA 0000: INDR
FD 76 0000: HALT
FD DB 0000: IN A, n

## Fuse

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
