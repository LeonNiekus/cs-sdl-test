using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public class Entity
    {
        protected string _name;
        protected string _tag;
        protected bool _active;
        protected int _layer;
        protected Point _transform;
        protected List<Component> _components = new();
        protected Entity _parent;
        protected View _belongsToScene;
        protected List<Entity> _children = new();

        public Entity(string name)
        {
            _active = true;
            _name = name;
            _layer = 0;
            _transform = new Point(0, 0);
        }

        public Entity(string name, string tag, int layer, Point transform)
        {
            _active = true;
            _name = name;
            _tag = tag;
            _layer = layer;
            _transform = transform;
        }

        public string Name { get => _name; set => _name = value; }
        public string Tag { get => _tag; set => _tag = value; }
        public bool Active { get => _active; }
        public int Layer { get => _layer; set => _layer = value; }
        public Point Transform { get => _transform; set => _transform = value; }
        public List<Component> Components { get => _components; }

        public static Entity find(string name)
        {
            foreach (Entity entity in Resources.get_all_entities())
                if (entity._name == name) 
                    return entity;

            return null;
        }

        public static List<Entity> find_all_with_tag(string tag)
        {
            List<Entity> result = new();

            foreach (Entity entity in Resources.get_all_entities())
                if (entity._tag == tag)
                    result.Add(entity);

            return result;
        }

        public static Entity find_first_with_tag(string tag)
        {
            foreach (Entity entity in Resources.get_all_entities())
                if (entity._tag == tag)
                    return entity;

            return null;
        }

        public static void destroy(Entity entity)
        {
            // TODO: remove from Views
            Resources.remove_entity(entity);
        }

        public void set_active(bool flag)
        {
            _active = flag;
            // TODO: do same for components
        }

        public void add_component(Component component)
        {
            if (!Resources.get_all_entities().Contains(this)) return;

            component.Parent = this;
            _components.Add(component);
        }
    }
}
