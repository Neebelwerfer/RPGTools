using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UserInterface.Interfaces;

namespace UserInterface.MenuItem
{
    public class TextButton : IButton
    {
        private string DisplayText { get; }
        private Texture2D Background { get; set; }
        private Delegate ClickAction { get; }

        public TextButton(Rectangle button, string text, Action clickAction) : base(button)
        {
            DisplayText = text;
            ClickAction = clickAction;
        }

        public TextButton(Rectangle button, string text, Action clickAction, Texture2D backGround) : base(button)
        {
            DisplayText = text;
            ClickAction = clickAction;
            Background = backGround;
        }

        //public override void Draw(SpriteBatch spriteBatch)
        //{
        //    spriteBatch.Draw(Background, ButtonBounds, Color.White);
        //}

        public override bool OnHover(Vector2 mousePos)
        {
            return false;
        }

        public override bool OnLeftClick(Vector2 mousePos)
        {
            ClickAction.DynamicInvoke(null);
            return true;
        }
    }
}
