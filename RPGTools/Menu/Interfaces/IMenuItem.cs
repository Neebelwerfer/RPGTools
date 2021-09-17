using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RPGTools.Menu.Interfaces
{
    public interface IMenuItem
    {
        bool InBounds(Vector2 CursorPos);
        bool ToggleVisibility();
        bool IsActive();
        void OnClick(Vector2 CursorPos);
        void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
