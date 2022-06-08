using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib
{
    public struct Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x; this.y = y;
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
    }
}
