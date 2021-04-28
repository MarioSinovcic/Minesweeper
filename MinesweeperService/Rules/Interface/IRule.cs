using MinesweeperService.Values;

namespace MinesweeperService.Rules.Interface
{
    public interface IRule
    {
        public bool IsRuleApplicable(GameState gameState);
        public GameState UpdateGameState(GameState gameState);
    }
}