using System.Collections.Generic;
using System.Linq;
using MinesweeperService.Enums;
using MinesweeperService.Rules.Interface;
using MinesweeperService.Values;

namespace MinesweeperService.Rules
{
    internal class CheckWinRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            if (gameState.GameStatus == GameStatus.Playing)
            {
                var tiles = GetTileList(gameState.Grid).ToList();
                var emptyTilesCount = tiles.Count(tile => tile.Type == TileType.Empty);
                var shownTilesCount = tiles.Count(tile => tile.Status == TileStatus.Shown);

                return emptyTilesCount == shownTilesCount;
            }

            return false;
        }

        public GameState UpdateGameState(GameState gameState)
        {
            return new GameState(GameStatus.Win, gameState.Grid, gameState.Coords);
        }
        
        private static IEnumerable<Tile> GetTileList(Grid grid)
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