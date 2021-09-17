namespace Map
{
    public class TileSelectedEvent
    {
        public TileSelectedEvent(Tile tileSelected)
        {
            TileSelected = tileSelected;
        }

        public Tile TileSelected { get; private set; }
    }
}
