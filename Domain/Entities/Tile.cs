using Domain.Enums;

namespace Domain.Entities
{
    public record Tile
    {
        public TileStatus Status { get; private init; } = TileStatus.Hidden;
        public TileType Type { get; init; }
        public Tile ShowTile() => this with {Status = TileStatus.Shown};
    }
}