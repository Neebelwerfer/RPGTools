using Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;
using System.Collections.Generic;

namespace UserInterface.Interfaces
{
    public abstract class IUserInterface
    {
        public List<IMenuItem> MenuItems { get; set; }
        public IUserInterface ()
        {
            MenuItems = new List<IMenuItem>();
        }
        public virtual bool OnScrollWheelChanged(object sender, MouseEvent e)
        {
            return false;
        }
        public virtual bool OnRightButtonClicked(object sender, MouseEvent e)
        {
            Vector2 cursor = new Vector2(e.CurrState.X, e.CurrState.Y);
            foreach (IMenuItem i in MenuItems)
            {
                if (i is IClickable c)
                    if (i.InBounds(cursor))
                    {
                        c.OnRigthClick(cursor);
                        return true;
                    }
            }
            return false;
        }
        public virtual bool OnLeftButtonHold(object sender, MouseEvent e)
        {
            Vector2 cursor = new Vector2(e.CurrState.X, e.CurrState.Y);
            foreach (IMenuItem i in MenuItems)
            {
                if (i is IClickable c)
                    if (i.InBounds(cursor))
                    {
                        c.OnLeftHold(cursor);
                        return true;
                    }
            }
            return false;
        }
        public virtual bool OnLeftButtonClicked(object sender, MouseEvent e)
        {
            Vector2 cursor = new Vector2(e.CurrState.X, e.CurrState.Y);
            foreach (IMenuItem i in MenuItems)
            {
                if (i is IClickable c)
                    if (i.InBounds(cursor))
                    {
                        c.OnLeftClick(cursor);
                        return true;
                    }
            }
            return false;
        }
        public virtual void OnMouseMoved(object sender, MouseEvent e)
        {
            Vector2 cursor = new Vector2(e.CurrState.X, e.CurrState.Y);
            foreach (IMenuItem i in MenuItems)
            {
                if (i is IHoverable h)
                    if (i.InBounds(cursor))
                    {
                        h.Hovered = true;
                        h.OnHover(cursor);
                    }
                    else
                        h.Hovered = false;
            }
        }
        public virtual bool OnKeyClicked(object sender, KeysClickedEvent e)
        {
            return false;
        }
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);
    }
}
