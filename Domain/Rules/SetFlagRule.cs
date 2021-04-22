using Domain.Enums;
using Domain.Rules.Interface;
using Domain.Values;

namespace Domain.Rules
{
    public class SetFlagRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            return gameState.GameStatus == GameStatus.SetFlag;
        }

        public GameState UpdateGameState(GameState gameState)
        {
            var updatedGrid = gameState.Grid;
            var coords = gameState.Coords;
            
            var tileType = updatedGrid.GetTileTypeAt(coords);
            updatedGrid.ReplaceTile(coords, new Tile(tileType, TileStatus.Flag));
            
            return new GameState(GameStatus.Playing, updatedGrid, gameState.Coords);
        }
    }
}