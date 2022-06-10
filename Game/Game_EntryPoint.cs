using API;
using CS_SDL_test.Lib;
using CS_SDL_test.Lib.API;
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
        private static TestGame _game;

        static void Main(string[] args)
        {
            _game = new TestGame(new API_Application());

            WindowHandle window = WindowHandle.Instance;
            window.set_size(new Rect(1080, 720));
            window.set_title("test pizza");
            window.set_icon(".\\assets\\hamster.png");

            exec(_game.Application);
        }

        public static TestGame get_game()
        {
            return _game;
        }
    }
}
