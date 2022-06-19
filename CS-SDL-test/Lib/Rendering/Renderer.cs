using System;
using CS_SDL_test.Lib.API;
using CS_OpenGL;
using SDL2;

namespace CS_SDL_test.Lib.Rendering
{
    internal class Renderer
    {
        private static Renderer _instance = null;
        private IntPtr _pRenderer = IntPtr.Zero;
        private IntPtr _pOpenGLContext = IntPtr.Zero;
        internal bool Is3D { get; set; }
        internal float RotX { get; set; }
        internal float RotY { get; set; }

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
            if (!Is3D)
            {
                destroy_renderer();
                _pRenderer = SDL.SDL_CreateRenderer(Window.Instance.RawPointer, -1, (SDL.SDL_RendererFlags)flags);
            }
            else
            {
                _pOpenGLContext = SDL.SDL_GL_CreateContext(Window.Instance.RawPointer);
                if (_pOpenGLContext == IntPtr.Zero) Debug.log_error("Unable to create OpenGLContext: " + SDL.SDL_GetError());

                SDL.SDL_GL_MakeCurrent(Window.Instance.RawPointer, _pOpenGLContext);

                SDL.SDL_GL_SetSwapInterval(1);

                OpenGL.gl_Enable((uint)OpenGL.Target.GL_DEPTH_TEST);
                //OpenGL.gl_Viewport(0, 0, Window.Instance.get_width(), Window.Instance.get_height());
                //OpenGL.gl_MatrixMode((uint)OpenGL.MatrixMode.GL_PROJECTION);
                //OpenGL.gl_LoadIdentity();
                //OpenGL.gl_Ortho(Window.Instance.get_width(), 0, Window.Instance.get_height(), 0, -1, 1);
                //OpenGL.gl_MatrixMode((uint)OpenGL.MatrixMode.GL_MODELVIEW);
                //OpenGL.gl_LoadIdentity();
            }
        }

        public void destroy_renderer()
        {
            if (!Is3D)
            {
                if (_pRenderer == IntPtr.Zero) return;

                SDL.SDL_DestroyRenderer(_pRenderer);
                _pRenderer = IntPtr.Zero;
            }
            else
            {
                SDL.SDL_GL_DeleteContext(_pOpenGLContext);
            }
        }

        public void set_render_draw_colour(Colour? colour = null, OpenGL.GLcolour? gl_colour = null)
        {
            if (!Is3D)
            {
                Colour c = colour ?? Colour.black();
                SDL.SDL_SetRenderDrawColor(_pRenderer, c.r, c.g, c.b, c.a);
            }
            else
            {
                OpenGL.GLcolour gc = gl_colour ?? OpenGL.GLcolour.black();
                OpenGL.gl_ClearColor(gc.r, gc.g, gc.b, gc.a);
            }
        }

        public void set_render_clear()
        {
            if (!Is3D) SDL.SDL_RenderClear(_pRenderer);
            else
            {
                OpenGL.gl_Clear((uint)OpenGL.AttribMask.GL_DEPTH_BUFFER_BIT | (uint)OpenGL.AttribMask.GL_COLOR_BUFFER_BIT);
            }
        }

        public void set_render_present()
        {
            if (!Is3D) SDL.SDL_RenderPresent(_pRenderer);
            else SDL.SDL_GL_SwapWindow(Window.Instance.RawPointer);
        }

        public void render_sprite(Sprite sprite, Point3D position)
        {
            if (Is3D) return;

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
            if (Is3D) return;

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
            if (Is3D) return;

            set_render_draw_colour(colour);
            SDL.SDL_FRect s_rect;
            s_rect.x = rect.x;
            s_rect.y = rect.y;
            s_rect.w = rect.w;
            s_rect.h = rect.h;
            SDL.SDL_RenderDrawRectF(_pRenderer, ref s_rect);
        }

        public void render_point(Point3D point, Colour colour)
        {
            if (Is3D) return;

            set_render_draw_colour(colour);
            SDL.SDL_RenderDrawPointF(_pRenderer, point.x, point.y);
        }

        public void render_line(Point3D position1, Point3D position2, Colour colour)
        {
            if (Is3D) return;

            set_render_draw_colour(colour);
            SDL.SDL_RenderDrawLine(_pRenderer, position1.x, position1.y, position2.x, position2.y);
        }

        public void render_cuboid()
        {
            if (!Is3D) return;

            OpenGL.gl_LoadIdentity();

            OpenGL.gl_Rotatef(RotX, 1.0f, 0, 0);
            OpenGL.gl_Rotatef(RotY, 0, 1.0f, 0);

            //Multi-colored side - FRONT
            OpenGL.gl_Begin((uint)OpenGL.BeginMode.GL_POLYGON);
            OpenGL.gl_Color3f(1.0f, 0.0f, 0.0f); OpenGL.gl_Vertex3f(0.25f, -0.25f, -0.25f);      // P1 is red
            OpenGL.gl_Color3f(0.0f, 1.0f, 0.0f); OpenGL.gl_Vertex3f(0.25f, 0.25f, -0.25f);      // P2 is green
            OpenGL.gl_Color3f(0.0f, 0.0f, 1.0f); OpenGL.gl_Vertex3f(-0.25f, 0.25f, -0.25f);      // P3 is blue
            OpenGL.gl_Color3f(1.0f, 0.0f, 1.0f); OpenGL.gl_Vertex3f(-0.25f, -0.25f, -0.25f);      // P4 is purple
            OpenGL.gl_End();
            //BACK : White
            OpenGL.gl_Begin((uint)OpenGL.BeginMode.GL_POLYGON);
            OpenGL.gl_Color3f(1.0f, 1.0f, 1.0f);
            OpenGL.gl_Vertex3f(0.25f, -0.25f, 0.25f);
            OpenGL.gl_Vertex3f(0.25f, 0.25f, 0.25f);
            OpenGL.gl_Vertex3f(-0.25f, 0.25f, 0.25f);
            OpenGL.gl_Vertex3f(-0.25f, -0.25f, 0.25f);
            OpenGL.gl_End();
            //. Purple side - RIGHT
            OpenGL.gl_Begin((uint)OpenGL.BeginMode.GL_POLYGON);
            OpenGL.gl_Color3f(1.0f, 0.0f, 1.0f);
            OpenGL.gl_Vertex3f(0.25f, -0.25f, -0.25f);
            OpenGL.gl_Vertex3f(0.25f, 0.25f, -0.25f);
            OpenGL.gl_Vertex3f(0.25f, 0.25f, 0.25f);
            OpenGL.gl_Vertex3f(0.25f, -0.25f, 0.25f);
            OpenGL.gl_End();
            //. Green side - LEFT
            OpenGL.gl_Begin((uint)OpenGL.BeginMode.GL_POLYGON);
            OpenGL.gl_Color3f(0.0f, 1.0f, 0.0f);
            OpenGL.gl_Vertex3f(-0.25f, -0.25f, 0.25f);
            OpenGL.gl_Vertex3f(-0.25f, 0.25f, 0.25f);
            OpenGL.gl_Vertex3f(-0.25f, 0.25f, -0.25f);
            OpenGL.gl_Vertex3f(-0.25f, -0.25f, -0.25f);
            OpenGL.gl_End();
            //. Blue side - TOP
            OpenGL.gl_Begin((uint)OpenGL.BeginMode.GL_POLYGON);
            OpenGL.gl_Color3f(0.0f, 0.0f, 1.0f);
            OpenGL.gl_Vertex3f(0.25f, 0.25f, 0.25f);
            OpenGL.gl_Vertex3f(0.25f, 0.25f, -0.25f);
            OpenGL.gl_Vertex3f(-0.25f, 0.25f, -0.25f);
            OpenGL.gl_Vertex3f(-0.25f, 0.25f, 0.25f);
            OpenGL.gl_End();
            //. Red side - BOTTOM
            OpenGL.gl_Begin((uint)OpenGL.BeginMode.GL_POLYGON);
            OpenGL.gl_Color3f(1.0f, 0.0f, 0.0f);
            OpenGL.gl_Vertex3f(0.25f, -0.25f, -0.25f);
            OpenGL.gl_Vertex3f(0.25f, -0.25f, 0.25f);
            OpenGL.gl_Vertex3f(-0.25f, -0.25f, 0.25f);
            OpenGL.gl_Vertex3f(-0.25f, -0.25f, -0.25f);
            OpenGL.gl_End();
            OpenGL.gl_Flush();
        }

        public string get_error()
        {
            if (!Is3D)
            {
                return SDL.SDL_GetError();
            }
            else
            {
                if (SDL.SDL_GetError() == "" && OpenGL.gl_GetError() == 0) return "";
                else if (SDL.SDL_GetError() == "") return OpenGL.gl_GetError().ToString();
                else if (OpenGL.gl_GetError() == 0) return SDL.SDL_GetError();
                else return SDL.SDL_GetError() + " - " + OpenGL.gl_GetError().ToString();
            }
        }
    }
}
