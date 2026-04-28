using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Zen.Desktop.Host.Infrastructure.Settings;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class CountersMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
        {
            new(0, true, "Zen - Counters", Color.White, 0, 0, null),
            new(1, false, $"[1] {(AppSettings.Instance.ViewCounters ? ">" : " ")} Show {(AppSettings.Instance.ViewCounters ? "<" : " ")}", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
            new(2, false, $"[2] {(AppSettings.Instance.ViewCounters ? " " : ">")} Hide {(AppSettings.Instance.ViewCounters ? " " : "<")}", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
            new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
        };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        return id switch
        {
            1 => (MenuResult.CountersOn, null, null),
            2 => (MenuResult.CountersOff, null, null),
            _ => (MenuResult.NewMenu, new MainMenu(), null)
        };
    }}