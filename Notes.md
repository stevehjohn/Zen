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