using Domain.Enums;
using Domain.Rules.Interface;
using Domain.Values;

namespace Domain.Rules
{
    public class InvalidInputRule : IRule
    {
        public bool IsActive(GameState gameState)
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