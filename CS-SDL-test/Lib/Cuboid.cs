using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib
{
    public struct DoublePoint3D
    {
        public double x, y, z;

        public DoublePoint3D(double x, double y, double z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        public Point3D to_int_point()
        {
            return new Point3D(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(z));
        }
    }

    public struct Cuboid
    {
        public int x, y, z;
        public int w, h, l;
        public double w_axis_center, h_axis_center, l_axis_center;
        public DoublePoint3D total_center;

        public Cuboid(Point3D position, int w, int h, int l)
        {
            x = position.x;
            y = position.y;
            z = position.z;
            this.w = w;
            this.h = h;
            this.l = l;

            w_axis_center = x + (w / 2);
            h_axis_center = y + (h / 2);
            l_axis_center = z + (l / 2);
            total_center = new DoublePoint3D(w_axis_center, h_axis_center, l_axis_center);
        }

        public Cuboid(Point3D position, int size)
        {
            x = position.x;
            y = position.y;
            z = position.z;
            w = size;
            h = size;
            l = size;

            w_axis_center = x + (size / 2);
            h_axis_center = y + (size / 2);
            l_axis_center = z + (size / 2);
            total_center = new DoublePoint3D(w_axis_center, h_axis_center, l_axis_center);
        }

        public int x2()
        {
            return x + w;
        }

        public int y2()
        {
            return y + h;
        }

        public int z2()
        {
            return z + l;
        }
    }

    public struct FCuboid
    {
        public float x, y, z;
        public float w, h, l;
        public double w_axis_center, h_axis_center, l_axis_center;
        public DoublePoint3D total_center;

        public FCuboid(Point3D position, float w, float h, float l)
        {
            x = position.x;
            y = position.y;
            z = position.z;
            this.w = w;
            this.h = h;
            this.l = l;

            w_axis_center = x + (w / 2);
            h_axis_center = y + (h / 2);
            l_axis_center = z + (l / 2);
            total_center = new DoublePoint3D(w_axis_center, h_axis_center, l_axis_center);
        }

        public FCuboid(Point3D position, float size)
        {
            x = position.x;
            y = position.y;
            z = position.z;
            w = size;
            h = size;
            l = size;

            w_axis_center = x + (size / 2);
            h_axis_center = y + (size / 2);
            l_axis_center = z + (size / 2);
            total_center = new DoublePoint3D(w_axis_center, h_axis_center, l_axis_center);
        }

        public float x2()
        {
            return x + w;
        }

        public float y2()
        {
            return y + h;
        }

        public float z2()
        {
            return z + l;
        }
    }
}
