using System;
using CS_SDL_test.Lib.API;
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

        public void render_sprite(Sprite sprite, Point position)
        {
            SDL.SDL_Rect dst_rect;
            dst_rect.x = position.x;
            dst_rect.y = position.y;
            dst_rect.w = sprite.Dimensions.w;
            dst_rect.h = sprite.Dimensions.h;

            SDL.SDL_Point point = new();
            point.x = dst_rect.x + (dst_rect.w / 2);
            point.y = dst_rect.y + (dst_rect.h / 2);

            SDL.SDL_RendererFlip flip;

            if (sprite.FlipX && !sprite.FlipY) flip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL;
            else if (!sprite.FlipX && sprite.FlipY) flip = SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
            else if (sprite.FlipX && sprite.FlipY) flip = SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL;
            else flip = SDL.SDL_RendererFlip.SDL_FLIP_NONE;

            SDL.SDL_Rect src_rect;
            src_rect.x = sprite.Dimensions.x;
            src_rect.y = sprite.Dimensions.y;
            src_rect.w = sprite.Dimensions.w;
            src_rect.h = sprite.Dimensions.h;

            IntPtr surface = SDL_image.IMG_Load(sprite.FilePath);
            IntPtr texture = SDL.SDL_CreateTextureFromSurface(_pRenderer, surface);
            SDL.SDL_FreeSurface(surface);

            SDL.SDL_RenderCopyEx(_pRenderer, texture, ref src_rect, ref dst_rect, sprite.Rotation, ref point, flip);
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
