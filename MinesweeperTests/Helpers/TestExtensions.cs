using System.Collections.Generic;
using MinesweeperController.SetupBehaviours.Factories;
using MinesweeperService;
using MinesweeperService.Enums;
using MinesweeperService.Values;

namespace MinesweeperTests.Helpers
{
    internal static class TestExtensions
    {
        internal static GameState PerformMove(string jsonGridPath, GameStatus gameStatus, Coords coords)
        {
            var grid = new JsonGridSetupFactory(jsonGridPath).CreateGrid();
            var minesweeper = new Minesweeper(new GameState(gameStatus, grid, coords));
            minesweeper.PerformMove();
            return minesweeper.GetGameState();
        }
        
        internal static GameState PerformMove(Grid grid, GameStatus gameStatus, Coords coords)
        {
            var minesweeper = new Minesweeper(new GameState(gameStatus, grid, coords));
            minesweeper.PerformMove();
            return minesweeper.GetGameState();
        }
        
        internal static IEnumerable<Tile> LoopThroughGrid(Grid grid)
        {
            var tiles = new List<Tile>();
            
            for(var i =0; i<grid.Height;i++){
                for(var j =0; j<grid.Width;j++)
                {
                    var tileType = grid.GetTileTypeAt(new Coords(j, i));
                    var tileStatus = grid.GetTileStatusAt(new Coords(j, i));
                    
                    tiles.Add(new Tile(tileType, tileStatus));
                }
            }
            return tiles;
        }
    }
}