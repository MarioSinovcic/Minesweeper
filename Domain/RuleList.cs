using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Rules;
using Domain.Rules.Interface;

namespace Domain
{
    public class RuleList
    {
        private readonly IEnumerable<IRule> _gameRules = new ReadOnlyCollection<IRule>(new IRule[]
            {
                new InvalidInputRule(),
                new SetFlagRule(),
                new ShowMineRule(),
                new ShowEmptyNumberedTileRule(),
                new CascadeShowEmptyTilesRule(),
            }
        );
        
        public IRule GetWinningRule()
        {
            return new CheckWinRule();
        }

        public IEnumerable<IRule> GetRulesList()
        {
            return _gameRules;
        }
    }
}