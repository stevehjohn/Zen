using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Zen.Desktop.Host.Infrastructure.Settings;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class WaveformMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
        {
            new(0, true, "Zen - Waveform Viewer", Color.White, 0, 0, null),
            new(1, false, $"[1] {(AppSettings.Instance.ViewWaves ? ">" : " ")} On {(AppSettings.Instance.ViewWaves ? "<" : " ")}", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
            new(2, false, $"[2] {(AppSettings.Instance.ViewWaves ? " " : ">")} Off {(AppSettings.Instance.ViewWaves ? " " : "<")}", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
            new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
        };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                return (MenuResult.WaveformOn, null, null);

            case 2:
                return (MenuResult.WaveformOff, null, null);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }}