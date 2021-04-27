using Minesweeper_Service.Enums;

namespace Minesweeper_Service.Values
{
    public sealed record Tile(TileType Type, TileStatus Status = TileStatus.Hidden)
    {
        public Tile ShowTile() => this with {Status = TileStatus.Shown};
    }
}