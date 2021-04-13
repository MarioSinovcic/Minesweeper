using Application.Behaviour.Setup;
using Domain.Entities;
using Domain.Enums;

namespace Minesweeper_Tests.Stubs
{
    public record WinningGridStub : Grid
    {
        private const string PathName = "/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/Minesweeper Tests/Fakes/Grids/OneCornerMine.json";
        private static readonly Grid Grid = new JsonGridSetup(PathName).CreateGrid();

        public WinningGridStub() : base(Grid.Tiles)
        {
            for (var i = 0; i < Grid.Width; i++)
            {
                for (var j = 0; j < Grid.Height; j++)
                {
                    if (Grid.Tiles[j,i].Type == TileType.Empty)
                    {
                        Grid.Tiles[j,i] = Grid.Tiles[j, i].ShowTile();
                    }
                }
            }
            Grid.Tiles[1, 1] = new Tile(TileType.Empty); //last tile not shown, needs to be selected to win
        }
    }
}