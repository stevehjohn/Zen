# Notes

ToDos, aide memoires, etcetera.

## Useful Speccy References

- https://clrhome.org/table/
- http://ped.7gods.org/Z80N_table_ClrHome.html
- https://github.com/Agaxia/Z80Plus/blob/master/sources/Z80Plus.cpp
- https://github.com/raddad772/jsmoo/tree/main/misc/tests/GeneratedTests/z80
- https://discord.com/channels/654774470652723220/689220116801650811/885861410490433566
- https://github.com/hoglet67/Z80Decoder/wiki/Undocumented-Flags
- https://rk.nvg.ntnu.no/sinclair/faq/tech_z80.html#UNDOC

## Other References

- https://stackoverflow.com/questions/77384897/how-do-you-create-a-self-contained-net-core-app-for-macos-thats-x86-x64-arm6
- https://web.archive.org/web/20210420173215/https://docs.monogame.net/articles/packaging_games.html

## Failing Tests

### Raxoft Z80 Documented

Files: https://github.com/redcode/Z80/wiki/Zilog-Z80-CPU-Test-Suite
Explanation: https://github.com/raxoft/z80test

All registers, only documented flags.

```
LDIR -> NOP'
LDDR -> NOP'
INI
IND
INIR -> NOP'
INDR -> NOP'
```

### JSMoo

```
ED B2 0000: INIR
ED B3 0000: OTIR
ED BA 0000: INDR
ED BB 0000: OTDR
```

### Fuse

```
76: HALT
ddfd00: NOP
```