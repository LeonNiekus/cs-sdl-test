using System;
using CS_SDL_test.Lib.Core;
using CS_SDL_test.Lib.Rendering;
using SDL2;

namespace CS_SDL_test.Lib.API
{
    internal class Application
    {
        public const long FRAME_DELAY = 16;
        private bool _running;

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
            window.set_hint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "2");

            Input.init_listeners();

            Time.calculate_delta_time();

            uint total_frame_ticks = 0;
            uint total_frames = 0;
            uint start_time = Window.Ticks;

            EventManager.Instance.register_event_handler(
                EventType.WINDOW_CLOSE,
                new Events.EventCallback((Events.Event _) => 
                {
                    _running = false;
                })
            );

            long last_tick_time = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            while (_running)
            {
                ++total_frames;
                uint start_ticks = Window.Ticks;

                EventManager.Instance.poll_and_handle_events();

                if (DateTimeOffset.Now.ToUnixTimeMilliseconds() > last_tick_time + FRAME_DELAY)
                {
                    if (ViewManager.CurrentView != null)
                    {
                        ViewManager.CurrentView.on_update();

                        foreach (Entity entity in ViewManager.CurrentView.Entities.get_active())
                        {
                            foreach (Component component in entity.Components)
                            {
                                if (component is PhysicsBody physics_body)
                                {
                                    physics_body.update_physics();
                                }
                                else if (component is Script script)
                                {
                                    script.on_frame_tick();
                                }
                            }
                        }
                    }

                    last_tick_time = DateTimeOffset.Now.ToUnixTimeMilliseconds() + FRAME_DELAY;
                }

                renderer.set_render_draw_colour(Colour.black());
                renderer.set_render_clear();

                if (ViewManager.CurrentView != null)
                    ViewManager.CurrentView.render_view(ViewManager.get_active_camera());

                renderer.set_render_present();

                uint end_ticks = Window.Ticks;
                float frame_time = (end_ticks - start_ticks) / 1000.0f;
                total_frame_ticks += end_ticks - start_ticks;

                Time.fps = 1.0f / frame_time;
                Time.average_fps = 1000.0f / (total_frame_ticks / total_frames);
            }
        }

        public virtual void request_close()
        {
            _running = false;
        }
    }
}
