using CS_SDL_test.Lib.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.Core
{
    public class EventManager
    {
        private static EventManager _instance = null;
        private InputEvents _inputEvents = null;

        private Dictionary<EventType, List<Events.EventCallback>> _callbacks = new();

        private EventManager() { _inputEvents = new InputEvents(); }

        public static EventManager Instance
        {
            get
            {
                return _instance ??= new EventManager();
            }
        }

        public void poll_and_handle_events()
        {
            try
            {
                for (;;)
                {
                    generate_event(_inputEvents.poll_events());
                }
            }
            catch (NoEventsFoundException)
            {
                return;
            }          
        }

        public void generate_event(Events.Event ev)
        {
            if (_callbacks.ContainsKey(ev.event_type))
            {
                List<Events.EventCallback> filtered = _callbacks[ev.event_type];
                foreach (var filter_ev in filtered) filter_ev.callback(ev);
            }
        }

        public void register_event_handler(EventType type, Events.EventCallback cb)
        {
            if (!_callbacks.ContainsKey(type)) _callbacks.Add(type, new List<Events.EventCallback>());
            _callbacks[type].Add(cb);
        }

        public void unregister_event_handler(EventType type, Events.EventCallback cb)
        {
            if (_callbacks.ContainsKey(type))
            {
                List<Events.EventCallback> filtered = _callbacks[type];
                List<Events.EventCallback> deleteQueue = new();

                foreach (var filter_ev in filtered)
                    if (filter_ev == cb)
                        deleteQueue.Add(filter_ev);

                foreach (var delete_item in deleteQueue)
                    _callbacks[type].Remove(delete_item);
            }
            else
            {
                throw new IndexOutOfRangeException("Event type does not exist in callback queue");
            }
        }

        public void unregister_all_event_handlers()
        {
            _callbacks.Clear();
        }
    }
}
