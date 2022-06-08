using System;
using SDL2;

namespace CS_SDL_test.Lib.Rendering
{
    internal class Renderer
    {
        private static Renderer _instance = null;
        private IntPtr _pRenderer = IntPtr.Zero;

        private Renderer() {}

        public static Renderer Instance
        {
            get
            {
                return _instance ??= new Renderer();
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

        public void set_render_draw_colour(Colour colour)
        {
            SDL.SDL_SetRenderDrawColor(_pRenderer, colour.r, colour.g, colour.b, colour.a);
        }

        public void set_render_clear()
        {
            SDL.SDL_RenderClear(_pRenderer);
        }

        public void set_render_present()
        {
            SDL.SDL_RenderPresent(_pRenderer);
        }

        public void render_image(string file_path, Point position)
        {
            IntPtr surface = SDL_image.IMG_Load(file_path);
            IntPtr texture = SDL.SDL_CreateTextureFromSurface(_pRenderer, surface);
            SDL.SDL_FreeSurface(surface);

            int sw, sh;
            SDL.SDL_QueryTexture(texture, out _, out _, out sw, out sh);

            SDL.SDL_Rect dst_rect;
            dst_rect.x = position.x;
            dst_rect.y = position.y;
            dst_rect.w = sw;
            dst_rect.h = sh;

            // TODO: temp values
            SDL.SDL_Rect src_rect;
            src_rect.x = 0;
            src_rect.y = 0;
            src_rect.w = sw;
            src_rect.h = sh;

            SDL.SDL_RenderCopy(_pRenderer, texture, ref src_rect, ref dst_rect); // TODO: change to RenderCopyEx
            SDL.SDL_DestroyTexture(texture);
        }

        public void render_rectangle_outline(Rect rect, Colour colour)
        {
            set_render_draw_colour(colour);
            SDL.SDL_FRect s_rect;
            s_rect.x = rect.x;
            s_rect.y = rect.y;
            s_rect.w = rect.w;
            s_rect.h = rect.h;
            SDL.SDL_RenderFillRectF(_pRenderer, ref s_rect);
        }

        public void render_rectangle(Rect rect, Colour colour)
        {
            set_render_draw_colour(colour);
            SDL.SDL_FRect s_rect;
            s_rect.x = rect.x;
            s_rect.y = rect.y;
            s_rect.w = rect.w;
            s_rect.h = rect.h;
            SDL.SDL_RenderDrawRectF(_pRenderer, ref s_rect);
        }

        public void render_point(Point point, Colour colour)
        {
            set_render_draw_colour(colour);
            SDL.SDL_RenderDrawPointF(_pRenderer, point.x, point.y);
        }

        public void render_line()
        {

        }
    }
}
