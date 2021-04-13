using System.Linq;
using Domain.Entities;
using Domain.Enums;

namespace Domain
{
    public static class RuleEvaluator //TODO: potential to refactor into list of IRules
    {
        public static GameStatus EvaluateGameStatus(Grid grid, Coords selectedTileCoordinates)
        {
            var x = selectedTileCoordinates.X;
            var y = selectedTileCoordinates.Y;
            
            if (x > grid.Width || x < 0 || y > grid.Height || y < 0)
            {
                return GameStatus.Error;
            }

            if (grid.Tiles[x, y].Type.Equals(TileType.Mine))
            {
                return GameStatus.Loss;
            }

            var tiles = grid.Tiles.Cast<Tile>().ToList();
            var emptyTilesCount = tiles.Count(tile => tile.Type == TileType.Empty);
            var shownTilesCount = tiles.Count(tile => tile.Status == TileStatus.Shown) + 1;

            return emptyTilesCount == shownTilesCount ? GameStatus.Win : GameStatus.Playing;
        }
    }
}