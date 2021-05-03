using MinesweeperService.Enums;
using MinesweeperService.Values;

namespace MinesweeperTests.Helpers.Stubs
{
    internal record EmptyGridWithLeftTileRevealedStub : Grid
    {
        private static readonly Tile[,] Tiles = {{new(TileType.Empty, TileStatus.Shown), new(TileType.Empty), new(TileType.Empty)}};
        
        internal EmptyGridWithLeftTileRevealedStub() : base(Tiles) { }
    }
}