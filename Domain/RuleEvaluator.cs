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

            return grid.Tiles[xCoord, yCoord].Type.Equals(TileType.Mine) ? GameStatus.Loss : GameStatus.Playing;
            
            // var tiles = grid.Tiles.Cast<Tile>().ToList();
            // var mineCount = tiles.Count(x => x.Type == TileType.Mine);
        }
    }
}