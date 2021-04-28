using System.Collections.Generic;
using System.Collections.ObjectModel;
using MinesweeperService.Rules;
using MinesweeperService.Rules.Interface;

namespace MinesweeperService
{
    internal class RuleList 
    {
        private readonly IEnumerable<IRule> _gameRules = new ReadOnlyCollection<IRule>(new IRule[]
            {
                new InvalidInputRule(),
                new SetFlagRule(),
                new ShowMineRule(),
                new ShowEmptyNumberedTileRule(),
                new CascadeShowEmptyTilesRule()
            }
        );
        
        internal IRule GetWinningRule()
        {
            return new CheckWinRule();
        }

        internal IEnumerable<IRule> GetRulesList()
        {
            return _gameRules;
        }
    }
}