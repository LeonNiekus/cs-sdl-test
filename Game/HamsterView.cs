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
            hamster.add_component(new Sprite(".\\assets\\hamster.png", new Rect(400, 400)));
            hamster.add_component(new HamsterScript(400, 400));
            hamster.get_component<Sprite>().Active = true;
            hamster.get_component<HamsterScript>().Active = true;
            add_entity(hamster);
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
