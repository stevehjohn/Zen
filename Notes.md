# Notes

ToDos, aide memoires, etc...

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

### Z80 Documented

ADC HL, RR
LDIR -> NOP'
LDDR -> NOP'
INI
IND
INIR -> NOP'
INDR -> NOP'

### JSMoo

76 0000: HALT
DD 76 0000: HALT
ED A2 0157: INI
ED AA 0000: IND
ED B2 0000: INIR
ED B3 0000: OTIR
ED BA 0000: INDR
ED BB 0000: OTDR
FD 76 0000: HALT

### Fuse

76: HALT
ddfd00: NOP
ed4a: ADC HL, BC
ed5a: ADC HL, DE
ed6a: ADC HL, HL
ed7a: ADC HL, SP
edaa_01: IND
edaa_02: IND
edb2: INIR
edba: INDR
