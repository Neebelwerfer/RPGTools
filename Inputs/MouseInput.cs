using Microsoft.Xna.Framework.Input;
using System;

namespace Inputs
{
    public class MouseInput
    {
        private byte leftCounter;
        private byte rightCounter;
        public static MouseState PrevMouseState { get; private set; }
        public static MouseState CurrMouseState { get; private set; }

        public static event EventHandler<MouseEvent> LeftButtonClicked;
        public static event EventHandler<MouseEvent> LeftButtonHold;
        public static event EventHandler<MouseEvent> ScrollWheelChanged;
        public static event EventHandler<MouseEvent> MouseMoved;
        public static event EventHandler<MouseEvent> RightButtonClicked;
        public static event EventHandler<MouseEvent> RightButtonHold;

        public MouseInput()
        {
            PrevMouseState = Mouse.GetState();
            CurrMouseState = PrevMouseState;
            leftCounter = 0;
            rightCounter = 0;
        }

        private void UpdateLeftMouseButton()
        {
            var prev = PrevMouseState.LeftButton;
            var curr = CurrMouseState.LeftButton;

            if(prev == ButtonState.Released && curr == ButtonState.Pressed)
            {
                leftCounter = 0;
            }
            else if (prev == ButtonState.Pressed && curr == ButtonState.Pressed)
            {
                if (leftCounter > 5) LeftButtonHold?.Invoke(this, new MouseEvent(PrevMouseState, CurrMouseState));
                else leftCounter++;
            } 
            else if (prev == ButtonState.Pressed && curr == ButtonState.Released)
            {
                if (leftCounter <= 5) LeftButtonClicked?.Invoke(this, new MouseEvent(PrevMouseState, CurrMouseState));
                leftCounter = 0;
            }
        }

        private void UpdateRightMouseButton()
        {
            var prev = PrevMouseState.RightButton;
            var curr = CurrMouseState.RightButton;

            if (prev == ButtonState.Released && curr == ButtonState.Pressed)
            {
                rightCounter = 0;
            }
            else if (prev == ButtonState.Pressed && curr == ButtonState.Pressed)
            {
                if (rightCounter > 6) RightButtonHold?.Invoke(this, new MouseEvent(PrevMouseState, CurrMouseState));
                else rightCounter++;
            }
            else if (prev == ButtonState.Pressed && curr == ButtonState.Released)
            {
                if (rightCounter <= 6) RightButtonClicked?.Invoke(this, new MouseEvent(PrevMouseState, CurrMouseState));
                rightCounter = 0;
            }
        }

        private void UpdateScrollWheel()
        {

            int mouseWheelChange = CurrMouseState.ScrollWheelValue - PrevMouseState.ScrollWheelValue;

            if (mouseWheelChange != 0)
            {
                ScrollWheelChanged?.Invoke(this, new MouseEvent(PrevMouseState, CurrMouseState, mouseWheelChange));
            }
        }

        private void UpdateMousePosition()
        {
            if (PrevMouseState.X != CurrMouseState.X || PrevMouseState.Y != CurrMouseState.Y) MouseMoved?.Invoke(this, new MouseEvent(PrevMouseState, CurrMouseState));
        }

        public void Update()
        {
            PrevMouseState = CurrMouseState;
            CurrMouseState = Mouse.GetState();

            UpdateLeftMouseButton();
            UpdateRightMouseButton();
            UpdateScrollWheel();
            UpdateMousePosition();
        }
    }
}
