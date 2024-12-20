using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Zen.Desktop.Host.Infrastructure.Settings;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class VisualisationMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
        {
            new(0, true, "Zen - Visualisation", Color.White, 0, 0, null),
            new(1, false, $"[1] {(AppSettings.Instance.Visualisation == Visualisation.Off ? ">" : " ")} Off {(AppSettings.Instance.Visualisation == Visualisation.Off ? "<" : " ")}", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
            new(2, false, $"[2] {(AppSettings.Instance.Visualisation == Visualisation.Waveforms ? ">" : " ")} Waveforms {(AppSettings.Instance.Visualisation == Visualisation.Waveforms ? "<" : " ")}", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
            new(3, false, $"[3] {(AppSettings.Instance.Visualisation == Visualisation.SpectrumAnalyser ? ">" : " ")} Spectrum Analyser {(AppSettings.Instance.Visualisation == Visualisation.SpectrumAnalyser ? "<" : " ")}", Color.Yellow, 1, 7, Keys.D3, Color.LightGreen),
            new(4, false, $"[4] {(AppSettings.Instance.Visualisation == Visualisation.VideoRam ? ">" : " ")} Video Memory {(AppSettings.Instance.Visualisation == Visualisation.VideoRam ? "<" : " ")}", Color.Yellow, 1, 9, Keys.D4, Color.LightGreen),
            new(5, false, $"[5] {(AppSettings.Instance.Visualisation == Visualisation.VideoBanks ? ">" : " ")} 128k Video Banks {(AppSettings.Instance.Visualisation == Visualisation.VideoBanks ? "<" : " ")}", Color.Yellow, 1, 11, Keys.D5, Color.LightGreen),
            new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
        };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                return (MenuResult.VisualisationOff, null, null);

            case 2:
                return (MenuResult.VisualisationWaveform, null, null);

            case 3:
                return (MenuResult.VisualisationSpectrumAnalyser, null, null);
            
            case 4:
                return (MenuResult.VisualisationVideoRam, null, null);
            
            case 5:
                return (MenuResult.VisualisationVideoBanks, null, null);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }}