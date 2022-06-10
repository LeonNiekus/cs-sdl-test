using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public static class Time
    {
        public static long previous_time = 0;
        public static float delta_time = 0.0f;
        private static double time_scale = 1.0f;
        public static float fps = 0.0f;
        public static float average_fps = 0.0f;

        public static void calculate_delta_time()
        {
            delta_time = 1.0f / (1000.0f / Application.FRAME_DELAY); 
        }

        public static double time_scale_default()
        {
            return 1.0f;
        }

        public static double get_time_scale()
        {
            return time_scale;
        }

        public static void set_time_scale(double new_time_scale)
        {
            const double time_scale_min = 0.0f;
            const double time_scale_max = double.MaxValue;

            time_scale = Math.Clamp(new_time_scale, time_scale_min, time_scale_max);
        }
    }
}
