using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public class Component
    {
        protected Entity _parent = null;

        public Entity Parent 
        { 
            get => _parent; 
            set 
            { 
                _parent = value;
                parent_changed();
            } 
        }
        public bool Active { get; set; }

        protected virtual void parent_changed() { }
    }
}
