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

        /*public static Colour operator +(Colour a, Colour b)
        {
            if (a.r + b.r > 255 && a.g + b.g > 255 && a.b + b.b > 255) return new Colour(255, 255, 255);
            return new Colour((byte)a.r + b.r, a.g + b.g, a.b + b.b);
        }

        public static Colour operator -(Colour a, Colour b)
        {
            return new Colour();
        }

        public static Colour operator *(Colour a, Colour b)
        {
            return new Colour();
        }

        public static Colour operator /(Colour a, Colour b)
        {
            return new Colour();
        }*/
    }
}
