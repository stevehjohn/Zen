using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class SoundMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
        {
            new(0, true, "Zen - Sound", Color.White, 0, 0, null),
            new(1, false, "[1] On", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
            new(2, false, "[2] Off", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
            new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
        };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                return (MenuResult.SoundOn, null, null);

            case 2:
                return (MenuResult.SoundOff, null, null);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }}