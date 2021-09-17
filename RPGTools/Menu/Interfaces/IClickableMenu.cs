using Microsoft.Xna.Framework;

namespace RPGTools.Menu.Interfaces
{
    public abstract class IClickableMenu : IMenu
    {
        protected bool isHovered;


        public IClickableMenu(Game1 context) : base(context) => isHovered = false;

        public void SetHover(bool hover) => isHovered = hover;
        public abstract void OnClick(Vector2 CursorPos);
    }
}
