using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Zen.Desktop.Host.Infrastructure.Settings;

namespace Zen.Desktop.Host.Infrastructure.Menu;

public class SoundMenu : MenuBase
{
    public override List<Label> GetMenu()
    {
        var items = new List<Label>
        {
            new(0, true, "Zen - Sound", Color.White, 0, 0, null),
            new(1, false, $"[1] {(AppSettings.Instance.Sound ? " " : ">")} Off {(AppSettings.Instance.Sound ? " " : "<")}", Color.Yellow, 1, 3, Keys.D1, Color.LightGreen),
            new(2, false, $"[2] {(AppSettings.Instance.Sound && AppSettings.Instance.AudioEngine == AudioEngine.PortAudio ? ">" : " ")} On - PortAudio {(AppSettings.Instance.AudioEngine == AudioEngine.PortAudio ? "<" : " ")}", Color.Yellow, 1, 5, Keys.D2, Color.LightGreen),
            new(3, false, $"[3] {(AppSettings.Instance.Sound && AppSettings.Instance.AudioEngine == AudioEngine.Bass ? ">" : " ")} On - Bass {(AppSettings.Instance.AudioEngine == AudioEngine.Bass ? "<" : " ")}", Color.Yellow, 1, 7, Keys.D3, Color.LightGreen),
            new(96, true, "It will Depend on your", Color.Wheat, 0, 12, null),
            new(97, true, "hardware and OS", Color.Wheat, 0, 13, null),
            new(98, true, "whether Port Audio or Bass", Color.Wheat, 0, 14, null),
            new(98, true, "produce better audio", Color.Wheat, 0, 15, null),
            new(99, true, "[ESC] Close Menu", Color.FromNonPremultiplied(255, 64, 64, 255), 0, 21, Keys.Escape, Color.LightGreen)
        };

        return items;
    }

    public override (MenuResult Result, MenuBase NewMenu, object Arguments) ItemSelected(int id)
    {
        switch (id)
        {
            case 1:
                return (MenuResult.SoundOff, null, null);

            case 2:
                return (MenuResult.SoundPortAudio, null, null);

            case 3:
                return (MenuResult.SoundBass, null, null);

            default:
                return (MenuResult.NewMenu, new MainMenu(), null);
        }
    }}