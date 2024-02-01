using System;
using System.Collections.Generic;
using Zen.Z80.Processor;

namespace Zen.Desktop.Host.ProcessorHooks.ManicMiner;

public class RoutePlanner
{
    private readonly MapCell[,] _map = new MapCell[32, 16];

    private readonly List<(int X, int Y)> _keys = [];

    private (int X, int Y) _start;

    private readonly int _level;

    private readonly Interface _interface;

    public RoutePlanner(int level, Interface @interface)
    {
        _level = level;

        _interface = @interface;
    }
    
    public void PlanRoute()
    {
        ParseMap();
        
        PlaceKeys();
        
        FindStart();
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
                
                _map[x, y] = tile;
            }
            
            Console.WriteLine();
        }
    }

    private void PlaceKeys()
    {
        _keys.Clear();

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
                
                _keys.Add(position);
                
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

        _start = (location % 32, location / 32); 
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