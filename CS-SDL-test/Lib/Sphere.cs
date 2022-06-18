using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib
{
    public struct Sphere
    {
        public Point3D center;
        public int w, h, l;
        public double rad_w, rad_h, rad_l;

        public Sphere(Point3D center, int w, int h, int l)
        {
            this.center = center;
            this.w = w;
            this.h = h;
            this.l = l;

            rad_w = w / 2;
            rad_h = h / 2;
            rad_l = l / 2;
        }

        public Sphere(Point3D center, int size)
        {
            this.center = center;
            w = size;
            h = size;
            l = size;

            rad_l = rad_h = rad_w = size / 2;
        }
    }
}
