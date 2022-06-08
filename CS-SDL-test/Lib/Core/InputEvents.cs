using CS_SDL_test.Lib.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using System.Runtime.InteropServices;

namespace CS_SDL_test.Lib.Core
{
    public class InputEvents
    {
        public InputEvents() { }

        private static bool get_mouse_state(ref int x, ref int y, Input.MouseButton button = Input.MouseButton.LEFT)
        {
            Point position = new();
            SDL.SDL_PumpEvents();

            var state = SDL.SDL_GetMouseState(out position.x, out position.y);
            x = position.x;
            y = position.y;

            return (state & SDL.SDL_BUTTON((uint)button)) != 0;
        }

        public Events.Event poll_events()
        {
            SDL.SDL_Event ev;
            if (0 < SDL.SDL_PollEvent(out ev))
            {
                switch (ev.type)
                {
                    case SDL.SDL_EventType.SDL_WINDOWEVENT:
                        if (ev.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED) return new Events.Event(false, EventType.WINDOW_RESIZE);
                        if (ev.window.windowEvent == SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE) return new Events.Event(false, EventType.WINDOW_CLOSE);
                        break;

                    case SDL.SDL_EventType.SDL_KEYDOWN:
                        return new Events.KeyEvent(false, EventType.KEY_DOWN, (Input.KeyCode)ev.key.keysym.scancode, true);

                    case SDL.SDL_EventType.SDL_KEYUP:
                        return new Events.KeyEvent(false, EventType.KEY_UP, (Input.KeyCode)ev.key.keysym.scancode, false);

                    case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN:
                        if (ev.button.button > SDL.SDL_BUTTON_RIGHT) break;
                        return new Events.MouseEvent(false, EventType.MOUSE_BUTTON_PRESSED, new Point(ev.button.x, ev.button.y), (Input.MouseButton)ev.button.button, true);

                    case SDL.SDL_EventType.SDL_MOUSEBUTTONUP:
                        if (ev.button.button > SDL.SDL_BUTTON_RIGHT) break;
                        return new Events.MouseEvent(false, EventType.MOUSE_BUTTON_RELEASED, new Point(ev.button.x, ev.button.y), (Input.MouseButton)ev.button.button, false);

                    case SDL.SDL_EventType.SDL_MOUSEMOTION:
                        return new Events.MouseEvent(false, EventType.MOUSE_MOVED, new Point(ev.button.x, ev.button.y), (Input.MouseButton)ev.button.button, false);

                    case SDL.SDL_EventType.SDL_MOUSEWHEEL:
                        return new Events.MouseEvent(false, EventType.MOUSE_SCROLLED, new Point(ev.button.x, ev.button.y), (Input.MouseButton)ev.button.button, false);

                    default:
                        break;
                }

                return new Events.Event(false, EventType.NONE);
            }
            throw new KeyNotFoundException("No more events in queue");
        }

        public static bool get_key_state(Input.KeyCode key)
        {
            SDL.SDL_PumpEvents();

            byte[] raw_states = new byte[240];
            IntPtr state = SDL.SDL_GetKeyboardState(out int num_keys);
            Marshal.Copy(state, raw_states, 0, 240);

            if (num_keys < (int)Input.KeyCode.KEYCODE_END - 1) throw new Exception("Unable to return all key states");

            return raw_states[(int)key] > 0;
        }

        public static bool get_mouse_button_state(Input.MouseButton button)
        {
            int _ = 0;
            return get_mouse_state(ref _, ref _, button);
        }

        public static Point mouse_position()
        {
            Point pos = new();
            get_mouse_state(ref pos.x, ref pos.y);
            return pos;
        }
    }
}
