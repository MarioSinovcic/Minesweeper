using Application.Behaviour.Setup;
using Domain.Enums;
using Domain.Values;

namespace Minesweeper_Tests.Stubs
{
    public record WinningGridStub : Grid
    {
        private const string PathName = "/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/Minesweeper Tests/Fakes/Grids/OneCornerMine.json";
        private static readonly Grid Grid = (Grid) new JsonGridSetup(PathName).CreateGrid();

        public WinningGridStub() : base(Grid)
        {
            for (var i = 0; i < Grid.Width; i++)
            {
                for (var j = 0; j < Grid.Height; j++)
                {
                    var coords = new Coords(i, j);
                    if (Grid.GetTileTypeAt(coords) != TileType.Empty) continue;
                    var updatedTile = Grid.GetInvertTileStatus(coords);
                    Grid.ReplaceTile(coords,updatedTile);
                }
            }

            var winningTileCoords = new Coords(1, 1);
            var lastTileNeededToWin = new Tile(TileType.Empty); //last tile not shown, needs to be selected to win
            Grid.ReplaceTile(winningTileCoords,lastTileNeededToWin);
        }
    }
}