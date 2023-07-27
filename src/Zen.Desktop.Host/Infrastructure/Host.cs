using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Linq;
using Zen.Common;
using Zen.Common.Infrastructure;
using Zen.Desktop.Host.Display;
using Zen.Desktop.Host.Features;
using Zen.Desktop.Host.Infrastructure.Menu;
using Zen.Desktop.Host.Infrastructure.Settings;
using Zen.System;
using Zen.System.FileHandling;
using Zen.System.FileHandling.Interfaces;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Model = Zen.System.Infrastructure.Model;

namespace Zen.Desktop.Host.Infrastructure;

public class Host : Game
{
    private const int StartPause = 30;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly GraphicsDeviceManager _graphicsDeviceManager;

    private int _scaleFactor = AppSettings.Instance.ScaleFactor;

    private VideoRenderer _vRamAdapter;

    private Motherboard _motherboard;

    private SpriteBatch _spriteBatch;

    private string _imageName = "Standard ROM";

    private bool _hostStarted;

    private MenuSystem _menuSystem;

    private int? _pause = StartPause;

    private bool _soundState;

    private WaveVisualiser _waveVisualiser;

    public Host()
    {
        if (AppSettings.Instance.ViewWaves)
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                     {
                                         PreferredBackBufferWidth = (Constants.ScreenWidthPixels + Constants.WavePanelWidth) * _scaleFactor,
                                         PreferredBackBufferHeight = Constants.ScreenHeightPixels * _scaleFactor
                                     };
        }
        else
        {
            _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                     {
                                         PreferredBackBufferWidth = Constants.ScreenWidthPixels * _scaleFactor,
                                         PreferredBackBufferHeight = Constants.ScreenHeightPixels * _scaleFactor
                                     };
        }

        Content.RootDirectory = "_Content";

        IsMouseVisible = false;

        SetMotherboard(AppSettings.Instance.SystemModel);
    }

    private void SetMotherboard(Model model)
    {
        _motherboard = new Motherboard(model);

        _motherboard.AddPeripheral(new Peripherals.Keyboard());
        _motherboard.AddPeripheral(new Peripherals.Kempston());
        _motherboard.AddPeripheral(new Peripherals.DiskDrive());

        _motherboard.Sound = AppSettings.Instance.Sound;

        _imageName = $"Standard {model} ROM";

        AppSettings.Instance.SystemModel = model;

        AppSettings.Instance.Save();

        _vRamAdapter = new VideoRenderer(_motherboard.VideoAdapter.ScreenFrame, _graphicsDeviceManager);

        if (AppSettings.Instance.ViewWaves)
        {
            _waveVisualiser = new WaveVisualiser(_graphicsDeviceManager, _scaleFactor);

            _motherboard.AyAudio.SignalHook = _waveVisualiser.ReceiveSignals;

            _motherboard.Beeper.SignalHook = _waveVisualiser.ReceiveSignal;
        }
    }

    protected override void OnActivated(object sender, EventArgs args)
    {
        if (! _hostStarted)
        {
            _motherboard.Start();

            _motherboard.Pause();

            _hostStarted = true;

            _motherboard.Fast = AppSettings.Instance.Fast;

            _motherboard.Sound = AppSettings.Instance.Sound;
        }
    }

    protected override void Initialize()
    {
        Window.Title = "Zen";

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (_pause != null)
        {
            _pause--;
        }

        if (_pause == 0)
        {
            _pause = null;

            _motherboard.Resume();
        }

        if (Keyboard.GetState().IsKeyDown(Keys.F10) && _menuSystem == null)
        {
            _motherboard.Pause();

            _vRamAdapter.RenderDisplay();

            var screen = _vRamAdapter.Display;

            _soundState = _motherboard.Sound;

            _motherboard.Sound = false;

            _menuSystem = new MenuSystem(screen, _graphicsDeviceManager, Content, MenuFinished);
        }

        if (_menuSystem != null)
        {
            _menuSystem.Update();
        }

        base.Update(gameTime);

        Window.Title = $"Zen - FPS: {Counters.Instance.GetCountPerSecond(Counter.RenderedFrames)} OPS: {Counters.Instance.GetCountPerSecond(Counter.Instructions)} SFPS: {Counters.Instance.GetCountPerSecond(Counter.SpectrumFrames)} AYFPS: {Counters.Instance.GetCountPerSecond(Counter.AyFrames)}";
    }

    private void MenuFinished(MenuResult result, object arguments)
    {
        _menuSystem = null;

        switch (result)
        {
            case MenuResult.Restart:
                SetMotherboard((Model) arguments);

                _motherboard.Start();

                return;

            case MenuResult.LoadZ80Sna:
                LoadZ80Sna((string) arguments);

                break;

            case MenuResult.SpeedNormal:
                _motherboard.Fast = false;

                break;

            case MenuResult.SpeedFast:
                _motherboard.Fast = true;

                break;

            case MenuResult.SaveState:
                SaveState();

                break;

            case MenuResult.LoadState:
                LoadState();

                break;

            case MenuResult.ChangeScale:
                ChangeScale((int) arguments);

                break;

            case MenuResult.SoundOn:
                _motherboard.Sound = true;
                AppSettings.Instance.Sound = true;
                AppSettings.Instance.Save();

                break;

            case MenuResult.SoundOff:
                _motherboard.Sound = false;
                AppSettings.Instance.Sound = false;
                AppSettings.Instance.Save();

                break;

            case MenuResult.WaveformOn:
                AppSettings.Instance.ViewWaves = true;
                AppSettings.Instance.Save();

                _graphicsDeviceManager.PreferredBackBufferWidth = (Constants.ScreenWidthPixels + Constants.WavePanelWidth) * _scaleFactor;
                _graphicsDeviceManager.PreferredBackBufferHeight = Constants.ScreenHeightPixels * _scaleFactor;
                _graphicsDeviceManager.ApplyChanges();

                _waveVisualiser = new WaveVisualiser(_graphicsDeviceManager, _scaleFactor);
                _motherboard.AyAudio.SignalHook = _waveVisualiser.ReceiveSignals;
                _motherboard.Beeper.SignalHook = _waveVisualiser.ReceiveSignal;

                break;
            
            case MenuResult.WaveformOff:
                AppSettings.Instance.ViewWaves = false;
                AppSettings.Instance.Save();

                _graphicsDeviceManager.PreferredBackBufferWidth = Constants.ScreenWidthPixels * _scaleFactor;
                _graphicsDeviceManager.PreferredBackBufferHeight = Constants.ScreenHeightPixels * _scaleFactor;
                _graphicsDeviceManager.ApplyChanges();

                _waveVisualiser = null;
                _motherboard.AyAudio.SignalHook = null;
                _motherboard.Beeper.SignalHook = null;

                break;
        }

        if (_soundState && result != MenuResult.SoundOff)
        {
            _motherboard.Sound = true;
        }

        _motherboard.Resume();
    }

    private void ChangeScale(int scale)
    {
        _scaleFactor = scale;

        if (AppSettings.Instance.ViewWaves)
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = (Constants.ScreenWidthPixels + Constants.WavePanelWidth) * _scaleFactor;
            _graphicsDeviceManager.PreferredBackBufferHeight = Constants.ScreenHeightPixels * _scaleFactor;
        }
        else
        {
            _graphicsDeviceManager.PreferredBackBufferWidth = Constants.ScreenWidthPixels * _scaleFactor;
            _graphicsDeviceManager.PreferredBackBufferHeight = Constants.ScreenHeightPixels * _scaleFactor;
        }

        _graphicsDeviceManager.ApplyChanges();

        _waveVisualiser.ScaleFactor = scale;

        AppSettings.Instance.ScaleFactor = _scaleFactor;
        AppSettings.Instance.Save();
    }

    private void LoadState()
    {
        _motherboard.Pause();

        _motherboard.Reset();

        var adapter = new ZenFileAdapter(_motherboard);

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Zen Snapshots");

        var directoryInfo = new DirectoryInfo(path);

        var file = directoryInfo.EnumerateFiles("*").MaxBy(f => f.CreationTimeUtc);

        if (file != null)
        {
            _imageName = adapter.Load(file.FullName);
        }

        _motherboard.Resume();
    }

    private void SaveState()
    {
        _motherboard.Pause();

        var adapter = new ZenFileAdapter(_motherboard);

        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Zen Snapshots", $"{_imageName} {DateTime.Now:yyyy-MM-dd HH-mm}.zen.json");

        adapter.Save(path, _imageName, _motherboard.Model);

        _motherboard.Resume();
    }

    private void LoadZ80Sna(string filename)
    {
        IFileLoader loader = null;

        switch (Path.GetExtension(filename).ToLowerInvariant())
        {
            case ".z80":
                loader = new Z80FileLoader(_motherboard.State, _motherboard.Ram, _motherboard.Model);

                break;
            case ".sna":
                loader = new SnaFileLoader(_motherboard.State, _motherboard.Ram);

                break;

            case ".tap":
                _motherboard.StageFile(filename);

                break;
            default:
                // TODO: Proper exception
                throw new Exception("Unsupported file format");
        }

        if (loader != null)
        {
            loader.Load(filename);
        }

        _imageName = filename.Split(Path.DirectorySeparatorChar)[^2];

        _motherboard.Resume();
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        Texture2D screen;

        if (_menuSystem != null)
        {
            screen = _menuSystem.Menu;
        }
        else
        {
            _vRamAdapter.RenderDisplay();

            screen = _vRamAdapter.Display;
        }

        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        _spriteBatch.Draw(screen, new Rectangle(0, 0, Constants.ScreenWidthPixels * _scaleFactor, Constants.ScreenHeightPixels * _scaleFactor), new Rectangle(0, 0, Constants.ScreenWidthPixels, Constants.ScreenHeightPixels), Color.White);

        if (_waveVisualiser != null)
        {
            var waves = _waveVisualiser.Waves;

            if (waves != null)
            {
                _spriteBatch.Draw(waves, new Rectangle(Constants.ScreenWidthPixels * _scaleFactor, 0, Constants.WavePanelWidth * _scaleFactor, Constants.ScreenHeightPixels * _scaleFactor), new Rectangle(0, 0, Constants.WavePanelWidth * _scaleFactor, Constants.ScreenHeightPixels * _scaleFactor), Color.White);
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);

        Counters.Instance.IncrementCounter(Counter.RenderedFrames);

        screen.Dispose();
    }
}