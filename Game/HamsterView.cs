using API;
using CS_SDL_test.Lib;
using CS_SDL_test.Lib.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class HamsterView : View
    {
        public HamsterView() : base() { }
        public HamsterView(string view_name, Rect view_size) : base(view_name, view_size) { }

        public override void on_update()
        {
        }

        public override void on_create()
        {
            var hamster = Resources.create_entity("hamster");
            hamster.set_active(true);
            var hamster_sprite = new Sprite(".\\assets\\hamster.png", new Rect(400, 400))
            {
                Layer = 2
            };
            hamster.add_component(hamster_sprite);
            hamster.add_component(new PhysicsBody(4, 2, true));
            hamster.add_component(new HamsterScript(400, 400));
            add_entity(hamster);
            ViewManager.get_active_camera().Target = hamster;

            var sad_man = Resources.create_entity("sad_man", "sad", 1, new Point3D(300, 300));
            sad_man.set_active(true);
            var sad_man_sprite = new Sprite(".\\assets\\sad.png", new Rect(200, 171))
            {
                Layer = 3
            };
            sad_man.add_component(sad_man_sprite);
            sad_man.add_component(new SadManScript(200, 171));
            add_entity(sad_man);

            var cam_plc = Resources.create_entity("Camera Handle", "cmhndl", 0, new Point3D(0, 0));
            cam_plc.set_active(true);
            cam_plc.add_component(new CameraMoveScript());
            add_entity(cam_plc);
        }

        public override void on_destroy()
        {
        }

        public override void on_activate()
        {
        }

        public override void on_deactivate()
        {
        }

        public override void on_load()
        {
        }

        public override void on_unload()
        {
        }
    }
}
