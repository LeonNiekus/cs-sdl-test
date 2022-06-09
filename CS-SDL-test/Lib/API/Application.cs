using System;
using CS_SDL_test.Lib.Core;
using CS_SDL_test.Lib.Rendering;

namespace CS_SDL_test.Lib.API
{
    public class Application
    {
        private bool _running;
        private int _previous_frames = 0;

        public Application(bool running = true)
        {
            _running = running;
            if (!_running) return;

            Window window = Window.Instance;
            window.init_sdl();
            window.create_window(null, new Rect(250, 250, 500, 500), 0);

            Renderer renderer = Renderer.Instance;
            renderer.create_renderer(2);
        }

        ~Application()
        {
            Renderer renderer = Renderer.Instance;
            renderer.destroy_renderer();

            Window window = Window.Instance;
            window.close_window();
        }

        public void run()
        {
            Window window = Window.Instance;
            Renderer renderer = Renderer.Instance;

            Input.init_listeners();

            Time.calculate_delta_time();

            uint total_frame_ticks = 0;
            uint total_frames = 0;

            EventManager.Instance.register_event_handler(
                EventType.WINDOW_CLOSE,
                new Events.EventCallback((Events.Event _) => 
                {
                    _running = false;
                })
            );

            while (_running)
            {
                Time.calculate_delta_time();
                total_frames++;
                uint start_ticks = Window.Ticks;

                EventManager.Instance.poll_and_handle_events();

                if (Input.get_key_down(Input.KeyCode.ESCAPE)) _running = false;

                renderer.set_render_draw_colour(Colour.black());
                renderer.set_render_clear();

                if (ViewManager.CurrentView != null)
                {
                    ViewManager.CurrentView.on_update();

                    foreach (Entity entity in ViewManager.CurrentView.Entities.get_active())
                    {
                        foreach (Component component in entity.Components)
                        {
                            if (component is Script script)
                            {
                                script.on_frame_tick();
                            }
                        }
                    }

                    var cur_view = ViewManager.CurrentView;
                    var active_camera = ViewManager.get_active_camera();

                    cur_view.render_view(active_camera);
                }

                renderer.set_render_present();

                uint cap_end_ticks = Window.Ticks;
                float cap_time = cap_end_ticks - start_ticks;
                if (cap_time < 16.666f) window.delay((uint)(16.666f - cap_time));

                uint end_ticks = Window.Ticks;
                float frame_time = (end_ticks - start_ticks) / 1000.0f;
                total_frame_ticks += end_ticks - start_ticks;

                Time.fps = 1.0f / frame_time;
                Time.average_fps = 1000.0f / (total_frame_ticks / total_frames);
                _previous_frames = (int)Time.fps;

                // Temporary test prints
                Console.Clear();
                Console.WriteLine(Time.fps);
            }
        }
    }
}
