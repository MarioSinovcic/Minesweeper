using Minesweeper_Service.Values;

namespace Minesweeper_Service.Rules.Interface
{
    public interface IRule
    {
        public bool IsRuleApplicable(GameState gameState);
        public GameState UpdateGameState(GameState gameState);
    }
}