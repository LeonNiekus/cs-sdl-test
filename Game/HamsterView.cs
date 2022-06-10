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
            hamster.add_component(new PhysicsBody(10, 2, true));
            hamster.add_component(new HamsterScript(400, 400));
            add_entity(hamster);

            var sad_man = Resources.create_entity("sad_man", "sad", 1, new Point(300, 300));
            sad_man.set_active(true);
            sad_man.add_component(new Sprite(".\\assets\\sad.png", new Rect(200, 171)));
            sad_man.add_component(new SadManScript(200, 171));
            add_entity(sad_man);
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
