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
        protected bool _flip_x;
        protected bool _flip_y;
        protected double _rotation;

        public Sprite(string file_path, Rect dimensions) : base()
        {
            _file_path = file_path;
            _dimensions = dimensions;
            _layer = 0;
            _flip_x = false;
            _flip_y = false;
            _rotation = 0.0f;
        }

        public Sprite(string file_path, Rect dimensions, int layer) : base()
        {
            _file_path = file_path;
            _dimensions = dimensions;
            _layer = layer;
            _flip_x = false;
            _flip_y = false;
            _rotation = 0.0f;
        }

        public string FilePath { get => _file_path; set => _file_path = value; }
        public Rect Dimensions { get => _dimensions; set => _dimensions = value; }
        public int Layer { get => _layer; set => _layer = value; }
        public bool FlipX { get => _flip_x; set => _flip_x = value; }
        public bool FlipY { get => _flip_y; set => _flip_y = value; }
        public double Rotation { get => _rotation; set => _rotation = value; }
    }
}
