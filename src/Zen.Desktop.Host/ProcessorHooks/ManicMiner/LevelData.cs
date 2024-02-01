using System;
using System.Collections.Generic;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class LevelData
{
    public MapCell[,] Map { get; } = new MapCell[32, 16];

    public List<(int X, int Y)> Keys { get; } = [];

    public (int X, int Y) Start { get; private set; }

    public (int X, int Y) End { get; private set; }
    
    private readonly int _level;

    private readonly Interface _interface;

    public LevelData(int level, Interface @interface)
    {
        _level = level;

        _interface = @interface;
        
        GetLevelData();
    }
    
    private void GetLevelData()
    {
        ParseMap();
        
        PlaceKeys();
        
        FindStart();
        
        FindEnd();
    }

    private void ParseMap()
    {
        var start = 0xB000 + 0x0400 * (_level - 1);

        for (var y = 0; y < 16; y++)
        {
            for (var x = 0; x < 32; x++)
            {
                var tile = ParseTile((ushort) (start + y * 32 + x));
                
                Console.Write((int) tile);
                
                Map[x, y] = tile;
            }
            
            Console.WriteLine();
        }
    }

    private void PlaceKeys()
    {
        Keys.Clear();

        var start = 0xB276 + 0x0400 * (_level - 1);

        for (var i = 0; i < 5; i++)
        {
            var lsb = _interface.ReadFromMemory((ushort) (start + i * 5));

            if (lsb < 0xFF)
            {
                var msb = _interface.ReadFromMemory((ushort) (start + 1 + i * 5));

                var location = (msb << 8) + lsb;

                Console.WriteLine($"{msb:X2} {lsb:X2} {location:X4}");
                
                location -= 0x5C00;

                var position = (location % 32, location / 32); 
                
                Keys.Add(position);
                
                Console.WriteLine(position);
            }
        }
    }

    private void FindStart()
    {
        var start = 0xB26C + 0x0400 * (_level - 1);
        
        var lsb = _interface.ReadFromMemory((ushort) start);

        var msb = _interface.ReadFromMemory((ushort) start);

        var location = (msb << 8) + lsb;

        // + 1 == where Willy's feet are
        Start = (location % 32, location / 32 + 1); 
    }

    private void FindEnd()
    {
        var start = 0xB2B0 + 0x0400 * (_level - 1);
        
        var lsb = _interface.ReadFromMemory((ushort) start);

        var msb = _interface.ReadFromMemory((ushort) start);

        var location = (msb << 8) + lsb;

        End = (location % 32, location / 32); 
    }

    private MapCell ParseTile(ushort location)
    {
        var data = _interface.ReadFromMemory(location);

        return data switch
        {
            0x42 => MapCell.Floor,
            0x02 => MapCell.Floor,
            0x16 => MapCell.Wall,
            0x04 => MapCell.Floor,
            0x44 => MapCell.Hazard,
            0x05 => MapCell.Hazard,
            _ => MapCell.Empty
        };
    }
}