using Domain.Values;

namespace Domain.Rules.Interface
{
    public interface IRule
    {
        public bool IsRuleApplicable(GameState gameState);
        public GameState UpdateGameState(GameState gameState);
    }
}