using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib
{
    public struct Colour
    {
        public byte r, g, b, a;

        public Colour(byte r, byte g, byte b, byte a)
        {
            this.r = r; this.g = g; this.b = b; this.a = a;
        }

        public Colour(byte r, byte g, byte b)
        {
            this.r = r; this.g = g; this.b = b; a = 1;
        }

        public static Colour operator +(Colour a, Colour b)
        {
            if (a.r + b.r > 255 || a.g + b.g > 255 || a.b + b.b > 255) return new Colour(255, 255, 255);
            return new Colour(Convert.ToByte(a.r + b.r), Convert.ToByte(a.g + b.g), Convert.ToByte(a.b + b.b));
        }

        public static Colour operator -(Colour a, Colour b)
        {
            if (a.r - b.r < 0 || a.g - b.g < 0 || a.b - b.b < 0) return new Colour(0, 0, 0);
            return new Colour(Convert.ToByte(a.r - b.r), Convert.ToByte(a.g - b.g), Convert.ToByte(a.b - b.b));
        }

        public static Colour black()
        {
            return new Colour(0, 0, 0);
        }

        public static Colour white()
        {
            return new Colour(255, 255, 255);
        }

        public static Colour blue()
        {
            return new Colour(0, 0, 255);
        }

        public static Colour red()
        {
            return new Colour(255, 0, 0);
        }

        public static Colour yellow()
        {
            return new Colour(255, 255, 0);
        }

        public static Colour green()
        {
            return new Colour(0, 255, 0);
        }

        //etc.
    }
}
