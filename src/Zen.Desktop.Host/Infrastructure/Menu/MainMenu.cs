using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class MainMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
                    {
                        new(0, true, "Zen - Menu", Color.White, 0, 0, null),
                        new(1, false, "[1] Select System", Color.Yellow, 1, 2, Keys.D1, Color.LightGreen),
                        new(2, false, "[2] Load Z80/SNA/TAP File", Color.Yellow, 1, 4, Keys.D2, Color.LightGreen),
                        new(3, false, "[3] Load/Save State", Color.Yellow, 1, 6, Keys.D3, Color.LightGreen),
                        new(4, false, "[4] Emulator Speed", Color.Yellow, 1, 8, Keys.D4, Color.LightGreen),
                        new(5, false, "[5] Display Scale", Color.Yellow, 1, 10, Keys.D5, Color.LightGreen),
                        new(6, false, "[6] Sound", Color.Yellow, 1, 12, Keys.D6, Color.LightGreen),
                        new(7, false, "[7] Visualisation", Color.Yellow, 1, 14, Keys.D7, Color.LightGreen),
                        new(8, false, "[8] Counters", Color.Yellow, 1, 16, Keys.D8, Color.LightGreen),
                        new(9, false, "[9] Colours", Color.Yellow, 1, 18, Keys.D9, Color.LightGreen),
                        new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 20, Keys.Escape, Color.LightGreen)
                    };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                return (MenuResult.NewMenu, new SystemMenu(), null);

            case 2:
                return (MenuResult.LoadZ80Sna, null, null);

            case 3:
                return (MenuResult.NewMenu, new StateMenu(), null);

            case 4:
                return (MenuResult.NewMenu, new SpeedMenu(), null);

            case 5:
                return (MenuResult.NewMenu, new ScaleMenu(), null);
            
            case 6:
                return (MenuResult.NewMenu, new SoundMenu(), null);

            case 7:
                return (MenuResult.NewMenu, new VisualisationMenu(), null);

            case 8:
                return (MenuResult.NewMenu, new CountersMenu(), null);

            case 9:
                return (MenuResult.NewMenu, new ColoursMenu(), null);
            
            case 99:
                return (MenuResult.Exit, null, null);

            default:
                return (MenuResult.NoAction, null, null);
        }
    }
}