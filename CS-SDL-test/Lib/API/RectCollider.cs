using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_SDL_test.Lib.API
{
    public class RectCollider : Collider
    {
        public RectCollider(List<int> collide_layers, Point3D offset) : base(collide_layers, offset)
        {

        }

        public override FloatContainer resolve_collision(Collider coll1, Collider coll2, FloatContainer velocity, ref bool collision_occurred)
        {
            throw new NotImplementedException();
        }
    }
}
