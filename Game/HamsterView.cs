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
        public HamsterView()
        {
            var hamster = Resources.create_entity("hamster");
            var sprite = new Sprite(".\\assets\\hamster.png", new Rect(400, 400));
            hamster.add_component(sprite);
            var script = new HamsterScript(400, 400);
            hamster.add_component(script);
        }
    }
}
