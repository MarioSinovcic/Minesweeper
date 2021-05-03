using MinesweeperService.Enums;
using MinesweeperService.Values;

namespace MinesweeperTests.Helpers.Stubs
{
    internal record GridWithAllTilesRevealedStub : Grid
    {
        private static readonly Tile[,] Tiles = {{new(TileType.Empty, TileStatus.Shown), new(TileType.Empty, TileStatus.Shown)}};
        
        internal GridWithAllTilesRevealedStub() : base(Tiles) { }
    }
}