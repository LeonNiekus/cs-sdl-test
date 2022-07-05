using CS_SDL_test.Lib.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public class Camera : Entity
    {
        private Entity _target;
        private int _scene_width; 
        private int _scene_height;
        private bool _follow_target;
        private bool _center_on_target;

        public int SceneWidth { get => _scene_width; }
        public int SceneHeight { get => _scene_height; }
        public Entity Target { get => _target; set => _target = value; }
        public bool FollowTarget { get => _follow_target; set => _follow_target = value; }
        public bool CenterOnTarget { get => _center_on_target; set => _center_on_target = value; }

        public Camera(string name, bool follow_target = true, bool center_on_target = false) : base(name)
        {
            _follow_target = follow_target;
            _center_on_target = center_on_target;
        }

        public void set_scene_size(int w, int h)
        {
            _scene_width = w;
            _scene_height = h;
        }

        public void move_x(int dx)
        {
            _transform.x += dx;
        }

        public void move_y(int dy)
        {
            _transform.y += dy;
        }

        public void move_z(int dz)
        {
            _transform.z += dz;
        }

        public Point3D to_local(Entity world_target)
        {
            Point3D obj_pt = world_target.Transform;

            if (_target != null && _target == world_target)
            {
                var sprite = world_target.get_component<Sprite>();
                if (_center_on_target)
                {
                    _transform.x = obj_pt.x - (_scene_width / 2) + (sprite != null ? sprite.Dimensions.w / 2 : 0);
                    _transform.y = obj_pt.y - (_scene_height / 2) + (sprite != null ? sprite.Dimensions.h / 2 : 0);
                }
                else if (_follow_target)
                {
                    float smooth_time = 0.1f;
                    Vector2 vel = new(0, 0);

                    FloatContainer obj_center = new
                    (
                        a: obj_pt.x - (_scene_width / 2) + (sprite != null ? sprite.Dimensions.w / 2 : 0),
                        b: obj_pt.y - (_scene_height / 2) + (sprite != null ? sprite.Dimensions.h / 2 : 0)
                    );

                    Point3D desired_cam_pos = smooth_damp(_transform, ref obj_center, ref vel, ref smooth_time, int.MaxValue, Time.delta_time);

                    _transform = desired_cam_pos;
                }
            }

            int cam_view_w_diff = ViewManager.CurrentView.Size.w - _scene_width;
            if (cam_view_w_diff < 0) throw new Exception("WorldObjectError: Camera width > View width");
            int cam_view_h_diff = ViewManager.CurrentView.Size.h - _scene_height;
            if (cam_view_h_diff < 0) throw new Exception("WorldObjectError: Camera height > View height");

            Point3D cam_pos = Transform;
            _transform.x = Math.Clamp(cam_pos.x, 0, cam_view_w_diff);
            _transform.y = Math.Clamp(cam_pos.y, 0, cam_view_h_diff);

            Point3D result;
            result.x = obj_pt.x - cam_pos.x;
            result.y = obj_pt.y - cam_pos.y;
            result.z = obj_pt.z - cam_pos.z;

            return result;
        }

        private static Point3D smooth_damp(Point3D cur, ref FloatContainer tar, ref Vector2 cur_vel, ref float smooth_time, float max_speed, float dt)
        {
            smooth_time = Math.Max(0.0001f, smooth_time);
            float omega = 2.0f / smooth_time;

            float x = omega * dt;
            float exp = 1.0f / (1.0f + x + 0.48f * x * x + 0.235f * x * x * x);

            float dx = cur.x - tar.a;
            float dy = cur.y - tar.b;
            FloatContainer orig_to = tar;

            float max_change = max_speed * smooth_time;

            float max_change_sq = max_change * max_change;
            float sq_dist = dx * dx + dy * dy;
            if (sq_dist > max_change_sq)
            {
                float mag = (float)Math.Sqrt(sq_dist);
                dx = dx / mag * max_change;
                dy = dy / mag * max_change;
            }

            tar.a = cur.x - dx;
            tar.b = cur.y - dy;

            float t_x = (cur_vel.X + omega * dx) * dt;
            float t_y = (cur_vel.Y + omega * dy) * dt;

            cur_vel.X = (cur_vel.X - omega * t_x) * exp;
            cur_vel.Y = (cur_vel.Y - omega * t_y) * exp;

            float o_x = tar.a + (dx + t_x) * exp;
            float o_y = tar.b + (dy + t_y) * exp;

            float orig_min_cur_x = orig_to.a - cur.x;
            float orig_min_cur_y = orig_to.b - cur.y;
            float out_min_orig_x = o_x - orig_to.a;
            float out_min_orig_y = o_y - orig_to.b;

            if (orig_min_cur_x * out_min_orig_x + orig_min_cur_y * out_min_orig_y > 0)
            {
                o_x = orig_to.a;
                o_y = orig_to.b;

                cur_vel.X = (o_x - orig_to.a) / dt;
                cur_vel.Y = (o_y - orig_to.b) / dt;
            }

            return new((int)o_x, (int)o_y);
        }
    }
}
