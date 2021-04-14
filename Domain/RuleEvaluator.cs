using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Values;

namespace Domain
{
    public static class RuleEvaluator //TODO: potential to refactor into list of IRules (polymorphic) -> isValid(), 
    {
        public static GameStatus EvaluateGameStatus(Grid grid, Coords coords)
        {
            var (x, y) = coords;
            if (x > grid.Width || x < 0 || y > grid.Height || y < 0)
            {
                return GameStatus.Error;
            }

            if (grid.GetTileTypeAt(coords).Equals(TileType.Mine))
            {
                return GameStatus.Loss;
            }

            var tiles = GetTileList(grid).Cast<Tile>().ToList();
            var emptyTilesCount = tiles.Count(tile => tile.Type == TileType.Empty);
            var shownTilesCount = tiles.Count(tile => tile.Status == TileStatus.Shown) + 1;

            return emptyTilesCount == shownTilesCount ? GameStatus.Win : GameStatus.Playing;
        }
        
        public static List<Tile> GetTileList(Grid grid)
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