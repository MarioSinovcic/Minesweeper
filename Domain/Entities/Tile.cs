using Domain.Enums;

namespace Domain
{
    public sealed class Tile
    {
        public Tile(TileType type)
        {
            Type = type;
            Status = TileStatus.Hidden;
        }

        public Tile()
        {
        }

        public TileStatus Status { get; private set; }
        public TileType Type { get; set; }

        public void ShowTile()
        {
            Status = TileStatus.Shown;
        }
    }
}