using System.Collections.Generic;
using System.Linq;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Rules.Interface;
using Minesweeper_Service.Values;
using Minesweeper_Service.Values.Interfaces;

namespace Minesweeper_Service.Rules
{
    public class CheckWinRule : IRule
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
        
        private static IEnumerable<Tile> GetTileList(IGrid grid)
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