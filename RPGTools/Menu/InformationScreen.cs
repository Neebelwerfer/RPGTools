using Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGTools.Menu.Interfaces;
using System;

namespace RPGTools.Menu
{
    public class InformationScreen : IMenu
    {
        private readonly Color HOVER = Color.Red;
        private readonly Color STANDARD = Color.Silver;

        private Tile selectedTile;
    

        public InformationScreen(Game1 context) : base(context)
        {
            var (width, height) = (context.GraphicsDevice.Viewport.Width, context.GraphicsDevice.Viewport.Height);
            bound = new Rectangle(0, (int)(height / 2.5), width / 4, (int)(height / 1.5));

            MapControl.TileSelected += OnTileSelected;
            MapControl.TileDeselected += OnTileDeselected;
        }


        protected void OnTileDeselected(object sender, EventArgs e)
        {
            selectedTile = null;
            if (active) ToggleVisibility();
        }

        protected void OnTileSelected(object sender, TileSelectedEvent e)
        {
            if (!active) ToggleVisibility();
            selectedTile = e.TileSelected;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (!active) return;

            spriteBatch.Draw(pixel, new Vector2(bound.X, bound.Y), bound, STANDARD);
        }
    }
}
