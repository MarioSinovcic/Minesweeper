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
            var grid = gameState.Grid;
            var coords = gameState.Coords;
            
            var updateGrid = grid.WithRevealedTileAt(coords);
            
            return new GameState(GameStatus.Loss, updateGrid, coords);
        }
    }
}