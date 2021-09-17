using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UserInterface.Interfaces
{
    public interface IMenuItem
    {
        bool InBounds(Vector2 CursorPos);
        bool ToggleVisibility();
        bool IsActive();
        void Draw(SpriteBatch spriteBatch);
    }
}
