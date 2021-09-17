using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface.Interfaces
{
    public abstract class IClickableMenu : IMenu
    {
        protected bool isHovered;


        public IClickableMenu(GraphicsDevice graphicsDevice) : base(graphicsDevice) => isHovered = false;

        public void SetHover(bool hover) => isHovered = hover;
        public abstract void OnClick(Vector2 CursorPos);
    }
}
