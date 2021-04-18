using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Values;

namespace Domain
{
    public static class RuleEvaluator //TODO: potential to refactor into list of IRules (polymorphic) -> isValid(), 
    {   //TODO: should not be static (only one class uses this)

        private const int TilesLeftToWin = 1;
        
        public static GameStatus ValidateInput(IGrid grid, Coords coords, GameStatus gameStatus)
        {
            var (x, y) = coords;
            if (x > grid.Width || x < 0 || y > grid.Height || y < 0)
            {
                return GameStatus.Error;
            }

            return gameStatus;
        }

        public static GameStatus EvaluateGameStatus(IGrid grid, Coords coords, GameStatus gameStatus)
        {
            if (gameStatus == GameStatus.SetFlag)
            {
                return GameStatus.Playing;
            }
            
            if (grid.GetTileTypeAt(coords) == TileType.Mine)
            {
                return GameStatus.Loss;
            }

            var tiles = GetTileList(grid);
            var emptyTilesCount = tiles.Count(tile => tile.Type == TileType.Empty);
            var shownTilesCount = tiles.Count(tile => tile.Status == TileStatus.Shown);

            return emptyTilesCount == shownTilesCount ? GameStatus.Win : GameStatus.Playing;
        }

        private static List<Tile> GetTileList(IGrid grid)
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