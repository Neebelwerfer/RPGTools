using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace Inputs
{
    public class KeyboardInputs
    {
        public static KeyboardState PrevKeyState { get; private set; }
        public static KeyboardState CurrKeyState { get; private set; }

        public static event EventHandler<KeysClickedEvent> KeysClicked;
        
        public KeyboardInputs() 
        {
            PrevKeyState = Keyboard.GetState();
            CurrKeyState = PrevKeyState;
        }

        public void Update()
        {
            PrevKeyState = CurrKeyState;
            CurrKeyState = Keyboard.GetState();

            var clickedKeys = PrevKeyState.GetPressedKeys().Where((k) => !CurrKeyState.GetPressedKeys().Contains(k)).ToList();
            if (clickedKeys.Count() > 0) KeysClicked?.Invoke(this, new KeysClickedEvent(clickedKeys)); 
        }
    }
}
