using Microsoft.Xna.Framework;

namespace Zen.Desktop.Host.Features.SupportingClasses;

public static class PaletteGenerator
{
    public static Color[] GetPalette(int steps, Color[] markers)
    {
        var palette = new Color[steps];

        var markerPeriod = steps / (markers.Length - 2);

        var current = markers[0];

        var next = markers[1];

        var counter = markerPeriod;

        var markerIndex = 1;

        for (var i = 0; i < steps; i++)
        {
            palette[i] = new Color(current.R, current.G, current.B);

            current.R += (byte) ((next.R - current.R) / markerPeriod);
            current.G += (byte) ((next.G - current.G) / markerPeriod);
            current.B += (byte) ((next.B - current.B) / markerPeriod);

            counter--;

            if (counter == 0)
            {
                counter = markerPeriod;

                markerIndex++;

                next = markers[markerIndex];
            }
        }

        return palette;
    }
}