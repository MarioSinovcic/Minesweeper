using Domain.Enums;

namespace Domain.Entities
{
    public record Tile
    {
        public TileStatus Status { get; private set; } = TileStatus.Hidden;
        public TileType Type { get; init; }
        public void ShowTile()
        {
            Status = TileStatus.Shown;
        }
    }
}