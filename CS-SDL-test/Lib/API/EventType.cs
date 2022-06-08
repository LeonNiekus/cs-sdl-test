using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public enum EventType
    {
        NONE,
        WINDOW_CLOSE,
        WINDOW_RESIZE,
        KEY_UP,
        KEY_DOWN,
        MOUSE_BUTTON_PRESSED,
        MOUSE_BUTTON_RELEASED,
        MOUSE_MOVED,
        MOUSE_SCROLLED,
        COLLISION
    }
}
