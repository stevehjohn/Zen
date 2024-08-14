using System;
using System.IO;
using System.Text.Json;
using Zen.Common.Infrastructure;
using Zen.System.Infrastructure;

namespace Zen.Desktop.Host.Infrastructure.Settings;

public class AppSettings
{
    private const string SettingsFile = "app-settings.json";

    private static readonly Lazy<AppSettings> Lazy = new(GetAppSettings);

    public static AppSettings Instance => Lazy.Value;

    public string LastZ80SnaPath { get; set; }

    public Model SystemModel { get; set; }

    public int ScaleFactor { get; set; }
    
    public bool Sound { get; set; }
    
    public AudioEngine AudioEngine { get; set; }

    public Speed Speed { get; set; }

    public Visualisation Visualisation { get; set; }

    public bool ViewCounters { get; set; }
    
    public ColourScheme ColourScheme { get; set; }

    private static AppSettings GetAppSettings()
    {
        AppSettings settings;
        
        try
        {
            var json = File.ReadAllText(SettingsFile);

            settings = JsonSerializer.Deserialize<AppSettings>(json);

            settings.Speed = Speed.Normal;
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(AppSettings), exception);

            settings = new AppSettings();
        }

        return settings;
    }

    public void Save()
    {
        try
        {
            File.WriteAllText(SettingsFile, JsonSerializer.Serialize(this));
        }
        catch (Exception exception)
        {
            Logger.LogException(nameof(AppSettings), exception);
        }
    }
}