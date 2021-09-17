using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInterface.Interfaces
{
    interface IHoverable
    {
        bool Hovered { get; set; }
        bool OnHover(Vector2 mousePos);
    }
}
