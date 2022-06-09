using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public class EntityCollection
    {
        private List<Entity> _collection;

        public EntityCollection()
        {
            _collection = new();
        }

        public EntityCollection(List<Entity> entities)
        {
            _collection = entities;
        }

        public void add_entity(Entity entity)
        {
            _collection.Add(entity);
        }

        public void remove_entity(Entity entity)
        {
            if (_collection.Contains(entity)) _collection.Remove(entity);
        }

        public List<Entity> get_all()
        {
            return _collection;
        }

        public List<Entity> get_by_name(string name)
        {
            List<Entity> result = new();
            foreach (var entity in _collection) if (entity.Name == name) result.Add(entity);
            return result;
        }

        public List<Entity> get_by_tag(string tag)
        {
            List<Entity> result = new();
            foreach (var entity in _collection) if (entity.Tag == tag) result.Add(entity);
            return result;
        }

        public List<Entity> get_by_layer(int layer)
        {
            List<Entity> result = new();
            foreach (var entity in _collection) if (entity.Layer == layer) result.Add(entity);
            return result;
        }

        public List<Entity> get_active()
        {
            List<Entity> result = new();
            foreach (var entity in _collection) if (entity.Active) result.Add(entity);
            return result;
        }

        public Camera get_active_camera()
        {
            foreach(var entity in _collection)
                if (entity.Active && entity is Camera camera)
                    return camera;
            return null;
        }
    }
}
