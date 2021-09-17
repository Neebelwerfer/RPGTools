using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace UserInterface.Interfaces
{
    public abstract class IMenu
    {
        protected static Texture2D pixel;
        protected bool active;
        protected Rectangle bound;
        protected List<IMenuItem> MenuItems;
        protected GraphicsDevice _GraphicsDevice { get; }

        public IMenu(GraphicsDevice graphicsDevice)
        {
            _GraphicsDevice = graphicsDevice;
            if (pixel == null)
            {
                pixel = new Texture2D(graphicsDevice, 1, 1);
                pixel.SetData(new Color[] { Color.White });
            }
            active = false;
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
