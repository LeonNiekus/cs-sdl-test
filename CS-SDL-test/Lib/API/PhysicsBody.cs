using CS_SDL_test.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public struct FloatContainer
    {
        public float a, b;

        public FloatContainer(float a, float b)
        {
            this.a = a; this.b = b;
        }

        public static FloatContainer operator +(FloatContainer a, FloatContainer b)
        {
            return new FloatContainer(a.a + b.a, a.b + b.b);
        }

        public static FloatContainer operator -(FloatContainer a, FloatContainer b)
        {
            return new FloatContainer(a.a - b.a, a.b - b.b);
        }

        public static FloatContainer operator *(FloatContainer a, FloatContainer b)
        {
            return new FloatContainer(a.a * b.a, a.b * b.b);
        }

        public static FloatContainer operator *(FloatContainer a, float factor)
        {
            return new FloatContainer(a.a * factor, a.b * factor);
        }

        public static FloatContainer operator /(FloatContainer a, float factor)
        {
            return new FloatContainer(a.a / factor, a.b / factor);
        }

        public static bool operator ==(FloatContainer a, FloatContainer b)
        {
            return a.a == b.a && a.b == b.b;
        }

        public static bool operator !=(FloatContainer a, FloatContainer b)
        {
            return a.a != b.a || a.b != b.b;
        }

        public float mag()
        {
            return (float)Math.Sqrt(a * a + b * b);
        }
    }

    public class PhysicsBody : Component
    {
        private bool _enable_gravity;
        private float _gravity_scale;
        private float _mass;
        private FloatContainer _velocity;
        private FloatContainer _acceleration;
        private float _jump_height;
        private float _time_to_jump_apex;
        private float _x_friction;
        private float _jump_velocity;

        public PhysicsBody(float jump_height, float time_to_jump_apex, bool enable_gravity, float mass = 1.0f, float x_friction = 2.0f)
        {
            _jump_height = jump_height;
            _time_to_jump_apex = time_to_jump_apex;
            _enable_gravity = enable_gravity;
            _mass = mass;
            _x_friction = x_friction;
        }

        public FloatContainer Velocity { get => _velocity; set => _velocity = value; }
        public float Mass { get => _mass; set => _mass = value; }
        public float JumpHeight { get => _jump_height; }
        public float TimeToJumpApex { get => _time_to_jump_apex; }
        public float JumpVelocity { get => _jump_velocity + _gravity_scale; }
        public bool HasGravity
        {
            set 
            {
                _enable_gravity = value;
                _gravity_scale = _enable_gravity ? (float)(-(2.0f * _jump_height) / Math.Pow(_time_to_jump_apex, 2)) : 0.0f;
                _jump_velocity = _enable_gravity ? Math.Abs(_gravity_scale) * _time_to_jump_apex : 5.0f;
            }
        }
        public float XFriction { get => _x_friction; set { if (value < 1.0f) return; _x_friction = value; } }

        private void update_physics_on_colliders(Collider collider1, Collider collider2)
        {
            if (collider1 == collider2) return;

            bool collision_occurred = false;
            if (_velocity.a != 0 || _velocity.b != 0)
                _velocity = collider1.resolve_collision(collider1, collider2, _velocity, ref collision_occurred);

            if (collision_occurred)
            {
                var collisionEvent = new Events.CollisionEvent(false, EventType.COLLISION, collider1, collider2);
                EventManager.Instance.generate_event(collisionEvent);
            }
        }

        public void add_force(FloatContainer force_direction)
        {
            _velocity += force_direction;
        }

        public void update_physics()
        {
            int time_steps = (int)Math.Ceiling(Time.get_time_scale());
            float time_scale = (float)(Time.get_time_scale() / time_steps);

            _velocity.a /= _x_friction;

            for (int i = 0; i < time_steps; ++i)
            {
                _velocity.b += _gravity_scale * Time.delta_time * time_scale;

                var collider1 = Parent.get_component<Collider>();
                if (collider1 != null && collider1.Active)
                {
                    collider1.CollisionData.reset();

                    foreach (var entity in ViewManager.CurrentView.Entities.get_active())
                    {
                        Collider collider2 = entity.get_component<Collider>();
                        if (collider2 != null && collider2.Active)
                            update_physics_on_colliders(collider1, collider2);
                    }
                }

                Parent.Transform = Parent.Transform + new Point3D((int)(_velocity.a * time_scale), (int)(_velocity.b * time_scale));
            }
        }
    }
}
