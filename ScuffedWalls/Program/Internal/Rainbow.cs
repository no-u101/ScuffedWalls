using System;
using System.Drawing;
using System.Linq;

namespace ScuffedWalls
{
    class Rainbow
    {
        public int color;
        public static float gradientColor = 0;

        public Rainbow()
        {
            color = 0;
        }
        public void Next()
        {
            Console.ForegroundColor = Rainbow.toConsoleColor((ScuffedColor)color);
            color++;
            if (color == Enum.GetValues(typeof(ScuffedColor)).Length) color = 0;
        }
        static ConsoleColor toConsoleColor(ScuffedColor c)
        {
            foreach (var color in Enum.GetValues(typeof(ConsoleColor)))
            {
                if (color.ToString() == c.ToString()) return (ConsoleColor)color;
            }
            return ConsoleColor.Red;
        }

        public void PrintRainbow(string s)
        {
            foreach (var letter in s)
            {
                Next();
                Console.Write(letter);
            }
            Console.Write("\n");
            Console.ResetColor();
        }

        public static ColorRGB NextColorGradient()
        {
            gradientColor += 0.01f;
            if (gradientColor > 1)
            {
                gradientColor = 0;
            }

            return HSL2RGB(gradientColor, 0.5, 0.5);
        }


        // Given H,S,L in range of 0-1

        // Returns a Color (RGB struct) in range of 0-255

        public static ColorRGB HSL2RGB(double h, double sl, double l)

        {

            double v;

            double r, g, b;



            r = l;   // default to gray

            g = l;

            b = l;

            v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl);

            if (v > 0)

            {

                double m;

                double sv;

                int sextant;

                double fract, vsf, mid1, mid2;



                m = l + l - v;

                sv = (v - m) / v;

                h *= 6.0;

                sextant = (int)h;

                fract = h - sextant;

                vsf = v * sv * fract;

                mid1 = m + vsf;

                mid2 = v - vsf;

                switch (sextant)

                {

                    case 0:

                        r = v;

                        g = mid1;

                        b = m;

                        break;

                    case 1:

                        r = mid2;

                        g = v;

                        b = m;

                        break;

                    case 2:

                        r = m;

                        g = v;

                        b = mid1;

                        break;

                    case 3:

                        r = m;

                        g = mid2;

                        b = v;

                        break;

                    case 4:

                        r = mid1;

                        g = m;

                        b = v;

                        break;

                    case 5:

                        r = v;

                        g = m;

                        b = mid2;

                        break;

                }

            }

            ColorRGB rgb;

            rgb.R = Convert.ToByte(r * 255.0f);

            rgb.G = Convert.ToByte(g * 255.0f);

            rgb.B = Convert.ToByte(b * 255.0f);

            return rgb;

        }

        public static void RGB2HSL(ColorRGB rgb, out double h, out double s, out double l)

        {

            double r = rgb.R / 255.0;

            double g = rgb.G / 255.0;

            double b = rgb.B / 255.0;

            double v;

            double m;

            double vm;

            double r2, g2, b2;



            h = 0; // default to black

            s = 0;

            l = 0;

            v = Math.Max(r, g);

            v = Math.Max(v, b);

            m = Math.Min(r, g);

            m = Math.Min(m, b);

            l = (m + v) / 2.0;

            if (l <= 0.0)

            {

                return;

            }

            vm = v - m;

            s = vm;

            if (s > 0.0)

            {

                s /= (l <= 0.5) ? (v + m) : (2.0 - v - m);

            }

            else

            {

                return;

            }

            r2 = (v - r) / vm;

            g2 = (v - g) / vm;

            b2 = (v - b) / vm;

            if (r == v)

            {

                h = (g == m ? 5.0 + b2 : 1.0 - g2);

            }

            else if (g == v)

            {

                h = (b == m ? 1.0 + r2 : 3.0 - b2);

            }

            else

            {

                h = (r == m ? 3.0 + g2 : 5.0 - r2);

            }

            h /= 6.0;
        }
    }

    public struct ColorRGB

    {

        public byte R;

        public byte G;

        public byte B;

        public ColorRGB(Color value)

        {

            this.R = value.R;

            this.G = value.G;

            this.B = value.B;

        }

        public static implicit operator Color(ColorRGB rgb)

        {

            Color c = Color.FromArgb(rgb.R, rgb.G, rgb.B);

            return c;

        }

        public static explicit operator ColorRGB(Color c)

        {

            return new ColorRGB(c);

        }

    }

    enum ScuffedColor
    {
        Red, Yellow, Green, Cyan, Blue, Magenta
    }



}
