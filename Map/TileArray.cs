using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    public class TileArray
    {
        private Tile[] Tiles { get; set; }
        public int Width { get; }
        public int Height { get; }

        public TileArray(int width, int height)
        {
            Tiles = new Tile[width * height];
            Width = width;
            Height = height;
        }

        public Tile this[int index]
        {
            get => Tiles[index];
            set => Tiles[index] = value;
        }

        public Tile this[int x, int y]
        {
            get => Tiles[y * Width + x];
            set => Tiles[y * Width + x] = value;
        }

    }
}
