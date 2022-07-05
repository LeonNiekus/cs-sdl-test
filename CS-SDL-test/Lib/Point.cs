using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib
{
    public struct Point3D
    {
        public int x, y, z;

        public Point3D(int x, int y, int z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        public Point3D(int x, int y)
        {
            this.x = x; this.y = y; z = 0;
        }

        public static Point3D operator +(Point3D a, Point3D b)
        {
            return new Point3D(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Point3D operator +(Point3D a, int factor)
        {
            return new Point3D(a.x + factor, a.y + factor, a.z + factor);
        }

        public static Point3D operator -(Point3D a, Point3D b)
        {
            return new Point3D(a.x - b.x, a.y - b.y, a.z + b.z);
        }

        public static Point3D operator *(Point3D a, Point3D b)
        {
            return new Point3D(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Point3D operator /(Point3D a, Point3D b)
        {
            return new Point3D(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Point3D operator /(Point3D a, int factor)
        {
            return new Point3D(a.x / factor, a.y / factor, a.z / factor);
        }

        public void transform_x(int factor)
        {
            x += factor;
        }

        public void transform_y(int factor)
        {
            y += factor;
        }

        public void transform_z(int factor)
        {
            z += factor;
        }

        public void transform(int factor_x, int factor_y, int factor_z)
        {
            x += factor_x;
            y += factor_y;
            z += factor_z;
        }

        public void transform(int factor)
        {
            x += factor;
            y += factor;
            z += factor;
        }
    }

    public struct FPoint3D
    {
        public float x, y, z;

        public FPoint3D(float x, float y, float z)
        {
            this.x = x; this.y = y; this.z = z;
        }

        public FPoint3D(float x, float y)
        {
            this.x = x; this.y = y; z = 0;
        }

        public static FPoint3D operator +(FPoint3D a, FPoint3D b)
        {
            return new FPoint3D(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static FPoint3D operator +(FPoint3D a, float factor)
        {
            return new FPoint3D(a.x + factor, a.y + factor, a.z + factor);
        }

        public static FPoint3D operator -(FPoint3D a, FPoint3D b)
        {
            return new FPoint3D(a.x - b.x, a.y - b.y, a.z + b.z);
        }

        public static FPoint3D operator *(FPoint3D a, FPoint3D b)
        {
            return new FPoint3D(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static FPoint3D operator /(FPoint3D a, FPoint3D b)
        {
            return new FPoint3D(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static FPoint3D operator /(FPoint3D a, float factor)
        {
            return new FPoint3D(a.x / factor, a.y / factor, a.z / factor);
        }

        public void transform_x(float factor)
        {
            x += factor;
        }

        public void transform_y(float factor)
        {
            y += factor;
        }

        public void transform_z(float factor)
        {
            z += factor;
        }

        public void transform(float factor_x, float factor_y, float factor_z)
        {
            x += factor_x;
            y += factor_y;
            z += factor_z;
        }

        public void transform(float factor)
        {
            x += factor;
            y += factor;
            z += factor;
        }
    }
}
