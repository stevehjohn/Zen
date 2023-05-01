using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class StateMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
                    {
                        new(0, true, "Zen - State", Color.White, 0, 0, null),
                        new(1, false, "[1] Save System State", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
                        new(2, false, "[2] Load System State", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
                        new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 16, Keys.Escape, Color.LightGreen)
                    };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                return (MenuResult.SaveState, null, null);

            case 2:
                return (MenuResult.LoadState, null, null);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }
}