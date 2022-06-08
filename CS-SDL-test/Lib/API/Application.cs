using System;
using CS_SDL_test.Lib.Core;
using CS_SDL_test.Lib.Rendering;

namespace CS_SDL_test.Lib.API
{
    public class Application
    {
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

            Input.init_listeners();

            EventManager.Instance.register_event_handler(
                EventType.WINDOW_CLOSE,
                new Events.EventCallback((Events.Event _) => 
                {
                    _running = false;
                })
            );

            while (_running)
            {
                EventManager.Instance.poll_and_handle_events();

                if (Input.get_key_down(Input.KeyCode.ESCAPE)) _running = false;

                renderer.set_render_draw_colour(Colour.black());
                renderer.set_render_clear();

                foreach (Entity entity in Resources.get_all_entities())
                {
                    foreach (Component component in entity.Components)
                    {
                        if (component is Sprite sprite)
                        {
                            renderer.render_image(sprite.FilePath, entity.Transform);
                        }
                        else if (component is Script script)
                        {
                            script.on_frame_tick();
                        }
                    }
                }

                renderer.set_render_present();
                window.delay(10);
            }
        }
    }
}
