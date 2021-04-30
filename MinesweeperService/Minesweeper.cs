using System.Linq;
using MinesweeperService.Values;

namespace MinesweeperService
{
    public class Minesweeper 
    {
        private readonly RuleList _rules;
        private GameState _gameState; 

        public Minesweeper(GameState gameState)
        {
            _rules = new RuleList();
            _gameState = gameState;
        }
        
        public void PerformMove() 
        {
            UpdateGameStatus();
            CheckIfGameIsWon();
        }
        
        public GameState GetGameState() 
        {
            return _gameState; 
        }

        private void UpdateGameStatus()
        {
            var rules = _rules.GetRulesList();
            
            foreach (var rule in rules.Where(rule => rule.IsRuleApplicable(_gameState)))
            {
                _gameState = rule.UpdateGameState(_gameState);
                break;
            }
        }

        private void CheckIfGameIsWon()
        {
            var winningRule = _rules.GetWinningRule();
            
            if (winningRule.IsRuleApplicable(_gameState))
            {
                _gameState = winningRule.UpdateGameState(_gameState);
            }
        }
    }
}