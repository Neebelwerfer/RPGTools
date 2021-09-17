using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGTools.Menu.Interfaces;
using System;

namespace RPGTools.Menu
{
    class MapDragSelectMenu : IClickableMenu
    {
        public MapDragSelectMenu(Game1 context) :base(context)
        {

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime) => throw new NotImplementedException();

        public override void OnClick(Vector2 CursorPos) => throw new NotImplementedException();
    }
}
