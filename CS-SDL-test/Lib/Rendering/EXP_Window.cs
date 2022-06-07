using System;
using SDL2;

namespace CS_SDL_test.Lib.Rendering
{
    public static class EXP_Window
    {
        public static void set_title(string title)
        {
            SDL.SDL_SetWindowTitle(Window.Instance.RawPointer, title);
        }

        public static void set_size(Rect rect)
        {
            SDL.SDL_SetWindowSize(Window.Instance.RawPointer, rect.w, rect.h);
        }

        public static void set_resizable(int resizable)
        {
            SDL.SDL_SetWindowResizable(Window.Instance.RawPointer, (SDL.SDL_bool)resizable);
        }

        public static void set_position(Rect rect)
        {
            SDL.SDL_SetWindowPosition(Window.Instance.RawPointer, rect.x, rect.y);
        }

        public static void set_brightness(float brightness)
        {
            SDL.SDL_SetWindowBrightness(Window.Instance.RawPointer, brightness);
        }

        public static void set_fullscreen_mode(WINDOW_MODE mode)
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

            SDL.SDL_SetWindowFullscreen(Window.Instance.RawPointer, flag);
        }

        public static void maximize()
        {
            SDL.SDL_MaximizeWindow(Window.Instance.RawPointer);
        }

        public static void minimize()
        {
            SDL.SDL_MinimizeWindow(Window.Instance.RawPointer);
        }

        public static void raise()
        {
            SDL.SDL_RaiseWindow(Window.Instance.RawPointer);
        }

        public static void restore()
        {
            SDL.SDL_RestoreWindow(Window.Instance.RawPointer);
        }

        public static void set_icon(string icon_path)
        {
            IntPtr surface = IntPtr.Zero;
            try
            {
                surface = SDL_image.IMG_Load(icon_path);
                if (surface == IntPtr.Zero) throw new NullReferenceException("Unable to load image data into memory. Image: " + icon_path);

                SDL.SDL_SetWindowIcon(Window.Instance.RawPointer, surface);
                SDL.SDL_FreeSurface(surface);
            }
            catch (Exception)
            {
                if (surface != IntPtr.Zero) SDL.SDL_FreeSurface(surface);
            }
        }

        public static string get_title()
        {
            return SDL.SDL_GetWindowTitle(Window.Instance.RawPointer);
        }

        public static int get_height()
        {
            SDL.SDL_GetWindowSize(Window.Instance.RawPointer, out _, out int h);
            return h;
        }

        public static int get_width()
        {
            SDL.SDL_GetWindowSize(Window.Instance.RawPointer, out int w, out _);
            return w;
        }
    }
}
