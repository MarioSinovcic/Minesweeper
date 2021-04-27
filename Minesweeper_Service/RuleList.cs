using System.Collections.Generic;
using System.Collections.ObjectModel;
using Minesweeper_Service.Rules;
using Minesweeper_Service.Rules.Interface;

namespace Minesweeper_Service
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