using System.Linq;
using Minesweeper_Service.Values;

namespace Minesweeper_Service
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
        
        public void PerformMove() //command
        {
            UpdateGameStatus();
            CheckIfGameIsWon();
        }
        
        public GameState GetGameState() //query
        {
            return _gameState; //TODO: remove all public methods
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