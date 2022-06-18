using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public static class Debug
    {
        public enum LogLevel
        {
            DEBUG,
            INFO,
            WARNING,
            ERROR
        }

        public enum LogType
        {
            CONSOLE,
            FILE
        }

        public struct Line
        {
            public Point3D start, end;
            public Colour colour;

            public Line(Point3D start, Point3D end, Colour colour)
            {
                this.start = start;
                this.end = end;
                this.colour = colour;
            }
        }

        private static bool _show_collisions = false;
        private static List<Line> _lines = new();

        public static bool ShowCollisions { get => _show_collisions; set => _show_collisions = value; }

        public static string get_timestamp()
        {
            return DateTime.Now.ToString().Replace(':', '_');
        }
        
        private static void log(LogType type, LogLevel level, dynamic message)
        {
            string result = "";
            result += get_timestamp() + " : " + level.ToString() + " : ";
            result += message.ToString();
            Console.WriteLine(result);
        }

        public static void set_line(Point3D start, Point3D end, Colour colour)
        {
            _lines.Add(new(start, end, colour));
        }

        public static void draw_lines(Camera camera)
        {
            foreach (Line line in _lines)
                Rendering.Renderer.Instance.render_line(line.start, line.end, line.colour);

            _lines.Clear();
        }

        public static void log(dynamic message)
        {
            log(LogType.CONSOLE, LogLevel.INFO, message);
        }

        public static void log_debug(dynamic message)
        {
            log(LogType.CONSOLE, LogLevel.DEBUG, message);
        }

        public static void log_error(dynamic message)
        {
            log(LogType.CONSOLE, LogLevel.ERROR, message);
        }

        public static void log_warning(dynamic message)
        {
            log(LogType.CONSOLE, LogLevel.WARNING, message);
        }

        public static void clear_console()
        {
            Console.Clear();
        }

        public static void log_to_file(string path, string file_name, dynamic message)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            if (!File.Exists(path + "\\" + file_name + ".txt"))
            {
                using (FileStream fs = File.OpenWrite(path + "\\" + file_name + ".txt"))
                {
                    byte[] msg = new UTF8Encoding(true).GetBytes(message.ToString());
                    fs.Write(msg, 0, msg.Length);
                }
            }
            else
            {
                File.AppendAllText(path + "\\" + file_name + ".txt", message.ToString());
            }
        }
    }
}
