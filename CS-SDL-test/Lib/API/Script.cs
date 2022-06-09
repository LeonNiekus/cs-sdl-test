using CS_SDL_test.Lib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public abstract class Script : Component
    {
        private Events.EventCallback _key_down_callback;
        private Events.EventCallback _key_up_callback;
        private Events.EventCallback _mouse_button_pressed_callback;
        private Events.EventCallback _mouse_button_released_callback;
        private Events.EventCallback _mouse_moved_callback;
        private Events.EventCallback _collision_callback;

        public Script() : base()
        {
            init_listeners();
        }

        ~Script()
        {
            def_listeners();
        }

        private void init_listeners() 
        {
            _key_down_callback = new Events.EventCallback((Events.Event e) => 
            {
                if (Parent.Active && Active) on_key_pressed((Events.KeyEvent)e);
            });
            EventManager.Instance.register_event_handler(EventType.KEY_DOWN, _key_down_callback);

            _key_up_callback = new Events.EventCallback((Events.Event e) =>
            {
                if (Parent.Active && Active) on_key_released((Events.KeyEvent)e);
            });
            EventManager.Instance.register_event_handler(EventType.KEY_UP, _key_up_callback);

            _mouse_button_released_callback = new Events.EventCallback((Events.Event e) =>
            {
                if (Parent.Active && Active) on_mouse_released((Events.MouseEvent)e);
            });
            EventManager.Instance.register_event_handler(EventType.MOUSE_BUTTON_RELEASED, _mouse_button_released_callback);

            _mouse_button_pressed_callback = new Events.EventCallback((Events.Event e) =>
            {
                if (Parent.Active && Active) on_mouse_pressed((Events.MouseEvent)e);
            });
            EventManager.Instance.register_event_handler(EventType.MOUSE_BUTTON_PRESSED, _mouse_button_pressed_callback);

            _mouse_moved_callback = new Events.EventCallback((Events.Event e) =>
            {
                if (Parent.Active && Active) on_mouse_moved((Events.MouseEvent)e);
            });
            EventManager.Instance.register_event_handler(EventType.MOUSE_MOVED, _mouse_moved_callback);

            _collision_callback = new Events.EventCallback((Events.Event e) =>
            {
                var ev = (Events.CollisionEvent) e;
                if (!(Parent != null && Parent.Active) || !Active) return;
 
                // TODO: also call function in other collision target

                on_collision_enter((Events.CollisionEvent)e);
            });
            EventManager.Instance.register_event_handler(EventType.COLLISION, _collision_callback);
        }

        private void def_listeners()
        {
            EventManager.Instance.unregister_event_handler(EventType.KEY_DOWN, _key_down_callback);
            EventManager.Instance.unregister_event_handler(EventType.KEY_UP, _key_up_callback);
            EventManager.Instance.unregister_event_handler(EventType.MOUSE_BUTTON_RELEASED, _mouse_button_released_callback);
            EventManager.Instance.unregister_event_handler(EventType.MOUSE_BUTTON_PRESSED, _mouse_button_pressed_callback);
            EventManager.Instance.unregister_event_handler(EventType.MOUSE_MOVED, _mouse_moved_callback);
            EventManager.Instance.unregister_event_handler(EventType.COLLISION, _collision_callback);
        }

        protected override void parent_changed()
        {
            if (Parent != null) on_create();
        }

        public virtual void on_create() { }

        public virtual void on_frame_tick() { }

        public virtual void on_collision_enter(Events.CollisionEvent collision) { }

        public virtual void on_mouse_moved(Events.MouseEvent e) { }

        public virtual void on_mouse_pressed(Events.MouseEvent e) { }

        public virtual void on_mouse_released(Events.MouseEvent e) { }

        public virtual void on_key_pressed(Events.KeyEvent e) { }

        public virtual void on_key_released(Events.KeyEvent e) { }
    }
}
