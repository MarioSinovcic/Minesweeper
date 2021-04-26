using Domain.Enums;
using Domain.Rules.Interface;
using Domain.Values;

namespace Domain.Rules
{
    public class ShowMineRule : IRule
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
            
            var updatedTile = updatedGrid.GetInvertTileStatus(coords);
            updatedGrid.ReplaceTile(coords, updatedTile);
            
            return new GameState(GameStatus.Loss, updatedGrid, coords);
        }
    }
}