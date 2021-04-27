using System.Collections.Generic;
using Minesweeper_Controller.SetupBehaviours.Factories;
using Minesweeper_Service;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;
using Minesweeper_Service.Values.Interfaces;

namespace Minesweeper_Tests
{
    internal static class TestExtensions
    {
        public static GameState PerformMove(string jsonGridPath, GameStatus gameStatus, Coords coords)
        {
            var grid = new JsonGridSetupFactory(jsonGridPath).CreateGrid();
            var minesweeper = new Minesweeper(new GameState(gameStatus, grid, coords));
            minesweeper.PerformMove();
            return minesweeper.GetGameState();
        }
        
        public static GameState PerformMove(IGrid grid, GameStatus gameStatus, Coords coords)
        {
            var minesweeper = new Minesweeper(new GameState(gameStatus, grid, coords));
            minesweeper.PerformMove();
            return minesweeper.GetGameState();
        }
        
        public static IEnumerable<Tile> LoopThroughGrid(IGrid grid)
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