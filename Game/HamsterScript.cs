﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CS_SDL_test.Lib;
using CS_SDL_test.Lib.API;

namespace Game
{
    public class HamsterScript : Script
    {
        private int factor_x = 0, factor_y = 0, sprite_w, sprite_h;

        public HamsterScript(int sprite_w, int sprite_h) : base() 
        {
            this.sprite_w = sprite_w;
            this.sprite_h = sprite_h;
        }

        public override void on_key_pressed(Events.KeyEvent e)
        {
            switch (e.key)
            {
                case Input.KeyCode.A:
                    move_hamster(dx: -100);
                    break;
                case Input.KeyCode.D:
                    move_hamster(dx: 100);
                    break;
                case Input.KeyCode.W:
                    move_hamster(dy: -3);
                    break;
                case Input.KeyCode.S:
                    move_hamster(dy: 3);
                    break;
                case Input.KeyCode.ESCAPE:
                    Game_EntryPoint.get_game().Application.request_close();
                    break;
            }
        }

        public override void on_create()
        {
            Debug.log("HamsterScript created!");
            Debug.log_error(new Point3D(1, 1).x);
        }

        public override void on_frame_tick()
        {
            //bounce();
        }

        private void bounce()
        {
            Point3D parent_pos = Parent.Transform;

            if (parent_pos.x + sprite_w >= 1080) factor_x = -1;
            else if (parent_pos.x <= 0) factor_x = 1;

            if (parent_pos.y + sprite_h >= 720) factor_y = -1;
            else if (parent_pos.y <= 0) factor_y = 1;

            parent_pos.transform(factor_x, factor_y, 0);
            Parent.Transform = parent_pos;
        }

        private void move_hamster(int dx = 0, int dy = 0)
        {
            var phys_body = Parent.get_component<PhysicsBody>();

            if (phys_body != null)
                phys_body.add_force(new FloatContainer(dx, dy));
        }
    }
}
