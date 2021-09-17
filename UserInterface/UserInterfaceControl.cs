using Inputs;
using Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using UserInterface.Interfaces;
using UserInterface.Menus;

namespace UserInterface
{
    public class UserInterfaceControl
    {
        public static GraphicsDevice _GraphicsDevice { get; private set; }
        public static Texture2D WhitePixel { get; private set; }
        public enum GameState
        {
            MAIN_MENU,
            IMPORT_MAP,
            EDIT_MAP,
            PLAY_MAP,
            NONE
        };

        public GameState _GameState { get; private set; } = GameState.NONE;

        public MapControl MapControl { get; }
        private IDictionary<GameState, IUserInterface> UserInterfaces { get; set; }
        private IUserInterface currentInterface;

        public UserInterfaceControl(MapControl mapControl, GraphicsDevice graphicsDevice)
        {
            _GraphicsDevice = graphicsDevice;
            UserInterfaces = new Dictionary<GameState, IUserInterface>
            {
                { GameState.MAIN_MENU, new MainMenu(this) }
            };
            SetGameState(GameState.MAIN_MENU);
            MapControl = mapControl;
            KeyboardInputs.KeysClicked += OnKeyClicked;
            MouseInput.LeftButtonClicked += OnLeftButtonClicked;
            MouseInput.LeftButtonHold += OnLeftButtonHold;
            MouseInput.RightButtonClicked += OnRightButtonClicked;
            MouseInput.ScrollWheelChanged += OnScrollWheelChanged;
            MouseInput.MouseMoved += OnMouseMoved;
            WhitePixel = new Texture2D(_GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            WhitePixel.SetData(new Color[] { Color.Blue });
        }

        private void OnMouseMoved(object sender, MouseEvent e)
        {
            currentInterface?.OnMouseMoved(sender, e);
        }

        private void OnScrollWheelChanged(object sender, MouseEvent e)
        {
            bool? res = currentInterface?.OnScrollWheelChanged(sender, e);
            if ((res.HasValue && !res.Value) || !res.HasValue)
                MapControl?.OnScrollwheelChanged(sender, e);
        }

        private void OnRightButtonClicked(object sender, MouseEvent e)
        {
            bool? res = currentInterface?.OnRightButtonClicked(sender, e);
            if ((res.HasValue && !res.Value) || !res.HasValue)
                MapControl?.OnRightButtonClicked(sender, e);
        }

        private void OnLeftButtonHold(object sender, MouseEvent e)
        {
            bool? res = currentInterface?.OnLeftButtonHold(sender, e);
            if ((res.HasValue && !res.Value) || !res.HasValue)
                MapControl?.OnLeftButtonHold(sender, e);
        }

        private void OnLeftButtonClicked(object sender, MouseEvent e)
        {
            bool? res = currentInterface?.OnLeftButtonClicked(sender, e);
            if((res.HasValue && !res.Value) || !res.HasValue) 
                MapControl?.OnLeftButtonClicked(sender, e);
        }

        private void OnKeyClicked(object sender, KeysClickedEvent e)
        {
            currentInterface?.OnKeyClicked(sender, e);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentInterface?.Draw(spriteBatch, gameTime);
        }

        public void SetGameState(GameState state)
        {
            if (_GameState == state) return;
            _GameState = state;
            UserInterfaces.TryGetValue(state, out IUserInterface res);
            currentInterface = res;
        }
        
    }
}
