using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace RPGTools.Menu.Interfaces
{
    public abstract class IMenu
    {
        protected static Texture2D pixel;
        protected bool active;
        protected Rectangle bound;
        protected Game1 context;
        protected List<IMenuItem> MenuItems;

        public IMenu(Game1 context)
        {
            if (pixel == null)
            {
                pixel = new Texture2D(context.GraphicsDevice, 1, 1);
                pixel.SetData(new Color[] { Color.White });
            }
            active = false;
            this.context = context;
            MenuItems = new List<IMenuItem>();
        }

        /// <summary>
        /// Checks if mouse is in the menu bounds
        /// </summary>
        /// <param name="cursorPos">The untransformed cursor position</param>
        /// <returns></returns>
        public bool InBounds(Vector2 cursorPos) => (cursorPos.X >= bound.X && cursorPos.X <= bound.Width + bound.X && (cursorPos.Y >= bound.Y && cursorPos.Y <= bound.Height + bound.Y));
        public bool ToggleVisibility() => active = !active;
        public bool IsActive() => active;
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

    }
}
