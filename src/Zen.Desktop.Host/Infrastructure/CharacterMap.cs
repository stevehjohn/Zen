using System.Collections.Generic;

namespace Zen.Desktop.Host.Infrastructure;

public class CharacterMap
{
    private readonly Dictionary<char, int> _characterMap;

    public static CharacterMap Instance { get; } = new();

    private CharacterMap()
    {
        _characterMap = new Dictionary<char, int>();

        InitialiseCharacterMap();
    }

    public int GetCharacterStartPixel(char character)
    {
        if (_characterMap.TryGetValue(character, out var pixel))
        {
            return pixel;
        }

        return _characterMap['?'];
    }

    private void InitialiseCharacterMap()
    {
        _characterMap.Add(' ', 0);
        _characterMap.Add('!', 8);
        _characterMap.Add('"', 16);
        _characterMap.Add('#', 24);
        _characterMap.Add('$', 32);
        _characterMap.Add('%', 40);
        _characterMap.Add('&', 48);
        _characterMap.Add('\'', 56);
        _characterMap.Add('(', 64);
        _characterMap.Add(')', 72);
        _characterMap.Add('*', 80);
        _characterMap.Add('+', 88);
        _characterMap.Add(',', 96);
        _characterMap.Add('-', 104);
        _characterMap.Add('.', 112);
        _characterMap.Add('/', 120);

        _characterMap.Add('0', 1024);
        _characterMap.Add('1', 1032);
        _characterMap.Add('2', 1040);
        _characterMap.Add('3', 1048);
        _characterMap.Add('4', 1056);
        _characterMap.Add('5', 1064);
        _characterMap.Add('6', 1072);
        _characterMap.Add('7', 1080);
        _characterMap.Add('8', 1088);
        _characterMap.Add('9', 1096);
        _characterMap.Add(':', 1104);
        _characterMap.Add(';', 1112);
        _characterMap.Add('<', 1120);
        _characterMap.Add('=', 1128);
        _characterMap.Add('>', 1136);
        _characterMap.Add('?', 1144);

        _characterMap.Add('@', 2048);
        _characterMap.Add('A', 2056);
        _characterMap.Add('B', 2064);
        _characterMap.Add('C', 2072);
        _characterMap.Add('D', 2080);
        _characterMap.Add('E', 2088);
        _characterMap.Add('F', 2096);
        _characterMap.Add('G', 2104);
        _characterMap.Add('H', 2112);
        _characterMap.Add('I', 2120);
        _characterMap.Add('J', 2128);
        _characterMap.Add('K', 2136);
        _characterMap.Add('L', 2144);
        _characterMap.Add('M', 2152);
        _characterMap.Add('N', 2160);
        _characterMap.Add('O', 2168);

        _characterMap.Add('P', 3072);
        _characterMap.Add('Q', 3080);
        _characterMap.Add('R', 3088);
        _characterMap.Add('S', 3096);
        _characterMap.Add('T', 3104);
        _characterMap.Add('U', 3112);
        _characterMap.Add('V', 3120);
        _characterMap.Add('W', 3128);
        _characterMap.Add('X', 3136);
        _characterMap.Add('Y', 3144);
        _characterMap.Add('Z', 3152);
        _characterMap.Add('[', 3160);
        _characterMap.Add('\\', 3168);
        _characterMap.Add(']', 3176);
        _characterMap.Add('^', 3184);
        _characterMap.Add('_', 3192);

        _characterMap.Add('£', 4096);
        _characterMap.Add('a', 4104);
        _characterMap.Add('b', 4112);
        _characterMap.Add('c', 4120);
        _characterMap.Add('d', 4128);
        _characterMap.Add('e', 4136);
        _characterMap.Add('f', 4144);
        _characterMap.Add('g', 4152);
        _characterMap.Add('h', 4160);
        _characterMap.Add('i', 4168);
        _characterMap.Add('j', 4176);
        _characterMap.Add('k', 4184);
        _characterMap.Add('l', 4192);
        _characterMap.Add('m', 4200);
        _characterMap.Add('n', 4208);
        _characterMap.Add('o', 4216);

        _characterMap.Add('p', 5120);
        _characterMap.Add('q', 5128);
        _characterMap.Add('r', 5136);
        _characterMap.Add('s', 5144);
        _characterMap.Add('t', 5152);
        _characterMap.Add('u', 5160);
        _characterMap.Add('v', 5168);
        _characterMap.Add('w', 5176);
        _characterMap.Add('x', 5184);
        _characterMap.Add('y', 5192);
        _characterMap.Add('z', 5200);
        _characterMap.Add('{', 5208);
        _characterMap.Add('|', 5216);
        _characterMap.Add('}', 5224);
        _characterMap.Add('~', 5232);
        _characterMap.Add('©', 5240);
    }
}