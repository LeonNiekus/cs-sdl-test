using System;
using SDL2;

namespace CS_SDL_test.Lib.Rendering
{
    public class Window
    {
        public void create_window(string? title, Rect rect, uint flags)
        {
            SDL.SDL_CreateWindow(title ?? "New Window", rect.x, rect.y, rect.w, rect.h, (SDL.SDL_WindowFlags)flags);
        }
    }
}
