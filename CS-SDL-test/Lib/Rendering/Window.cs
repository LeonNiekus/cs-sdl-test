using System;
using SDL2;

namespace CS_SDL_test.Lib.Rendering
{
    internal class Window
    {
        #region internal
        private static Window _instance = null;
        private IntPtr _pWindow = IntPtr.Zero;

        private Window() {}

        public static Window Instance
        {
            get 
            {
                if (_instance == null) _instance = new Window();
                return _instance;
            }
        }

        public IntPtr RawPointer { get => _pWindow; }

        public static uint Ticks { get => SDL.SDL_GetTicks(); }

        public void init_sdl()
        {
            uint flags = SDL.SDL_INIT_TIMER | SDL.SDL_INIT_AUDIO | SDL.SDL_INIT_VIDEO | SDL.SDL_INIT_EVENTS |
                       SDL.SDL_INIT_JOYSTICK | SDL.SDL_INIT_HAPTIC | SDL.SDL_INIT_GAMECONTROLLER;
            SDL.SDL_Init(flags);
        }

        public void create_window(string? title, Rect rect, uint flags)
        {
            _pWindow = SDL.SDL_CreateWindow(title ?? "New Window", rect.x, rect.y, rect.w, rect.h, (SDL.SDL_WindowFlags)flags);
        }

        public void close_window()
        {
            SDL.SDL_DestroyWindow(_pWindow);
            _pWindow = IntPtr.Zero;
            SDL.SDL_Quit();
        }

        public void delay(uint ms)
        {
            SDL.SDL_Delay(ms);
        }
        #endregion
    }
}
