using Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Map.Camera
{
    public class CameraControl
    {
        private readonly MainCamera Camera;
        public CameraControl(MainCamera camera)
        {
            Camera = camera;
        }

        #region EventHandling
        public void OnScrollwheelChanged(object sender, MouseEvent e)
        {
            KeyboardState keyState = Inputs.KeyboardInputs.CurrKeyState;
            if (keyState.IsKeyDown(Keys.LeftShift) || keyState.IsKeyDown(Keys.RightShift))
                Camera.ZoomSpeed = 0.5f;
            else Camera.ZoomSpeed = 0.25f;

            if (e.ScrollWheelChange > 0)
                Camera.Zoom += Camera.ZoomSpeed;
            else
                Camera.Zoom -= Camera.ZoomSpeed;
        }

        public void OnLeftButtonHold(object sender, MouseEvent e)
        {
            Vector2 newMousePoint = new Vector2(e.CurrState.X, e.CurrState.Y);
            Vector2 oldMousePoint = new Vector2(e.PrevState.X, e.PrevState.Y);
            var res = newMousePoint - oldMousePoint;
            if (res.Length() != 0)
            {
                res = res * -1;
                Camera.Move(res);
            }
        }
        #endregion
    }
}
