using Microsoft.Xna.Framework.Input;
using System;

namespace Inputs
{
    public class MouseEvent : EventArgs
    {
        public MouseEvent(MouseState prev, MouseState curr)
        {
            PrevState = prev;
            CurrState = curr;
        }

        public MouseEvent(MouseState prev, MouseState curr, int scrollWheelChange)
        {
            ScrollWheelChange = scrollWheelChange;
            PrevState = prev;
            CurrState = curr;
        }
        public MouseState PrevState { get; set; } 
        public MouseState CurrState { get; set; }
        public int ScrollWheelChange { get; set; }
    }
}
