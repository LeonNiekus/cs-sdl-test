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
        protected Rect _dimensions;
        protected int _layer;

        public Sprite(string file_path, Rect dimensions)
        {
            _file_path = file_path;
            _dimensions = dimensions;
            _layer = 0;
        }

        public Sprite(string file_path, Rect dimensions, int layer)
        {
            _file_path = file_path;
            _dimensions = dimensions;
            _layer = layer;
        }

        public string FilePath { get => _file_path; set => _file_path = value; }
        public Rect Dimensions { get => _dimensions; set => _dimensions = value; }
        public int Layer { get => _layer; set => _layer = value; }
    }
}
