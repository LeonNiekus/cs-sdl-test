using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public static class Events
    {
        public class Event
        {
            public bool handled;
            public EventType event_type;

            public Event(bool handled, EventType event_type)
            {
                this.handled = handled;
                this.event_type = event_type;
            }
        }

        public class EventCallback
        {
            private static ulong _uid;
            public Action<Event> callback;
            public ulong cuid;

            public EventCallback()
            {
                callback = null;
                cuid = 0;
            }

            public EventCallback(Action<Event> callback)
            {
                this.callback = callback;
                cuid = _uid++;
            }

            public static bool operator ==(EventCallback a, EventCallback b) { return a.cuid == b.cuid; }
            public static bool operator !=(EventCallback a, EventCallback b) { return a.cuid != b.cuid; }
        }

        public class MouseEvent : Event
        {
            public Point position;
            public Input.MouseButton button;
            public bool is_pressed;

            public MouseEvent(bool handled, EventType event_type, Point position, Input.MouseButton button, bool is_pressed) : base(handled, event_type)
            {
                this.position = position;
                this.button = button;
                this.is_pressed = is_pressed;
            }
        }

        public class CollisionEvent : Event 
        {
            // TODO

            public CollisionEvent(bool handled, EventType event_type) : base(handled, event_type) { }
        }

        public class KeyEvent : Event
        {
            public Input.KeyCode key;
            public bool is_pressed;

            public KeyEvent(bool handled, EventType event_type, Input.KeyCode key, bool is_pressed) : base(handled, event_type)
            {
                this.key = key;
                this.is_pressed = is_pressed;
            }
        }
    }
}
