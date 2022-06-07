using System;
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

            // Temporary values
            //renderer.set_render_draw_colour(new Colour(0, 255, 255));
            //renderer.render_rectangle(new Rect(50, 50, 100, 100));
            Rect dst_rect = new Rect(400, 400);
            int factor_x = 0;
            int factor_y = 0;
            for (int i = 0; i < 9999; i++)
            {
                renderer.set_render_draw_colour(Colour.black());
                renderer.set_render_clear();

                renderer.render_image(".\\assets\\hamster.png", dst_rect);
                renderer.set_render_present();

                window.delay(50);

                if (dst_rect.x2() >= 1080) factor_x = -5;
                else if (dst_rect.x <= 0) factor_x = 5;

                if (dst_rect.y2() >= 720) factor_y = -5;
                else if (dst_rect.y <= 0) factor_y = 5;

                dst_rect.transform(factor_x, factor_y);
            }

            /* while (_running)
            {
                // do main loop stuff
                window.delay(2000);
                _running = false;
            }*/
        }
    }
}
