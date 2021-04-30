using System;
using MinesweeperService.Enums;

namespace MinesweeperService.Values
{
    public sealed record Tile(TileType Type, TileStatus Status = TileStatus.Hidden)
    {
        public Tile ShowTile() => this with {Status = TileStatus.Shown};

        public static TileType GetRandomTileType(int mineFrequency)
        {
            var randomNum = new Random(); 
            return randomNum.Next(mineFrequency) < 1 ? TileType.Mine : TileType.Empty;
        }
    }
}