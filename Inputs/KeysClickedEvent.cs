using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Inputs
{
    public class KeysClickedEvent
    {
        public KeysClickedEvent(List<Keys> keysClicked) => KeysClicked = keysClicked;
        public List<Keys> KeysClicked { get; private set; }
    }
}
