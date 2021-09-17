using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface.Interfaces
{
    public abstract class IButton : IMenuItem, IHoverable, IClickable
    {
        protected Rectangle ButtonBounds { get; }
        protected bool Active { get; set; }
        public bool Hovered { get; set; }

        public IButton(Rectangle button)
        {
            Active = true;
            ButtonBounds = button;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(UserInterfaceControl.WhitePixel, ButtonBounds, Color.Blue);
        }
        public virtual bool InBounds(Vector2 CursorPos) => (CursorPos.X >= ButtonBounds.X && CursorPos.X <= ButtonBounds.Width + ButtonBounds.X && (CursorPos.Y >= ButtonBounds.Y && CursorPos.Y <= ButtonBounds.Height + ButtonBounds.Y));
        public bool IsActive() => Active;
        public abstract bool OnHover(Vector2 mousePos);
        public abstract bool OnLeftClick(Vector2 mousePos);
        public virtual bool OnLeftHold(Vector2 mousePos) => false;
        public virtual bool OnRigthClick(Vector2 mousePos) => false;
        public virtual bool OnRigthHold(Vector2 mousePos) => false;
        public virtual bool ToggleVisibility() => Active = !Active;
    }
}
