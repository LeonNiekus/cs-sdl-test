using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public struct Polygon
    {
        public List<FloatContainer> vertices;
        public List<FloatContainer> model;
        public FloatContainer position;
        public float angle;
        public bool overlapping;

        static void translate_to_world_coordinates(Polygon polygon)
        {
            for (int i = 0; i < polygon.model.Count; i++)
            {
                polygon.vertices[i] = new FloatContainer
                (
                    (float)((polygon.model[i].a * Math.Cos(polygon.angle)) - (polygon.model[i].b * Math.Sin(polygon.angle)) + polygon.position.a), 
                    (float)((polygon.model[i].a * Math.Sin(polygon.angle)) + (polygon.model[i].b * Math.Cos(polygon.angle)) + polygon.position.b)
                );
                polygon.overlapping = false;
            }
        }

        static FloatContainer get_center(List<FloatContainer> vertices)
        {
            FloatContainer sumCenter = new(0, 0);
            float sumWeight = 0.0f;

            for (int i = 0; i < vertices.Count; i++)
            {
                float weight = (vertices[i] - vertices[(i + 1 % vertices.Count)]).mag() + (vertices[i] - vertices[(i - 1 % vertices.Count)]).mag();
                sumCenter += vertices[i] * weight;
                sumWeight += weight;
            }
            return sumCenter / sumWeight;
        }
    }
}
