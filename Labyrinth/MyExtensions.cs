using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public static class MyExtensions
    {
        public static bool IsHit(this ICollection<Collision> coll, Point p)
        {
            return coll.Any(i => i.IsHit(p));
        }

        public static bool Collide(this ICollection<Collision> coll, Rectangle r)
        {
            return coll.Any(i => i.Collide(r));
        }
    }
}
