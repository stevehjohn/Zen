using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Zen.Common;
using Zen.Desktop.Host.Infrastructure.Settings;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class FileSelect : CharacterOverlayBase
{
    private const int SelectDelayFramesFast = 5;

    private const int SelectDelayFramesSlow = 10;

    private const int SelectDelayFramesVerySlow = 24;

    private const int FileRows = 16;

    private string _path;

    private readonly List<(string FullPath, string Display, bool IsDirectory)> _files = new();

    private readonly Color[] _backgroundData = new Color[Constants.ScreenWidthPixels * Constants.ScreenHeightPixels];

    private readonly Color[] _menuData = new Color[Constants.ScreenWidthPixels * Constants.ScreenHeightPixels];

    private int _selectDelay;

    private int _top;

    private int _y;

    private bool _cancelled;

    private bool _fileSelected;

    private bool _folderSelected;

    private readonly Action<string> _menuDone;

    public FileSelect(Texture2D background, GraphicsDeviceManager graphicsDeviceManager, ContentManager contentManager, Action<string> menuDone)
        : base(background, graphicsDeviceManager, contentManager)
    {
        var path = AppSettings.Instance.LastZ80SnaPath;

        if (string.IsNullOrWhiteSpace(path) || ! Path.Exists(path))
        {
            path = Directory.GetCurrentDirectory();
        }

        _path = path;

        GetFiles();

        _menuDone = menuDone;
        
        Background.GetData(_backgroundData);
        
        Menu = new Texture2D(GraphicsDeviceManager.GraphicsDevice, Constants.ScreenWidthPixels, Constants.ScreenHeightPixels);
    }

    public void Update()
    {
        DrawFileSelect();

        UpdateTextAnimation();

        if (! _cancelled && ! _fileSelected && ! _folderSelected)
        {
            CheckKeys();
        }
        else
        {
            _selectDelay--;

            if (_selectDelay == 0)
            {
                if (! _folderSelected)
                {
                    _menuDone(_cancelled ? null : _files[_top + _y].FullPath);
                }
                else
                {
                    _folderSelected = false;
                    
                    _top = 0;

                    _y = 0;

                    GetFiles();
                }
            }
        }
    }

    public void KeyTyped(char character)
    {
        var asString = character.ToString();
        
        var matches = _files.Where(f => f.Display.StartsWith(asString, StringComparison.InvariantCultureIgnoreCase)).ToList();

        if (matches.Count > 0)
        {
            var selectedName = _files[_top + _y];

            var selectedIndex = matches.IndexOf(selectedName);

            if (selectedIndex > -1)
            {
                selectedIndex++;

                if (selectedIndex >= matches.Count)
                {
                    selectedIndex = 0;
                }
            }
            else
            {
                selectedIndex = 0;
            }

            var target = matches[selectedIndex];

            while (_files[_top + _y].Display != target.Display)
            {
                _y++;

                if (_y >= FileRows)
                {
                    _y = FileRows - 1;
                    
                    _top++;
                }

                if (_y + _top >= _files.Count)
                {
                    _y = 0;

                    _top = 0;
                }
            }
        }
    }

    private void CheckKeys()
    {
        if (_selectDelay != 0)
        {
            _selectDelay--;

            return;
        }

        var keys = Keyboard.GetState();

        if (keys.IsKeyDown(Keys.Up))
        {
            _selectDelay = SelectDelayFramesFast;

            _y--;

            if (_y < 0)
            {
                _y = 0;

                _top--;

                if (_top < 0)
                {
                    _top = 0;
                }
            }

            return;
        }

        if (keys.IsKeyDown(Keys.Down))
        {
            _selectDelay = SelectDelayFramesFast;

            if (_y + _top + 1 == _files.Count)
            {
                return;
            }

            _y++;

            if (_y >= FileRows)
            {
                _y = FileRows - 1;

                _top++;

                if (_y + _top >= _files.Count)
                {
                    _top--;
                }
            }

            return;
        }

        if (keys.IsKeyDown(Keys.Enter))
        {
            if (_files[_top + _y].IsDirectory)
            {
                _path = _files[_top + _y].FullPath;

                _selectDelay = SelectDelayFramesSlow;

                _folderSelected = true;
            }
            else
            {
                _selectDelay = SelectDelayFramesVerySlow;
                
                AppSettings.Instance.LastZ80SnaPath = Path.GetDirectoryName(_files[_top + _y].FullPath);

                AppSettings.Instance.Save();

                _fileSelected = true;
            }
        }

        if (keys.IsKeyDown(Keys.Escape))
        {
            _selectDelay = SelectDelayFramesVerySlow;

            _cancelled = true;
        }
    }

    private void DrawFileSelect()
    {
        Array.Copy(_backgroundData, _menuData, _backgroundData.Length);
        
        DrawWindow(_menuData);

        DrawStaticItems(_menuData);

        DrawFileList(_menuData);

        Menu.SetData(_menuData);
    }

    private void GetFiles()
    {
        _files.Clear();

        if (string.IsNullOrWhiteSpace(_path))
        {
            var drives = Directory.GetLogicalDrives().ToList();

            drives.ForEach(d => _files.Add((d, d, true)));

            return;
        }

        var parent = Directory.GetParent(_path);

        if (parent != null)
        {
            _files.Add((parent.FullName, "..", true));
        }
        else
        {
            _files.Add((string.Empty, "..", true));
        }

        var directories = Directory.EnumerateDirectories(_path).OrderBy(d => d).ToList();

        directories.ForEach(d => _files.Add((d, Path.GetFileName(d), true)));

        var files = Directory.EnumerateFiles(_path).Where(f => Path.GetExtension(f).ToLowerInvariant() is ".z80" or ".sna" or ".tap").OrderBy(d => d).ToList();

        files.ForEach(f => _files.Add((f, Path.GetFileName(f), false)));
    }

    private void DrawFileList(Color[] data)
    {
        var i = _top;

        var y = 0;

        while (i < _files.Count && y < FileRows)
        {
            DrawString(data, TruncateFileName(_files[i].Display), 0, y + 3, _fileSelected || (_folderSelected && _top + i == _top + _y) ? Color.Yellow : Color.LightGreen, false, i == _top + _y);

            i++;

            y++;
        }
    }

    private static string TruncateFileName(string filename)
    {
        if (filename.Length < 29)
        {
            return filename;
        }

        if (string.IsNullOrWhiteSpace(Path.GetExtension(filename)))
        {
            return $"{filename[..25]}...";
        }

        return $"{filename[..21]}..{Path.GetExtension(filename)}";
    }

    private void DrawStaticItems(Color[] data)
    {
        DrawString(data, "Zen - Load Z80/SNA/TAP", 0, 0, Color.White, true);

        DrawString(data, "[ESC] Close Menu", 0, 21, _cancelled ? Color.LightGreen : Color.FromNonPremultiplied(255, 64, 64, 255), true, _cancelled);

        for (var y = 38; y < 40; y++)
        {
            for (var x = 24; x < 264; x++)
            {
                data[y * Constants.ScreenWidthPixels + x] = Color.White;

                data[(y + 146) * Constants.ScreenWidthPixels + x] = Color.White;
            }
        }
    }
}