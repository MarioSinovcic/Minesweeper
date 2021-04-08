using System.Linq;
using Domain.Entities;
using Domain.Enums;

namespace Domain
{
    public class RuleEvaluator
    {
        public static GameStatus EvaluateGameStatus(Grid grid, int[] selectedTile)
        {
            var xCoord = selectedTile[0];
            var yCoord = selectedTile[1];
            
            if (xCoord > grid.Width || xCoord < 0 || yCoord > grid.Height || yCoord < 0)
            {
                return GameStatus.Error;
            }

            if (grid.Tiles[xCoord, yCoord].Type.Equals(TileType.Mine))
            {
                return GameStatus.Loss;
            }

            var tiles = grid.Tiles.Cast<Tile>().ToList();
            var emptyTilesCount = tiles.Count(x => x.Type == TileType.Empty);
            var shownTilesCount = tiles.Count(x => x.Status == TileStatus.Shown) + 1;

            return emptyTilesCount == shownTilesCount ? GameStatus.Win : GameStatus.Playing;
        }
    }
}