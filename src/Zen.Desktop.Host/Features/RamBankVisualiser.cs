using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Zen.System.Modules;

namespace Zen.Desktop.Host.Features;

public class RamBankVisualiser
{
    private const int SideSize = 128;

    private const int Offset = 16;
    
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private readonly Color[] _data;

    private readonly Texture2D _visualisation;

    private Ram _ram;

    public Ram Ram
    {
        set => _ram = value;
    }

    public RamBankVisualiser(GraphicsDeviceManager graphicsDeviceManager, Ram ram)
    {
        _graphicsDeviceManager = graphicsDeviceManager;

        _ram = ram;

        _data = new Color[SideSize * SideSize];

        _visualisation = new Texture2D(_graphicsDeviceManager.GraphicsDevice, SideSize, SideSize);
    }

    public Texture2D RenderRam()
    {
        Array.Fill(_data, Color.Black);
        
        RenderBank();
        
        _visualisation.SetData(_data);

        return _visualisation;
    }

    private void RenderBank()
    {
        for (var y = 0; y < SideSize; y++)
        {
            for (var x = 0; x < SideSize / 8; x++)
            {
                var data = _ram.GetBank(3)[y * (SideSize / 8) + x];

                for (var i = 0; i < 8; i++)
                {
                    _data[Offset + y * (SideSize / 8) + x * 8 + i] = (data & 1 << (7 - i)) > 0 ? Color.LightGray : Color.Black;
                }
            }
        }
    }
}