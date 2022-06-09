using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public class Camera : Entity
    {
        private Colour _background_colour;
        private float _aspect_w;
        private float _aspect_h;
        private View _view;
        private Entity _lock_target = null;
        private bool _always_center;

        public Camera(string name, Colour background_colour, float aspect_w, float aspect_h) : base(name)
        {
            _background_colour = background_colour;
            _aspect_w = aspect_w;
            _aspect_h = aspect_h;
            _always_center = false;
        }

        public Colour BackgroundColour { get => _background_colour; set => _background_colour = value; }
        public float AspectWidth { get => _aspect_w; set => _aspect_w = value; }
        public float AspectHeight { get => _aspect_h; set => _aspect_h = value; }
        public View View { get => _view; set => _view = value; }

        // TODO: define world to local functions

        public void lock_on(Entity entity, bool always_center)
        {
            _always_center = always_center;
            _lock_target = entity;
        }

        public void release_lock()
        {
            _lock_target = null;
        }
    }
}
