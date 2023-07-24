using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Zen.Desktop.Host.Infrastructure.Settings;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class ScaleMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
                    {
                        new(0, true, "Zen - Display Scale", Color.White, 0, 0, null),
                        new(1, false, $"[1] {(AppSettings.Instance.ScaleFactor == 2 ? ">" : " ")} 2x {(AppSettings.Instance.ScaleFactor == 2 ? "<" : " ")}", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
                        new(2, false, $"[2] {(AppSettings.Instance.ScaleFactor == 4 ? ">" : " ")} 4x {(AppSettings.Instance.ScaleFactor == 4 ? "<" : " ")}", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
                        new(3, false, $"[3] {(AppSettings.Instance.ScaleFactor == 6 ? ">" : " ")} 6x {(AppSettings.Instance.ScaleFactor == 6 ? "<" : " ")}", Color.Yellow, 1, 7, Keys.D3, Color.LightGreen),
                        new(4, false, $"[4] {(AppSettings.Instance.ScaleFactor == 8 ? ">" : " ")} 8x {(AppSettings.Instance.ScaleFactor == 8 ? "<" : " ")}", Color.Yellow, 1, 9, Keys.D4, Color.LightGreen),
                        new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
                    };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                return (MenuResult.ChangeScale, null, id * 2);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }
}