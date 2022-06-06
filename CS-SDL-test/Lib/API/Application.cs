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
            window.create_window(null, new Rect(500, 500), 20);

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

            while (_running)
            {
                // do main loop stuff
                window.delay(2000);
                _running = false;
            }
        }
    }
}
