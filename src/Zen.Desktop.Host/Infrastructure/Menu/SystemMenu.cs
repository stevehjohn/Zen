using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zen.System.Infrastructure;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class SystemMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
                    {
                        new(0, true, "Zen - Select System", Color.White, 0, 0, null),
                        new(1, false, "[1] Spectrum 48K", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
                        new(2, false, "[2] Spectrum 128", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
                        new(3, false, "[3] Spectrum +2", Color.Yellow, 1, 7, Keys.D3, Color.LightGreen),
                        new(4, false, "[4] Spectrum +3", Color.Yellow, 1, 9, Keys.D4, Color.LightGreen),
                        new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
                    };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                return (MenuResult.Restart, null, Model.Spectrum48K);

            case 2:
                return (MenuResult.Restart, null, Model.Spectrum128);

            case 3:
                return (MenuResult.Restart, null, Model.SpectrumPlus2);

            case 4:
                return (MenuResult.Restart, null, Model.SpectrumPlus3);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }
}