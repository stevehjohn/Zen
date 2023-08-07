using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Zen.Common;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class MenuSystem : CharacterOverlayBase
{
    private const int SelectionFrameDelay = 24;

    private readonly Action<MenuResult, object> _menuFinished;

    private readonly Color[] _backgroundData = new Color[Constants.ScreenWidthPixels * Constants.ScreenHeightPixels];

    private readonly Color[] _menuData = new Color[Constants.ScreenWidthPixels * Constants.ScreenHeightPixels];
    
    private int _selectedItem = -1;

    private int _selectionDelay = SelectionFrameDelay;

    private MenuBase _menu;

    private FileSelect _fileSelect;

    public MenuSystem(Texture2D background, GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, Action<MenuResult, object> menuFinished)
        : base(background, graphicsDeviceManager, contentManager)
    {
        _menuFinished = menuFinished;

        _menu = new MainMenu();
        
        Background.GetData(_backgroundData);
        
        Menu = new Texture2D(GraphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.ScreenHeightPixels);
    }

    public void Update()
    {
        Texture2D menu = null;

        if (_fileSelect != null)
        {
            _fileSelect.Update();

            if (_fileSelect != null)
            {
                menu = _fileSelect.Menu;
            }
        }

        if (menu == null)
        {
            DrawMenu();

            UpdateTextAnimation();

            CheckSelection();

            ActionSelection();
        }
        else
        {
            Menu = menu;
        }
    }

    private void ActionSelection()
    {
        if (_selectedItem == -1)
        {
            return;
        }

        _selectionDelay--;

        if (_selectionDelay > 0)
        {
            return;
        }

        var result = _menu.ItemSelected(_selectedItem);

        switch (result.Result)
        {
            case MenuResult.NewMenu:
                _menu = result.NewMenu;

                _selectedItem = -1;

                _selectionDelay = SelectionFrameDelay;

                break;

            case MenuResult.LoadZ80Sna:
                _fileSelect = new FileSelect(Background, GraphicsDeviceManager, ContentManager, FileSelectDone);

                _selectedItem = -1;

                _selectionDelay = SelectionFrameDelay;

                break;

            default:
                _menuFinished(result.Result, result.Arguments);

                break;
        }
    }

    private void FileSelectDone(string path)
    {
        _fileSelect = null;

        if (! string.IsNullOrWhiteSpace(path))
        {
            _menuFinished(MenuResult.LoadZ80Sna, path);
        }
    }

    private void CheckSelection()
    {
        if (_selectedItem > -1)
        {
            return;
        }

        var keys = Keyboard.GetState();

        foreach (var item in _menu.GetMenu())
        {
            if (item.Id == 0 || ! item.SelectKey.HasValue)
            {
                continue;
            }

            if (keys.IsKeyDown(item.SelectKey!.Value))
            {
                _selectedItem = item.Id;

                break;
            }
        }
    }

    private void DrawMenu()
    {
        Array.Copy(_backgroundData, _menuData, _backgroundData.Length);
        
        DrawWindow(_menuData);

        var items = _menu.GetMenu();

        foreach (var item in items)
        {
            DrawString(_menuData, item.Text, item.X, item.Y, _selectedItem == item.Id ? item.SelectedColor!.Value : item.Color, item.Centered, _selectedItem == item.Id);
        }

        Menu.SetData(_menuData);
    }
}