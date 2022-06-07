using System;
using CS_SDL_test.Lib;
using CS_SDL_test.Lib.Rendering;

namespace API
{
    public class WindowHandle
    {
        private static WindowHandle _instance = null;

        private WindowHandle() {}

        public static WindowHandle Instance
        {
            get
            {
                if (_instance == null) _instance = new WindowHandle();
                return _instance;
            }
        }

        public void set_title(string title)
        {
            Window.Instance.set_title(title);
        }

        public void set_size(Rect rect)
        {
            Window.Instance.set_size(rect);
        }

        public void set_resizable(bool resizable)
        {
            Window.Instance.set_resizable(resizable);
        }

        public void set_position(Rect rect)
        {
            Window.Instance.set_position(rect);
        }

        public void set_brightness(float brightness)
        {
            Window.Instance.set_brightness(brightness);
        }

        public void set_fullscreen_mode(WINDOW_MODE mode)
        {
            Window.Instance.set_fullscreen_mode(mode);
        }

        public void maximize()
        {
            Window.Instance.maximize();
        }

        public void minimize()
        {
            Window.Instance.minimize();
        }

        public void raise()
        {
            Window.Instance.raise();
        }

        public void restore()
        {
            Window.Instance.restore();
        }

        public void set_icon(string icon_path)
        {
            Window.Instance.set_icon(icon_path);
        }

        public string get_title()
        {
            return Window.Instance.get_title();
        }

        public int get_height()
        {
            return Window.Instance.get_height();
        }

        public int get_width()
        {
            return Window.Instance.get_width();
        }
    }
}
