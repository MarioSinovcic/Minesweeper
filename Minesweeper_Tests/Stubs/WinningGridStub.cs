using Minesweeper_Controller.SetupBehaviours.Factories;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;

namespace Minesweeper_Tests.Stubs
{
    public record WinningGridStub : Grid
    {
        //TODO: bad string path
        private const string PathName = "/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/Minesweeper_Tests/Fakes/Grids/OneCornerMine.json";
        private static readonly Grid Grid = (Grid) new JsonGridSetupFactory(PathName).CreateGrid();

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

            //setting up one last tile that is not shown, this needs to be selected to win the game
            var winningTileCoords = new Coords(1, 1);
            var lastTileNeededToWin = new Tile(TileType.Empty); 
            Grid.ReplaceTile(winningTileCoords,lastTileNeededToWin);
        }
    }
}