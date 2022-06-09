using CS_SDL_test.Lib.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public static class ViewManager
    {
        private static List<View> _views = new();
        private static View _current_view = null;

        public static View CurrentView 
        { 
            get => _current_view; 
            set 
            {
                if (_current_view != null) _current_view.on_deactivate();
                _current_view = value;
                get_active_camera().View = _current_view;
                _current_view.on_activate();
            } 
        }

        private static void add_view(View view)
        {
            _views.Add(view);
        }

        public static int get_view_count()
        {
            return _views.Count();
        }

        public static T create_view<T>(Rect view_size, string view_name) where T : View
        {
            T view = (T)Activator.CreateInstance(typeof(T), new object[] { view_name, view_size });

            add_view(view);

            if (get_view_count() == 1)
            {
                int window_w = Math.Min(Window.Instance.get_width(), view_size.w);
                int window_h = Math.Min(Window.Instance.get_height(), view_size.h);
                var camera = Resources.create_camera("main_camera", Colour.black(), window_w, window_h);
                camera.set_active(true);

                view.on_create();
                _current_view = view;
            }
            else
            {
                view.on_create();
            }

            return view;
        }

        public static void destroy_view(View view)
        {
            foreach (var c_view in _views)
            {
                if (c_view == view)
                {
                    _views.Remove(c_view);
                    if (_views.Count() > 0 && _current_view == c_view)
                        _current_view = _views.Last();
                    else if (_views.Count == 0)
                    {
                        c_view.on_deactivate();
                        _current_view = null;
                    }

                    c_view.on_destroy();
                    return;
                }
            }
        }

        public static void destroy_all_views()
        {
            _current_view = null;
            foreach (var view in _views) view.on_destroy();
            _views.Clear();
        }

        public static void load_view(View view)
        {
            view.on_load();
        }

        public static void unload_view(View view)
        {
            view.on_unload();
        }

        public static View get_view_by_name(string name)
        {
            foreach (var view in _views) if (view.Name == name) return view;
            return null;
        }

        public static Camera get_active_camera()
        {
            return Entity.find_entity_of_type<Camera>(true);
        }

        public static void move_entity_to_view(Entity entity, View view)
        {
            var cur_view = entity.View;
            if (cur_view != null)
            {
                cur_view.remove_entity(entity);
                view.add_entity(entity);
            }
            else
            {
                view.add_entity(entity);
            }
        }
    }
}
