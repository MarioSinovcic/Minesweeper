using MinesweeperService.Enums;
using MinesweeperService.Rules.Interface;
using MinesweeperService.Values;

namespace MinesweeperService.Rules
{
    internal class InvalidInputRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            var grid = gameState.Grid;
            var (x, y) = gameState.Coords;
            
            return x >= grid.Width || x < 0 || y >= grid.Height || y < 0;
        }

        public GameState UpdateGameState(GameState gameState)
        {
            return new GameState(GameStatus.Error, gameState.Grid, gameState.Coords);
        }
    }
}