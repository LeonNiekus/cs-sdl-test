using System;
using CS_SDL_test.Lib;
using CS_SDL_test.Lib.Rendering;

namespace API
{
    public class _Window
    {
        public void test()
        {
            Window window = new(); // TODO: make singleton/static
            window.create_window(null, new Rect(0, 5, 10, 10), 20);
        }
    }
}
