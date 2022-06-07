using System;
using SDL2;

namespace CS_SDL_test.Lib.Rendering
{
    internal class Window
    {
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

        public void set_title(string title)
        {
            SDL.SDL_SetWindowTitle(_pWindow, title);
        }

        public void set_size(Rect rect)
        {
            SDL.SDL_SetWindowSize(_pWindow, rect.w, rect.h);
        }

        public void set_resizable(bool resizable)
        {
            int raw_bool = resizable ? 1 : 0;
            SDL.SDL_SetWindowResizable(_pWindow, (SDL.SDL_bool)raw_bool);
        }

        public void set_position(Rect rect)
        {
            SDL.SDL_SetWindowPosition(_pWindow, rect.x, rect.y);
        }

        public void set_brightness(float brightness)
        {
            SDL.SDL_SetWindowBrightness(_pWindow, brightness);
        }

        public void set_fullscreen_mode(WINDOW_MODE mode)
        {
            uint flag = 0;
            switch (mode)
            {
                case WINDOW_MODE.WINDOWED:
                    break;
                case WINDOW_MODE.FULLSCREEN:
                    flag = (uint)SDL.SDL_WindowFlags.SDL_WINDOW_FULLSCREEN;
                    break;
                case WINDOW_MODE.BORDERLESS_FULLSCREEN:
                    flag = (uint)SDL.SDL_WindowFlags.SDL_WINDOW_BORDERLESS;
                    break;
            }

            SDL.SDL_SetWindowFullscreen(_pWindow, flag);
        }

        public void maximize()
        {
            SDL.SDL_MaximizeWindow(_pWindow);
        }

        public void minimize()
        {
            SDL.SDL_MinimizeWindow(_pWindow);
        }

        public void raise()
        {
            SDL.SDL_RaiseWindow(_pWindow);
        }

        public void restore()
        {
            SDL.SDL_RestoreWindow(_pWindow);
        }

        public void set_icon(string icon_path)
        {
            IntPtr surface = IntPtr.Zero;
            try
            {
                surface = SDL_image.IMG_Load(icon_path);
                if (surface == IntPtr.Zero) throw new NullReferenceException("Unable to load image data into memory. Image: " + icon_path);

                SDL.SDL_SetWindowIcon(_pWindow, surface);
                SDL.SDL_FreeSurface(surface);
            }
            catch (Exception)
            {
                if (surface != IntPtr.Zero) SDL.SDL_FreeSurface(surface);
            }
        }

        public string get_title()
        {
            return SDL.SDL_GetWindowTitle(_pWindow);
        }

        public int get_height()
        {
            SDL.SDL_GetWindowSize(_pWindow, out _, out int h);
            return h;
        }

        public int get_width()
        {
            SDL.SDL_GetWindowSize(_pWindow, out int w, out _);
            return w;
        }
    }
}
