using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inputs;
using Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UserInterface.Interfaces;
using UserInterface.MenuItem;

namespace UserInterface.Menus
{
    public class MainMenu : IUserInterface
    {
        private UserInterfaceControl Context { get; }
        
        public MainMenu() : base()
        {
        }

        public MainMenu(UserInterfaceControl context) : base()
        {
            Context = context;
            MenuItems.Add(new TextButton(new Rectangle(200, 200, 100, 100), "Hej", SetStateToMap, UserInterfaceControl.WhitePixel));
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            MenuItems.ForEach((m) => m.Draw(spriteBatch));
        }

        private void SetStateToMap()
        {
            Context.SetGameState(UserInterfaceControl.GameState.PLAY_MAP);
            Texture2D test = Texture2D.FromStream(UserInterfaceControl._GraphicsDevice, File.OpenRead("C:\\Users\\Jakob\\Desktop\\DnDMapTest\\test1.jpg"));
            Context.MapControl.LoadMap(test);
        }
    }
}
