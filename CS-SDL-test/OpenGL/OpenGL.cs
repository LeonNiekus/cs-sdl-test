using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using CS_SDL_test.Lib.API;

namespace CS_OpenGL
{
    internal static class OpenGL
    {
        private const string LIB_NAME = "libGL";

        public struct GLcolour
        {
            public GLfclamp r, g, b, a;

            public GLcolour(GLfclamp r, GLfclamp g, GLfclamp b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                a = GLfclamp.f(1.0f);
            }

            public GLcolour(GLfclamp r, GLfclamp g, GLfclamp b, GLfclamp a)
            {
                this.r = r;
                this.g = g;
                this.b = b;
                this.a = a;
            }

            public static GLcolour black()
            {
                return new(GLfclamp.f(0.0f), GLfclamp.f(0.0f), GLfclamp.f(0.0f));
            }

            public static GLcolour white()
            {
                return new(GLfclamp.f(1.0f), GLfclamp.f(1.0f), GLfclamp.f(1.0f));
            }

            public static GLcolour red()
            {
                return new(GLfclamp.f(1.0f), GLfclamp.f(0.0f), GLfclamp.f(0.0f));
            }

            public static GLcolour green()
            {
                return new(GLfclamp.f(0.0f), GLfclamp.f(1.0f), GLfclamp.f(0.0f));
            }

            public static GLcolour blue()
            {
                return new(GLfclamp.f(0.0f), GLfclamp.f(0.0f), GLfclamp.f(1.0f));
            }

            public static GLcolour yellow()
            {
                return new(GLfclamp.f(1.0f), GLfclamp.f(1.0f), GLfclamp.f(0.0f));
            }

            public static GLcolour purple()
            {
                return new(GLfclamp.f(1.0f), GLfclamp.f(0.0f), GLfclamp.f(1.0f));
            }
        }

        public struct GLfclamp
        {
            public float value;

            public GLfclamp(float val)
            {
                if (val <= 1.0f && val >= 0.0f) value = val;
                else value = 0.0f;
            }

            public static implicit operator float(GLfclamp glfc)
            {
                return glfc.value;
            }

            public static GLfclamp f(float val)
            {
                return new GLfclamp(val);
            }
        }

        public enum BeginMode : uint
        {
            GL_POINTS         = 0x0000,
            GL_LINES          = 0x0001,
            GL_LINE_LOOP      = 0x0002,
            GL_LINE_STRIP     = 0x0003,
            GL_TRIANGLES      = 0x0004,
            GL_TRIANGLE_STRIP = 0x0005,
            GL_TRIANGLE_FAN   = 0x0006,
            GL_QUADS          = 0x0007,
            GL_QUAD_STRIP     = 0x0008,
            GL_POLYGON        = 0x0009
        }

        public enum AttribMask : uint 
        {
            GL_CURRENT_BIT         = 0x00000001,
            GL_POINT_BIT           = 0x00000002,
            GL_LINE_BIT            = 0x00000004,
            GL_POLYGON_BIT         = 0x00000008,
            GL_POLYGON_STIPPLE_BIT = 0x00000010,
            GL_PIXEL_MODE_BIT      = 0x00000020,
            GL_LIGHTING_BIT        = 0x00000040,
            GL_FOG_BIT             = 0x00000080,
            GL_DEPTH_BUFFER_BIT    = 0x00000100,
            GL_ACCUM_BUFFER_BIT    = 0x00000200,
            GL_STENCIL_BUFFER_BIT  = 0x00000400,
            GL_VIEWPORT_BIT        = 0x00000800,
            GL_TRANSFORM_BIT       = 0x00001000,
            GL_ENABLE_BIT          = 0x00002000,
            GL_COLOR_BUFFER_BIT    = 0x00004000,
            GL_HINT_BIT            = 0x00008000,
            GL_EVAL_BIT            = 0x00010000,
            GL_LIST_BIT            = 0x00020000,
            GL_TEXTURE_BIT         = 0x00040000,
            GL_SCISSOR_BIT         = 0x00080000,
            GL_ALL_ATTRIB_BITS     = 0x000fffff
        }

        public enum Target : uint
        {
            GL_CURRENT_COLOR                  = 0x0B00,
            GL_CURRENT_INDEX                  = 0x0B01,
            GL_CURRENT_NORMAL                 = 0x0B02,
            GL_CURRENT_TEXTURE_COORDS         = 0x0B03,
            GL_CURRENT_RASTER_COLOR           = 0x0B04,
            GL_CURRENT_RASTER_INDEX           = 0x0B05,
            GL_CURRENT_RASTER_TEXTURE_COORDS  = 0x0B06,
            GL_CURRENT_RASTER_POSITION        = 0x0B07,
            GL_CURRENT_RASTER_POSITION_VALID  = 0x0B08,
            GL_CURRENT_RASTER_DISTANCE        = 0x0B09,
            GL_POINT_SMOOTH                   = 0x0B10,
            GL_POINT_SIZE                     = 0x0B11,
            GL_POINT_SIZE_RANGE               = 0x0B12,
            GL_POINT_SIZE_GRANULARITY         = 0x0B13,
            GL_LINE_SMOOTH                    = 0x0B20,
            GL_LINE_WIDTH                     = 0x0B21,
            GL_LINE_WIDTH_RANGE               = 0x0B22,
            GL_LINE_WIDTH_GRANULARITY         = 0x0B23,
            GL_LINE_STIPPLE                   = 0x0B24,
            GL_LINE_STIPPLE_PATTERN           = 0x0B25,
            GL_LINE_STIPPLE_REPEAT            = 0x0B26,
            GL_LIST_MODE                      = 0x0B30,
            GL_MAX_LIST_NESTING               = 0x0B31,
            GL_LIST_BASE                      = 0x0B32,
            GL_LIST_INDEX                     = 0x0B33,
            GL_POLYGON_MODE                   = 0x0B40,
            GL_POLYGON_SMOOTH                 = 0x0B41,
            GL_POLYGON_STIPPLE                = 0x0B42,
            GL_EDGE_FLAG                      = 0x0B43,
            GL_CULL_FACE                      = 0x0B44,
            GL_CULL_FACE_MODE                 = 0x0B45,
            GL_FRONT_FACE                     = 0x0B46,
            GL_LIGHTING                       = 0x0B50,
            GL_LIGHT_MODEL_LOCAL_VIEWER       = 0x0B51,
            GL_LIGHT_MODEL_TWO_SIDE           = 0x0B52,
            GL_LIGHT_MODEL_AMBIENT            = 0x0B53,
            GL_SHADE_MODEL                    = 0x0B54,
            GL_COLOR_MATERIAL_FACE            = 0x0B55,
            GL_COLOR_MATERIAL_PARAMETER       = 0x0B56,
            GL_COLOR_MATERIAL                 = 0x0B57,
            GL_FOG                            = 0x0B60,
            GL_FOG_INDEX                      = 0x0B61,
            GL_FOG_DENSITY                    = 0x0B62,
            GL_FOG_START                      = 0x0B63,
            GL_FOG_END                        = 0x0B64,
            GL_FOG_MODE                       = 0x0B65,
            GL_FOG_COLOR                      = 0x0B66,
            GL_DEPTH_RANGE                    = 0x0B70,
            GL_DEPTH_TEST                     = 0x0B71,
            GL_DEPTH_WRITEMASK                = 0x0B72,
            GL_DEPTH_CLEAR_VALUE              = 0x0B73,
            GL_DEPTH_FUNC                     = 0x0B74,
            GL_ACCUM_CLEAR_VALUE              = 0x0B80,
            GL_STENCIL_TEST                   = 0x0B90,
            GL_STENCIL_CLEAR_VALUE            = 0x0B91,
            GL_STENCIL_FUNC                   = 0x0B92,
            GL_STENCIL_VALUE_MASK             = 0x0B93,
            GL_STENCIL_FAIL                   = 0x0B94,
            GL_STENCIL_PASS_DEPTH_FAIL        = 0x0B95,
            GL_STENCIL_PASS_DEPTH_PASS        = 0x0B96,
            GL_STENCIL_REF                    = 0x0B97,
            GL_STENCIL_WRITEMASK              = 0x0B98,
            GL_MATRIX_MODE                    = 0x0BA0,
            GL_NORMALIZE                      = 0x0BA1,
            GL_VIEWPORT                       = 0x0BA2,
            GL_MODELVIEW_STACK_DEPTH          = 0x0BA3,
            GL_PROJECTION_STACK_DEPTH         = 0x0BA4,
            GL_TEXTURE_STACK_DEPTH            = 0x0BA5,
            GL_MODELVIEW_MATRIX               = 0x0BA6,
            GL_PROJECTION_MATRIX              = 0x0BA7,
            GL_TEXTURE_MATRIX                 = 0x0BA8,
            GL_ATTRIB_STACK_DEPTH             = 0x0BB0,
            GL_CLIENT_ATTRIB_STACK_DEPTH      = 0x0BB1,
            GL_ALPHA_TEST                     = 0x0BC0,
            GL_ALPHA_TEST_FUNC                = 0x0BC1,
            GL_ALPHA_TEST_REF                 = 0x0BC2,
            GL_DITHER                         = 0x0BD0,
            GL_BLEND_DST                      = 0x0BE0,
            GL_BLEND_SRC                      = 0x0BE1,
            GL_BLEND                          = 0x0BE2,
            GL_LOGIC_OP_MODE                  = 0x0BF0,
            GL_INDEX_LOGIC_OP                 = 0x0BF1,
            GL_COLOR_LOGIC_OP                 = 0x0BF2,
            GL_AUX_BUFFERS                    = 0x0C00,
            GL_DRAW_BUFFER                    = 0x0C01,
            GL_READ_BUFFER                    = 0x0C02,
            GL_SCISSOR_BOX                    = 0x0C10,
            GL_SCISSOR_TEST                   = 0x0C11,
            GL_INDEX_CLEAR_VALUE              = 0x0C20,
            GL_INDEX_WRITEMASK                = 0x0C21,
            GL_COLOR_CLEAR_VALUE              = 0x0C22,
            GL_COLOR_WRITEMASK                = 0x0C23,
            GL_INDEX_MODE                     = 0x0C30,
            GL_RGBA_MODE                      = 0x0C31,
            GL_DOUBLEBUFFER                   = 0x0C32,
            GL_STEREO                         = 0x0C33,
            GL_RENDER_MODE                    = 0x0C40,
            GL_PERSPECTIVE_CORRECTION_HINT    = 0x0C50,
            GL_POINT_SMOOTH_HINT              = 0x0C51,
            GL_LINE_SMOOTH_HINT               = 0x0C52,
            GL_POLYGON_SMOOTH_HINT            = 0x0C53,
            GL_FOG_HINT                       = 0x0C54,
            GL_TEXTURE_GEN_S                  = 0x0C60,
            GL_TEXTURE_GEN_T                  = 0x0C61,
            GL_TEXTURE_GEN_R                  = 0x0C62,
            GL_TEXTURE_GEN_Q                  = 0x0C63,
            GL_PIXEL_MAP_I_TO_I               = 0x0C70,
            GL_PIXEL_MAP_S_TO_S               = 0x0C71,
            GL_PIXEL_MAP_I_TO_R               = 0x0C72,
            GL_PIXEL_MAP_I_TO_G               = 0x0C73,
            GL_PIXEL_MAP_I_TO_B               = 0x0C74,
            GL_PIXEL_MAP_I_TO_A               = 0x0C75,
            GL_PIXEL_MAP_R_TO_R               = 0x0C76,
            GL_PIXEL_MAP_G_TO_G               = 0x0C77,
            GL_PIXEL_MAP_B_TO_B               = 0x0C78,
            GL_PIXEL_MAP_A_TO_A               = 0x0C79,
            GL_PIXEL_MAP_I_TO_I_SIZE          = 0x0CB0,
            GL_PIXEL_MAP_S_TO_S_SIZE          = 0x0CB1,
            GL_PIXEL_MAP_I_TO_R_SIZE          = 0x0CB2,
            GL_PIXEL_MAP_I_TO_G_SIZE          = 0x0CB3,
            GL_PIXEL_MAP_I_TO_B_SIZE          = 0x0CB4,
            GL_PIXEL_MAP_I_TO_A_SIZE          = 0x0CB5,
            GL_PIXEL_MAP_R_TO_R_SIZE          = 0x0CB6,
            GL_PIXEL_MAP_G_TO_G_SIZE          = 0x0CB7,
            GL_PIXEL_MAP_B_TO_B_SIZE          = 0x0CB8,
            GL_PIXEL_MAP_A_TO_A_SIZE          = 0x0CB9,
            GL_UNPACK_SWAP_BYTES              = 0x0CF0,
            GL_UNPACK_LSB_FIRST               = 0x0CF1,
            GL_UNPACK_ROW_LENGTH              = 0x0CF2,
            GL_UNPACK_SKIP_ROWS               = 0x0CF3,
            GL_UNPACK_SKIP_PIXELS             = 0x0CF4,
            GL_UNPACK_ALIGNMENT               = 0x0CF5,
            GL_PACK_SWAP_BYTES                = 0x0D00,
            GL_PACK_LSB_FIRST                 = 0x0D01,
            GL_PACK_ROW_LENGTH                = 0x0D02,
            GL_PACK_SKIP_ROWS                 = 0x0D03,
            GL_PACK_SKIP_PIXELS               = 0x0D04,
            GL_PACK_ALIGNMENT                 = 0x0D05,
            GL_MAP_COLOR                      = 0x0D10,
            GL_MAP_STENCIL                    = 0x0D11,
            GL_INDEX_SHIFT                    = 0x0D12,
            GL_INDEX_OFFSET                   = 0x0D13,
            GL_RED_SCALE                      = 0x0D14,
            GL_RED_BIAS                       = 0x0D15,
            GL_ZOOM_X                         = 0x0D16,
            GL_ZOOM_Y                         = 0x0D17,
            GL_GREEN_SCALE                    = 0x0D18,
            GL_GREEN_BIAS                     = 0x0D19,
            GL_BLUE_SCALE                     = 0x0D1A,
            GL_BLUE_BIAS                      = 0x0D1B,
            GL_ALPHA_SCALE                    = 0x0D1C,
            GL_ALPHA_BIAS                     = 0x0D1D,
            GL_DEPTH_SCALE                    = 0x0D1E,
            GL_DEPTH_BIAS                     = 0x0D1F,
            GL_MAX_EVAL_ORDER                 = 0x0D30,
            GL_MAX_LIGHTS                     = 0x0D31,
            GL_MAX_CLIP_PLANES                = 0x0D32,
            GL_MAX_TEXTURE_SIZE               = 0x0D33,
            GL_MAX_PIXEL_MAP_TABLE            = 0x0D34,
            GL_MAX_ATTRIB_STACK_DEPTH         = 0x0D35,
            GL_MAX_MODELVIEW_STACK_DEPTH      = 0x0D36,
            GL_MAX_NAME_STACK_DEPTH           = 0x0D37,
            GL_MAX_PROJECTION_STACK_DEPTH     = 0x0D38,
            GL_MAX_TEXTURE_STACK_DEPTH        = 0x0D39,
            GL_MAX_VIEWPORT_DIMS              = 0x0D3A,
            GL_MAX_CLIENT_ATTRIB_STACK_DEPTH  = 0x0D3B,
            GL_SUBPIXEL_BITS                  = 0x0D50,
            GL_INDEX_BITS                     = 0x0D51,
            GL_RED_BITS                       = 0x0D52,
            GL_GREEN_BITS                     = 0x0D53,
            GL_BLUE_BITS                      = 0x0D54,
            GL_ALPHA_BITS                     = 0x0D55,
            GL_DEPTH_BITS                     = 0x0D56,
            GL_STENCIL_BITS                   = 0x0D57,
            GL_ACCUM_RED_BITS                 = 0x0D58,
            GL_ACCUM_GREEN_BITS               = 0x0D59,
            GL_ACCUM_BLUE_BITS                = 0x0D5A,
            GL_ACCUM_ALPHA_BITS               = 0x0D5B,
            GL_NAME_STACK_DEPTH               = 0x0D70,
            GL_AUTO_NORMAL                    = 0x0D80,
            GL_MAP1_COLOR_4                   = 0x0D90,
            GL_MAP1_INDEX                     = 0x0D91,
            GL_MAP1_NORMAL                    = 0x0D92,
            GL_MAP1_TEXTURE_COORD_1           = 0x0D93,
            GL_MAP1_TEXTURE_COORD_2           = 0x0D94,
            GL_MAP1_TEXTURE_COORD_3           = 0x0D95,
            GL_MAP1_TEXTURE_COORD_4           = 0x0D96,
            GL_MAP1_VERTEX_3                  = 0x0D97,
            GL_MAP1_VERTEX_4                  = 0x0D98,
            GL_MAP2_COLOR_4                   = 0x0DB0,
            GL_MAP2_INDEX                     = 0x0DB1,
            GL_MAP2_NORMAL                    = 0x0DB2,
            GL_MAP2_TEXTURE_COORD_1           = 0x0DB3,
            GL_MAP2_TEXTURE_COORD_2           = 0x0DB4,
            GL_MAP2_TEXTURE_COORD_3           = 0x0DB5,
            GL_MAP2_TEXTURE_COORD_4           = 0x0DB6,
            GL_MAP2_VERTEX_3                  = 0x0DB7,
            GL_MAP2_VERTEX_4                  = 0x0DB8,
            GL_MAP1_GRID_DOMAIN               = 0x0DD0,
            GL_MAP1_GRID_SEGMENTS             = 0x0DD1,
            GL_MAP2_GRID_DOMAIN               = 0x0DD2,
            GL_MAP2_GRID_SEGMENTS             = 0x0DD3,
            GL_TEXTURE_1D                     = 0x0DE0,
            GL_TEXTURE_2D                     = 0x0DE1,
            GL_FEEDBACK_BUFFER_POINTER        = 0x0DF0,
            GL_FEEDBACK_BUFFER_SIZE           = 0x0DF1,
            GL_FEEDBACK_BUFFER_TYPE           = 0x0DF2,
            GL_SELECTION_BUFFER_POINTER       = 0x0DF3,
            GL_SELECTION_BUFFER_SIZE          = 0x0DF4
        }

        public enum MatrixMode : uint
        {
            GL_MODELVIEW  = 0x1700,
            GL_PROJECTION = 0x1701,
            GL_TEXTURE    = 0x1702
        }

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_ClearColor(float r, float g, float b, float a);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Clear(uint mask);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Enable(uint cap);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Disable(uint cap);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Viewport(int x, int y, int width, int height);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_MatrixMode(uint mode);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_LoadIdentity();

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Ortho(double left, double right, double bottom, double top, double zNear, double zFar);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Begin(uint mode);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_End();

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Flush();

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Color3f(float r, float g, float b);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Vertex3i(int x, int y, int z);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Vertex3f(float x, float y, float z);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Vertex2f(float x, float y);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Translatef(float x, float y, float z);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern void gl_Rotatef(float angle, float x, float y, float z);

        [DllImport(LIB_NAME, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint gl_GetError();
    }
}
