using System;
using CS_SDL_test.Lib.Core;
using CS_SDL_test.Lib.Rendering;
using SDL2;

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

            int factor_x = 0, factor_y = 0;
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
                            renderer.render_image(sprite.FilePath, sprite.Position);
                            sprite.Position = bounce(sprite.Position, ref factor_x, ref factor_y);
                        }
                    }
                }

                renderer.set_render_present();
                window.delay(10);
            }
        }

        // TODO: remove temp function!
        private Rect bounce(Rect in_rect, ref int factor_x, ref int factor_y)
        {
            if (in_rect.x2() >= 1080) factor_x = -5;
            else if (in_rect.x <= 0) factor_x = 5;

            if (in_rect.y2() >= 720) factor_y = -5;
            else if (in_rect.y <= 0) factor_y = 5;

            in_rect.transform(factor_x, factor_y);
            return in_rect;
        }
    }
}
