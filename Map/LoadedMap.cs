using Map.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;

namespace Map
{
    public class LoadedMap
    {
        public Rectangle MapBorder { get; }
        public Texture2D MapTexture { get; }
        public TileArray Tiles { get; }
        public short TileSize { get; set; }
        public short BorderSize { get; set; }
        public Tile ChosenTile { get; set; }

        private MainCamera MainCam { get; }
        public static readonly Color LIGHT = Color.White;
        public static readonly Color SHADOW = Color.Gray;
        public static readonly Color CHOSEN = Color.Green;

        public LoadedMap(Texture2D map, TileArray tiles, short tileSize, short borderSize, GraphicsDevice _GraphicsDevice, MainCamera mainCamera)
        {
            MainCam = mainCamera;
            MapTexture = map;
            Tiles = tiles;
            TileSize = tileSize;
            BorderSize = borderSize;
            MapBorder = CalcMapSize();
        }

        private Rectangle CalcMapSize()
        {
            var xTiles = Tiles.Width;
            var yTiles = Tiles.Height;
            int mapHeight = yTiles * TileSize + (yTiles * BorderSize);
            int mapWidth = xTiles * TileSize + (xTiles * BorderSize);
            Console.WriteLine("Map height: " + mapHeight + " Map Width: " + mapWidth);
            return new Rectangle(0, 0, mapWidth, mapHeight);
        }

        //private void DrawBorder(SpriteBatch spriteBatch, Rectangle borderToDraw, int thicknessOfBorder, Color borderColour)
        //{
        //    // Draw top line
        //    spriteBatch.Draw(BorderPixel, new Rectangle(borderToDraw.X, borderToDraw.Y, borderToDraw.Width, thicknessOfBorder), borderColour);

        //    // Draw left line
        //    spriteBatch.Draw(BorderPixel, new Rectangle(borderToDraw.X, borderToDraw.Y, thicknessOfBorder, borderToDraw.Height), borderColour);

        //    // Draw right line
        //    spriteBatch.Draw(BorderPixel, new Rectangle((borderToDraw.X + borderToDraw.Width - thicknessOfBorder),
        //                                    borderToDraw.Y,
        //                                    thicknessOfBorder,
        //                                    borderToDraw.Height), borderColour);
        //    // Draw bottom line
        //    spriteBatch.Draw(BorderPixel, new Rectangle(borderToDraw.X,
        //                                    borderToDraw.Y + borderToDraw.Height - thicknessOfBorder,
        //                                    borderToDraw.Width,
        //                                    thicknessOfBorder), borderColour);
        //}


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var viewScreen = MainCam.WorldBorder();
            var startPoint = WorldToTile(viewScreen.X, viewScreen.Y);
            var endPoint = WorldToTile(viewScreen.Width, viewScreen.Height);

            if (startPoint.X > Tiles.Width || startPoint.Y > Tiles.Height) return;
            if (endPoint.X < 0 || endPoint.Y < 0) return;

            var startX = Math.Max(startPoint.X, 0);
            var startY = Math.Max(startPoint.Y, 0);
            var endY = Math.Min(endPoint.Y+1, Tiles.Height);
            var endX = Math.Min(endPoint.X+1, Tiles.Width);

            for (int y = startY; y < endY; y++)
                for (int x = startX; x < endX; x++)
                    DrawTile(spriteBatch, Tiles[x, y]);

        }

        private void DrawTile(SpriteBatch spriteBatch, Tile tile)
        {
            bool selected = ChosenTile != null && tile == ChosenTile;
            int tileHeight = tile.Height;
            int tileWidth = tile.Width;
            spriteBatch.Draw(MapTexture, new Vector2(tile.X, tile.Y), new Rectangle(tile.TextureX, tile.TextureY, tileWidth, tileHeight), selected ? CHOSEN : LIGHT);
            //DrawBorder(spriteBatch, new Rectangle(tile.X - BorderSize, tile.Y - BorderSize, tileWidth + BorderSize * 2, tileHeight + BorderSize * 2), BorderSize, Color.Black);
        }

        public Point WorldToTile(Point point) => WorldToTile(point.X, point.Y);
        public Point WorldToTile(int X, int Y) => new Point(X / (TileSize + BorderSize), Y / (TileSize + BorderSize));

    }
}
