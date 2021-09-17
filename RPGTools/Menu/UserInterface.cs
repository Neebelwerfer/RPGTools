using Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RPGTools.Menu.Interfaces;
using System.Collections.Generic;

namespace RPGTools.Menu
{
    /// <summary>
    /// This class handles rendering and checking bounds of each added menu.
    /// TODO: Add menu layers to avoid overlapping of menus? or maybe just go through the list in reverse
    /// </summary>
    public class UserInterface
    {
        public IClickableMenu currentlyHoveredMenu;
        List<IClickableMenu> ClickableMenus;
        List<IMenu> NonClickableMenus;

        public UserInterface()
        {
            ClickableMenus = new List<IClickableMenu>();
            NonClickableMenus = new List<IMenu>();
            MouseInput.MouseMoved += EventManager_MouseMoved;
            MouseInput.LeftButtonClicked += EventManager_LeftButtonClicked;
            currentlyHoveredMenu = null;
        }

        private void EventManager_LeftButtonClicked(object sender, MouseEvent e)
        {
            currentlyHoveredMenu?.OnClick(new Vector2(e.CurrState.X, e.CurrState.Y));
        }

        private void EventManager_MouseMoved(object sender, MouseEvent e)
        {
            CheckBounds(new Vector2(e.CurrState.X, e.CurrState.Y), out IClickableMenu menu);
            currentlyHoveredMenu = menu;
        }

        public void AddMenuToList(IClickableMenu menu) => ClickableMenus.Add(menu);
        public void AddMenuToList(IMenu menu) => NonClickableMenus.Add(menu);
        public void RemoveMenuFromList(IClickableMenu menu) => ClickableMenus.Remove(menu);
        public void RemoveMenuFromList(IMenu menu) => NonClickableMenus.Remove(menu);
        public void CheckBounds(Vector2 CursorPos, out IClickableMenu menu)
        {
            bool HoverOne = false;
            menu = null;
            foreach(var m in ClickableMenus)
            {
                if (!m.IsActive()) continue;

                if (menu == null && m.InBounds(CursorPos))
                {
                    menu = m;
                    m.SetHover(true);
                    HoverOne = true;
                }
                else m.SetHover(false);
            }
            if (HoverOne) return;

            foreach (var nm in NonClickableMenus)
            {
                if (nm.IsActive() && nm.InBounds(CursorPos))
                {
                    return;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            ClickableMenus.ForEach((m) => m.Draw(spriteBatch, gameTime));
            NonClickableMenus.ForEach((nm) => nm.Draw(spriteBatch, gameTime));
        }
    }
}
