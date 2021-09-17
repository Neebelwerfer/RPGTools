using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Map
{
    public class Tile
    {
        #region Positioning Properties
        /// <summary>
        /// The Tile X value based on position compared to other tiles
        /// </summary>
        public short TileX { get; private set; }

        /// <summary>
        /// The Tile Y value based on position compared to other tiles
        /// </summary>
        public short TileY { get; private set; }

        /// <summary>
        /// The X coordinate on the texture the Tile is based on
        /// </summary>
        public short TextureX { get; private set; }

        /// <summary>
        /// The Y coordinate on the texture the Tile is based on
        /// </summary>
        public short TextureY { get; private set; }

        /// <summary>
        /// X World coordinate
        /// </summary>
        public short X { get; private set; }

        /// <summary>
        /// Y World coordinate
        /// </summary>
        public short Y { get; private set; }

        /// <summary>
        /// The real width of the tile.
        /// Mostly used for the borders
        /// </summary>
        public short Width { get; private set; }

        /// <summary>
        /// The real height of the tile.
        /// Mostly used for the borders
        /// </summary>
        public short Height { get; private set; }
        #endregion

        public Tile(short tileSize, short x, short y, short offset, short height, short width)
        {
            Width = width;
            Height = height;
            TileX = x;
            TileY = y;
            X = (short)(tileSize * x + (offset * x));
            Y = (short)(tileSize * y + (offset * y));
            TextureX = (short)(x * tileSize);
            TextureY = (short)(y * tileSize);
        }
    }
}
