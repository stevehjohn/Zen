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

    private CountersVisualiser _countersVisualiser;

    public Host()
    {
        var width = Constants.ScreenWidthPixels * _scaleFactor;
        var height = Constants.ScreenHeightPixels * _scaleFactor;

        if (AppSettings.Instance.Visualisation == Visualisation.Waveforms)
        {
            width += Constants.VisualisationPanelWidth * _scaleFactor;
        }

        if (AppSettings.Instance.ViewCounters)
        {
            height += Constants.CountersPanelHeight * _scaleFactor;
        }

        _graphicsDeviceManager = new GraphicsDeviceManager(this)
                                 { 
                                     PreferredBackBufferWidth = width,
                                     PreferredBackBufferHeight = height
                                 };

        Content.RootDirectory = "_Content";

        IsMouseVisible = false;

        SetMotherboard(AppSettings.Instance.SystemModel);
    }

    private void SetMotherboard(Model model)
    {
        if (_motherboard != null)
        {
            _motherboard.Dispose();

            _motherboard = null;
        }

        _motherboard = new Motherboard(model);

        _motherboard.AddPeripheral(new Peripherals.Keyboard());
        _motherboard.AddPeripheral(new Peripherals.Kempston());
        _motherboard.AddPeripheral(new Peripherals.DiskDrive());

        _motherboard.Sound = AppSettings.Instance.Sound;

        _motherboard.Fast = AppSettings.Instance.Fast;

        _imageName = $"Standard {model} ROM";

        AppSettings.Instance.SystemModel = model;

        AppSettings.Instance.Save();

        if (_waveVisualiser != null)
        {
            _motherboard.AyAudio.AySignalHook = _waveVisualiser.ReceiveSignals;

            _motherboard.AyAudio.BeeperSignalHook = _waveVisualiser.ReceiveSignal;
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

        _countersVisualiser = new CountersVisualiser(_graphicsDeviceManager, Content);

        if (AppSettings.Instance.Visualisation == Visualisation.Waveforms)
        {
            _waveVisualiser = new WaveVisualiser(_graphicsDeviceManager);

            _motherboard.AyAudio.AySignalHook = _waveVisualiser.ReceiveSignals;

            _motherboard.AyAudio.BeeperSignalHook = _waveVisualiser.ReceiveSignal;
        }

        _vRamAdapter = new VideoRenderer(_motherboard.VideoAdapter.ScreenFrame, _graphicsDeviceManager);
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

        if (Keyboard.GetState().IsKeyDown(Keys.Tab) && _menuSystem == null)
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
    }

    private void MenuFinished(MenuResult result, object arguments)
    {
        _menuSystem = null;

        switch (result)
        {
            case MenuResult.Restart:
                SetMotherboard((Model) arguments);

                _motherboard.Start();

                _vRamAdapter.ScreenFrame = _motherboard.VideoAdapter.ScreenFrame;

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
            
            case MenuResult.VisualisationOff:
                AppSettings.Instance.Visualisation = Visualisation.Off;
                AppSettings.Instance.Save();

                _waveVisualiser = null;
                _motherboard.AyAudio.AySignalHook = null;
                _motherboard.AyAudio.BeeperSignalHook = null;

                ChangeScale(_scaleFactor);

                break;

            case MenuResult.VisualisationWaveform:
                AppSettings.Instance.Visualisation = Visualisation.Waveforms;
                AppSettings.Instance.Save();

                _waveVisualiser = new WaveVisualiser(_graphicsDeviceManager);
                _motherboard.AyAudio.AySignalHook = _waveVisualiser.ReceiveSignals;
                _motherboard.AyAudio.BeeperSignalHook = _waveVisualiser.ReceiveSignal;

                ChangeScale(_scaleFactor);

                break;

            case MenuResult.CountersOn:
                AppSettings.Instance.ViewCounters = true;
                AppSettings.Instance.Save();

                _countersVisualiser = new CountersVisualiser(_graphicsDeviceManager, Content);

                ChangeScale(_scaleFactor);

                break;

            case MenuResult.CountersOff:
                AppSettings.Instance.ViewCounters = false;
                AppSettings.Instance.Save();

                _countersVisualiser = null;

                ChangeScale(_scaleFactor);

                break;
            
            case MenuResult.SpectrumColours:
                AppSettings.Instance.ColourScheme = ColourScheme.Spectrum;
                AppSettings.Instance.Save();
                
                break;
            
            case MenuResult.CommodoreColours:
                AppSettings.Instance.ColourScheme = ColourScheme.Commodore;
                AppSettings.Instance.Save();
                
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

        var width = Constants.ScreenWidthPixels * _scaleFactor;
        var height = Constants.ScreenHeightPixels * _scaleFactor;

        if (AppSettings.Instance.Visualisation == Visualisation.Waveforms)
        {
            width += Constants.VisualisationPanelWidth * _scaleFactor;
        }

        if (AppSettings.Instance.ViewCounters)
        {
            height += Constants.CountersPanelHeight * _scaleFactor;
        }

        _graphicsDeviceManager.PreferredBackBufferWidth = width;
        _graphicsDeviceManager.PreferredBackBufferHeight = height;

        _graphicsDeviceManager.ApplyChanges();

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

            _motherboard.StateLoaded();
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

        _spriteBatch.Draw(screen, 
                          new Rectangle(0, 0, Constants.ScreenWidthPixels * _scaleFactor, Constants.ScreenHeightPixels * _scaleFactor), 
                          new Rectangle(0, 0, Constants.ScreenWidthPixels, Constants.ScreenHeightPixels), Color.White);

        if (_waveVisualiser != null)
        {
            var waves = _waveVisualiser.Waves;

            if (waves != null)
            {
                _spriteBatch.Draw(waves,
                                  new Rectangle(Constants.ScreenWidthPixels * _scaleFactor, 0, Constants.VisualisationPanelWidth * _scaleFactor, Constants.ScreenHeightPixels * _scaleFactor), 
                                  new Rectangle(0, 0, Constants.VisualisationPanelWidth, Constants.ScreenHeightPixels), Color.White);
            }
        }

        if (_countersVisualiser != null)
        {
            _spriteBatch.Draw(_countersVisualiser.RenderPanel(), 
                              new Rectangle(0, Constants.ScreenHeightPixels * _scaleFactor, Constants.ScreenWidthPixels * _scaleFactor, Constants.CountersPanelHeight * _scaleFactor), 
                              new Rectangle(0, 0, Constants.ScreenWidthPixels, Constants.CountersPanelHeight), Color.White);
        }

        _spriteBatch.End();

        base.Draw(gameTime);

        Counters.Instance.IncrementCounter(Counter.RenderedFrames);
    }
}