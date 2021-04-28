using MinesweeperService.Enums;

namespace MinesweeperService.Values
{
    public sealed record Tile(TileType Type, TileStatus Status = TileStatus.Hidden)
    {
        public Tile ShowTile() => this with {Status = TileStatus.Shown};
    }
}