using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public class Sprite : Component
    {
        protected string _file_path;
        protected Rect _position;

        public Sprite(string file_path, Rect position)
        {
            _file_path = file_path;
            _position = position;
        }

        public string FilePath { get => _file_path; set => _file_path = value; }
        public Rect Position { get => _position; set => _position = value; }
    }
}
