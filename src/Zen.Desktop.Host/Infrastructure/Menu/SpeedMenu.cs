using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zen.Desktop.Host.Infrastructure.Settings;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class SpeedMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
                    {
                        new(0, true, "Zen - Emulator Speed", Color.White, 0, 0, null),
                        new(1, false, $"[1] {(AppSettings.Instance.Speed == Speed.Slow ? ">" : " ")} Slow (experimental) {(AppSettings.Instance.Speed == Speed.Slow ? "<" : " ")}", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
                        new(2, false, $"[2] {(AppSettings.Instance.Speed == Speed.Normal ? ">" : " ")} Normal {(AppSettings.Instance.Speed == Speed.Normal ? "<" : " ")}", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
                        new(3, false, $"[3] {(AppSettings.Instance.Speed == Speed.Fast ? ">" : " ")} Fast {(AppSettings.Instance.Speed == Speed.Fast ? "<" : " ")}", Color.Yellow, 1, 7, Keys.D3, Color.LightGreen),
                        new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
                    };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                AppSettings.Instance.Speed = Speed.Slow;
                AppSettings.Instance.Save();

                return (MenuResult.SpeedSlow, null, null);

            case 2:
                AppSettings.Instance.Speed = Speed.Normal;
                AppSettings.Instance.Save();

                return (MenuResult.SpeedNormal, null, null);

            case 3:
                AppSettings.Instance.Speed = Speed.Fast;
                AppSettings.Instance.Save();

                return (MenuResult.SpeedFast, null, null);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }
}