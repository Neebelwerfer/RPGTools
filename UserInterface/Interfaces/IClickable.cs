using Microsoft.Xna.Framework;

namespace UserInterface.Interfaces
{
    internal interface IClickable
    {
        bool OnLeftClick(Vector2 mousePos);
        bool OnLeftHold(Vector2 mousePos);
        bool OnRigthClick(Vector2 mousePos);
        bool OnRigthHold(Vector2 mousePos);
    }
}
