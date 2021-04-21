using System.Linq;
using Domain.Values;

namespace Domain
{
    public class Minesweeper //TODO: setup mediator
    {
        private readonly RuleList _rules;
        private GameState _gameState; //TODO: make immutable?

        public Minesweeper(GameState gameState)
        {
            _rules = new RuleList();
            _gameState = gameState;
        }
        
        //TODO: game state factory
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
            
            foreach (var rule in rules.Where(rule => rule.IsActive(_gameState)))
            {
                _gameState = rule.UpdateGameState(_gameState);
                break;
            }
        }

        private void CheckIfGameIsWon()
        {
            var winningRule = _rules.GetWinningRule();
            
            if (winningRule.IsActive(_gameState))
            {
                _gameState = winningRule.UpdateGameState(_gameState);
            }
        }
    }
}