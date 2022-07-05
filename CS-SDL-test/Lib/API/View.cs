using CS_SDL_test.Lib.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public abstract class View
    {
        private string _name;
        private Rect _size;
        private EntityCollection _entities;

        public View()
        {
            _entities = new EntityCollection();
            _name = "DEFAULT";
            _size = new Rect(200, 200);
        }

        public View(string view_name, Rect view_size)
        {
            _entities = new EntityCollection();
            _name = view_name;
            _size = view_size;
        }

        public string Name { get => _name; }
        public Rect Size { get => _size; set => _size = value; }
        public EntityCollection Entities { get => _entities; }

        public abstract void on_create();
        public abstract void on_destroy();
        public abstract void on_load();
        public abstract void on_unload();
        public abstract void on_activate();
        public abstract void on_deactivate();
        public abstract void on_update();

        public void add_entity(Entity entity)
        {
            if (entity.Parent != null) return;

            var self = ViewManager.get_view_by_name(_name);
            entity.View = self;
            _entities.add_entity(entity);
        }

        public void remove_entity(Entity entity)
        {
            if (entity == null) return;
            entity.View = null;
            if (entity.Parent != null) return;
            _entities.remove_entity(entity);
        }

        public void render_view(Camera camera)
        {
            var active_entities = _entities.get_active();

            active_entities.Sort((Entity a, Entity b) => 
            {
                int a_layer = a.Layer;
                var a_sprite = a.get_component<Sprite>();
                if (a_sprite != null) a_layer = a_sprite.Layer;

                int b_layer = b.Layer;
                var b_sprite = b.get_component<Sprite>();
                if (b_sprite != null) b_layer = b_sprite.Layer;

                return a_layer.CompareTo(b_layer);
            });

            foreach (Entity entity in active_entities) draw_elements(entity, camera);
            draw_elements(camera, camera);
        }

        public void draw_elements(Entity entity, Camera camera)
        {
            var renderer = Renderer.Instance;
            var sprites = entity.get_components<Sprite>();

            foreach (var sprite in sprites) if (sprite.Active) renderer.render_sprite(sprite, camera.to_local(entity));
            foreach (var child in entity.Children) if (child.Active) draw_elements(child, camera);
        }
    }
}
