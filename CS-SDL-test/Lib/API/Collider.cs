using CS_SDL_test.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public abstract class Collider : Component
    {
        protected Point3D _offset;
        protected List<int> _collide_layers;
        protected CollisionData _collision_data;

        public Collider(List<int> collide_layers, Point3D offset)
        {
            _offset = offset;
            _collide_layers = collide_layers;
        }

        public List<int> CollideLayers { get => _collide_layers; }
        public Point3D Offset { get => _offset; }
        public Point3D Position 
        { 
            get 
            {
                return Parent.Transform + _offset;
            } 
        }
        public CollisionData CollisionData { get => _collision_data; }

        public abstract FloatContainer resolve_collision(Collider coll1, Collider coll2, FloatContainer velocity, ref bool collision_occurred);

        protected override void parent_changed()
        {
            if (Parent == null) return;

            EventManager.Instance.register_event_handler(EventType.COLLISION, new Events.EventCallback((Events.Event e) =>
            {
                var scripts = Parent.get_components<Script>();
                foreach (var script in scripts)
                    if (script.Active) script.on_collision_enter((Events.CollisionEvent)e);
            }));
        }

    }

    public struct CollisionData
    {
        public bool above, below, left, right, climbing_slope;
        public float slope_angle, slope_angle_old;

        public void reset()
        {
            above = below = false;
            left = right = false;
            climbing_slope = false;
            slope_angle_old = slope_angle;
            slope_angle = 0.0f;
        }
    }
}
