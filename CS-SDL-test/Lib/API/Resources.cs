using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public static class Resources
    {
        private static List<Entity> _entities = new();

        public static List<Entity> get_all_entities()
        {
            return _entities;
        }

        public static void add_entity(Entity entity)
        {
            if (!_entities.Contains(entity)) _entities.Add(entity);
        }

        public static Entity create_entity(string name)
        {
            Entity entity = new(name);
            add_entity(entity);
            return entity;
        }

        public static Camera create_camera(string name, Colour background_colour, float aspect_w, float aspect_h)
        {
            Camera camera = new(name, background_colour, aspect_w, aspect_h);
            add_entity(camera);
            return camera;
        }

        public static Entity create_entity(string name, string tag, int layer, Point transform)
        {
            Entity entity = new(name, tag, layer, transform);
            add_entity(entity);
            return entity;
        }

        public static void remove_entity(Entity entity)
        {
            if (_entities.Contains(entity)) _entities.Remove(entity);
            else throw new IndexOutOfRangeException("Resources._entities does not contain given entity.");
        }
    }
}
