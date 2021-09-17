using Inputs;
using Map.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Map
{
    public class MapControl
    {
        public static event EventHandler<TileSelectedEvent> TileSelected;
        public static event EventHandler TileDeselected;
        public MainCamera _MainCam { get; private set; }
        public LoadedMap _Map { get; private set; } 
        private GraphicsDevice GraphicsDevice { get; }
        private CameraControl CameraControl { get; set; }

        public MapControl(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime) => _Map?.Draw(spriteBatch, gameTime);

        #region Events
        public void OnRightButtonClicked(object sender, MouseEvent e)
        {
            if (_Map == null) return;
            _Map.ChosenTile = null;
            TileDeselected?.Invoke(this, EventArgs.Empty);
        }

        public void OnLeftButtonClicked(object sender, MouseEvent e)
        {
            if (_Map == null) return; 

            var mousePos = new Vector2(e.CurrState.X, e.CurrState.Y);
            var worldPos = _MainCam.ScreenToWorld(mousePos);
            Point tileP = _Map.WorldToTile(worldPos.ToPoint());
            if (tileP.Y >= _Map.Tiles.Height || tileP.X >= _Map.Tiles.Width || tileP.X < 0 || tileP.Y < 0) return;

            Tile found = _Map.Tiles[tileP.X, tileP.Y];
            if (_Map.ChosenTile != null && found == _Map.ChosenTile)
            {
                TileDeselected?.Invoke(this, EventArgs.Empty);
                _Map.ChosenTile = null;
            }
            else
            {
                _Map.ChosenTile = found;
                TileSelected?.Invoke(this, new TileSelectedEvent(_Map.ChosenTile));
            }
        }

        public void OnLeftButtonHold(object sender, MouseEvent e)
        {
            CameraControl?.OnLeftButtonHold(sender, e);
        }

        public void OnScrollwheelChanged(object sender, MouseEvent e)
        {
            CameraControl?.OnScrollwheelChanged(sender, e);
        }
        #endregion

        public void LoadMap(Texture2D mapTexture)
        {
            var tiles = SplitMapToTiles(mapTexture, 32, 1);
            _MainCam = new MainCamera(GraphicsDevice);
            _Map = new LoadedMap(mapTexture, tiles, 32, 1, GraphicsDevice, _MainCam);
            CameraControl = new CameraControl(_MainCam);
        }

        public void UnloadMap()
        {
            _MainCam = null;
            _Map = null;
            CameraControl = null;
        }

        private static TileArray SplitMapToTiles(Texture2D original, short size, short offset)
        {
            short yCount = (short)(original.Height / size + (size % original.Height == 0 ? 0 : 1));
            short xCount = (short)(original.Width / size + (size % original.Width == 0 ? 0 : 1));

            TileArray res = new TileArray(xCount, yCount);

            short pixels = (short)(size * size);

            Color[] originalData = new Color[original.Width * original.Height];
            original.GetData(originalData);

            for (short y = 0; y < yCount * size; y += size)
            {
                for (short x = 0; x < xCount * size; x += size)
                {
                    var (height, width) = CalcSizeOfTile(x, y, size, original.Width, original.Height, originalData);
                    var tileX = (short)(x / size);
                    var tileY = (short)(y / size);
                    var tileObj = new Tile(size, tileX, tileY, offset, height, width);

                    res[tileX, tileY] = tileObj;
                }
            }
            return res;
        }

        private static (short, short) CalcSizeOfTile(short x, short y, short size, int originalWidth, int originalHeight, Color[] originalData)
        {
            short height = 0, width = 0;

            for (short ty = 0; ty < size; ty++)
            {
                height = ty;
                if (y + ty >= originalHeight || x >= originalWidth || originalData[x + (y + ty) * originalWidth] == Color.Transparent) break;
            }

            for (short tx = 0; tx < size; tx++)
            {
                width = tx;
                if (x + tx >= originalWidth || y >= originalHeight || originalData[(tx + x) + y * originalWidth] == Color.Transparent) break;
            }
            return (height, width);
        }
    }
}
