using CS_SDL_test.Lib;
using CS_SDL_test.Lib.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class SadManScript : Script
    {
        private int factor_x = 5, factor_y = -5, sprite_w, sprite_h;

        public SadManScript(int sprite_w, int sprite_h) : base()
        {
            this.sprite_w = sprite_w;
            this.sprite_h = sprite_h;
        }

        public override void on_frame_tick()
        {
            bounce();
        }

        private void bounce()
        {
            Point3D parent_pos = Parent.Transform;

            if (parent_pos.x + sprite_w >= 1080) factor_x = -5;
            else if (parent_pos.x <= 0) factor_x = 5;

            if (parent_pos.y + sprite_h >= 720) factor_y = -5;
            else if (parent_pos.y <= 0) factor_y = 5;

            parent_pos.transform(factor_x, factor_y, 0);
            Parent.Transform = parent_pos;
        }
    }
}
