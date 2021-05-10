using MinesweeperService.Enums;
using MinesweeperService.Rules.Interface;
using MinesweeperService.Values;

namespace MinesweeperService.Rules
{
    internal class ShowMineRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            var grid = gameState.Grid;
            var coords = gameState.Coords;
            
            return grid.GetTileTypeAt(coords) == TileType.Mine;
        }
        
        public GameState UpdateGameState(GameState gameState)
        {
            var updatedGrid = gameState.Grid;
            var coords = gameState.Coords;
            
            var updatedTile = updatedGrid.GetInvertedTileAt(coords);
            updatedGrid.ReplaceTileAt(coords, updatedTile);
            
            return new GameState(GameStatus.Loss, updatedGrid, coords);
        }
    }
}