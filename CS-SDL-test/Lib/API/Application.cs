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
            window.create_window("Super coole game", new Rect(250, 250, 500, 500), 0);

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
            renderer.render_image(".\\assets\\hamster.png", new Rect(0, 0, 0, 0));
            renderer.set_render_present();

            int i = 0;
            while (_running)
            {
                // do main loop stuff
                window.delay(2000);
                _running = false;
            }
        }
    }
}
