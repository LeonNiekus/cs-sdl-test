﻿using API;
using CS_SDL_test.Lib;
using CS_SDL_test.Lib.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Game_EntryPoint : EntryPoint
    {
        static void Main(string[] args)
        {
            var game = new TestGame(new API_Application());

            WindowHandle window = WindowHandle.Instance;
            window.set_size(new Rect(1080, 720));
            window.set_title("test pizza");
            window.set_icon(".\\assets\\hamster.png");

            exec(game.Application);
        }
    }
}
