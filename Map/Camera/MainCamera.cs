using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Map.Camera
{
    public class MainCamera
    {
        private readonly GraphicsDevice graphicsDevice;
        protected float _zoom; // Camera Zoom
        protected float _rotation; // Camera Rotation

        public Matrix _transform; // Matrix Transform
        public Vector2 _pos; // Camera Position
        public bool isDragging = false;

        public MainCamera(GraphicsDevice graphicsDevice)
        {
            _zoom = 2.0f;
            _rotation = 0.0f;
            ZoomSpeed = 0.25f;
            ViewHeight = graphicsDevice.Viewport.Height;
            ViewWidth = graphicsDevice.Viewport.Width;
            _pos = new Vector2(ViewWidth / 4, ViewHeight / 4);
            this.graphicsDevice = graphicsDevice;
        }

        public int ViewHeight { get; set; }

        public int ViewWidth { get; set; }


        #region CameraControls
        // Sets and gets zoom
        public float Zoom
        {
            get => _zoom;
            set { _zoom = value; if (_zoom < 1f) _zoom = 1f; else if (_zoom > 3f) _zoom = 3f; } // Negative zoom will flip image
        }

        public float ZoomSpeed
        {
            get;
            set;
        }

        public float Rotation
        {
            get => _rotation;
            set => _rotation = value;
        }

        // Auxiliary function to move the camera
        public void Move(Vector2 amount) => Pos += amount;
        // Get set position
        public Vector2 Pos { get => _pos; set => _pos = value; }

        public Vector2 WorldToScreen(Vector2 worldPoint) => Vector2.Transform(worldPoint, GetTransformation());

        public Vector2 ScreenToWorld(Vector2 screenPoint) => Vector2.Transform(screenPoint, Matrix.Invert(GetTransformation()));

        public Rectangle WorldBorder()
        {
            var upperLeftCorner = ScreenToWorld(new Vector2(0, 0));
            var lowerRightCorner = ScreenToWorld(new Vector2(ViewWidth, ViewHeight));
            return new Rectangle((int)Math.Floor(upperLeftCorner.X), (int)Math.Floor(upperLeftCorner.Y), (int)Math.Ceiling(lowerRightCorner.X + 12), (int)Math.Ceiling(lowerRightCorner.Y + 12));
        }

        public Matrix GetTransformation()
        {
            _transform =
              Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
                                         Matrix.CreateRotationZ(Rotation) *
                                         Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(ViewWidth * 0.5f, ViewHeight * 0.5f, 0));
            return _transform;
        }

        private bool CheckOutsideMap(Vector2 pos, Rectangle mapBorder)
        {
            Vector2 upperLeft = new Vector2(pos.X - ViewWidth / 4, pos.Y - ViewHeight / 4);
            Vector2 lowerRight = new Vector2(pos.X + ViewWidth / 4, pos.Y + ViewHeight / 4);
            return (upperLeft.X > mapBorder.Width || upperLeft.Y > mapBorder.Height || lowerRight.X < mapBorder.X || lowerRight.Y < mapBorder.Y);
        }
        #endregion
    }
}
