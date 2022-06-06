using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib
{
    public struct Rect
    {
        public int x, y, w, h;

        public Rect(int x, int y, int w, int h)
        {
            this.x = x; this.y = y; this.w = w; this.h = h;
        }

        public Rect(int w, int h)
        {
            x = 0; y = 0; this.w = w; this.h = h;
        }

        public static Rect operator+(Rect a, Rect b)
        {
            return new Rect(a.x + b.x, a.y + b.y, a.w + b.w, a.h + b.h);
        }

        public static Rect operator-(Rect a, Rect b)
        {
            return new Rect(a.x - b.x, a.y - b.y, a.w - b.w, a.h - b.h);
        }

        public static Rect operator*(Rect a, Rect b)
        {
            return new Rect(a.x * b.x, a.y * b.y, a.w * b.w, a.h * b.h);
        }

        public static Rect operator/(Rect a, Rect b)
        {
            return new Rect(a.x / b.x, a.y / b.y, a.w / b.w, a.h / b.h);
        }
    }
}
