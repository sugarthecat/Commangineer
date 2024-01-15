using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commangineer.Auuki
{
    public interface AuukiTarget
    {
        public void Damage(int damage);
        public bool Alive { get; }
        public Vector2 CenterPosition { get; }
    }
}
