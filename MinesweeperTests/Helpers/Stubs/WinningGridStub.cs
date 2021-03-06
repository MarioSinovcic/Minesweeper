using MinesweeperController.SetupBehaviours.Factories;
using MinesweeperService.Enums;
using MinesweeperService.Values;

namespace MinesweeperTests.Helpers.Stubs
{
    internal record WinningGridStub : Grid
    {
        private const string PathName = "/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/MinesweeperTests/Helpers/Fakes/Grids/OneCornerMine.json";
        private static Grid Grid = new JsonGridSetupFactory(PathName).CreateGrid();

        public WinningGridStub() : base(Grid)
        {
            for (var i = 0; i < Grid.Width; i++)
            {
                for (var j = 0; j < Grid.Height; j++)
                {
                    var coords = new Coords(i, j);
                    if (Grid.GetTileTypeAt(coords) != TileType.Empty) continue;
                    Grid = Grid.WithRevealedTileAt(coords);
                }
            }

            //setting up one last tile that is not shown, this tile needs to be selected to win the game
            var winningTileCoords = new Coords(1, 1);
            var lastTileNeededToWin = new Tile(TileType.Empty); 
            Grid = Grid.WithNewTileAt(winningTileCoords,lastTileNeededToWin);
        }
    }
}