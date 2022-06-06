using System;
using SDL2;

namespace CS_SDL_test.Lib.Rendering
{
    public class Renderer
    {
        private static Renderer _instance = null;
        private IntPtr _pRenderer = IntPtr.Zero;

        private Renderer() {}

        public static Renderer Instance
        {
            get
            {
                if (_instance == null) _instance = new Renderer();
                return _instance;
            }
        }

        public IntPtr RawPointer { get => _pRenderer; }

        public void create_renderer(uint flags)
        {
            destroy_renderer();
            _pRenderer = SDL.SDL_CreateRenderer(Window.Instance.RawPointer, -1, (SDL.SDL_RendererFlags)flags);
        }

        public void destroy_renderer()
        {
            if (_pRenderer == IntPtr.Zero) return;

            SDL.SDL_DestroyRenderer(_pRenderer);
            _pRenderer = IntPtr.Zero;
        }
    }
}
