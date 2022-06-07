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

        public static Rect operator+(Rect a, int factor)
        {
            return new Rect(a.x + factor, a.y + factor, a.w + factor, a.h + factor);
        }

        public static Rect operator-(Rect a, Rect b)
        {
            return new Rect(a.x - b.x, a.y - b.y, a.w - b.w, a.h - b.h);
        }

        public static Rect operator-(Rect a, int factor)
        {
            return new Rect(a.x - factor, a.y - factor, a.w - factor, a.h - factor);
        }

        public static Rect operator*(Rect a, Rect b)
        {
            return new Rect(a.x * b.x, a.y * b.y, a.w * b.w, a.h * b.h);
        }

        public static Rect operator*(Rect a, int factor)
        {
            return new Rect(a.x * factor, a.y * factor, a.w * factor, a.h * factor);
        }

        public static Rect operator/(Rect a, Rect b)
        {
            return new Rect(a.x / b.x, a.y / b.y, a.w / b.w, a.h / b.h);
        }

        public static Rect operator/(Rect a, int factor)
        {
            return new Rect(a.x / factor, a.y / factor, a.w / factor, a.h / factor);
        }

        public int x2()
        {
            return x + w;
        }

        public int y2()
        {
            return y + h;
        }

        public void transform_x(int factor)
        {
            x += factor;
        }

        public void transform_y(int factor)
        {
            y += factor;
        }

        public void transform(int factor_x, int factor_y)
        {
            x += factor_x;
            y += factor_y;
        }

        public void transform(int factor)
        {
            x += factor;
            y += factor;
        }

        public void resize_w(int factor)
        {
            w += factor;
        }

        public void resize_h(int factor)
        {
            h += factor;
        }

        public void resize(int factor_w, int factor_h)
        {
            w += factor_w;
            h += factor_h;
        }

        public void resize(int factor)
        {
            w += factor;
            h += factor;
        }
    }
}
