using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class SpeedMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
                    {
                        new(0, true, "Zen - Emulator Speed", Color.White, 0, 0, null),
                        new(1, false, "[1] Normal", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
                        new(2, false, "[2] Fast", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
                        new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
                    };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                return (MenuResult.SpeedNormal, null, null);

            case 2:
                return (MenuResult.SpeedFast, null, null);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }
}