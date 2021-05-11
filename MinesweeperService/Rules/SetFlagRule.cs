using MinesweeperService.Enums;
using MinesweeperService.Rules.Interface;
using MinesweeperService.Values;

namespace MinesweeperService.Rules
{
    internal class SetFlagRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            return gameState.GameStatus == GameStatus.SetFlag;
        }

        public GameState UpdateGameState(GameState gameState)
        {
            var grid = gameState.Grid;
            var coords = gameState.Coords;
            
            var tileType = grid.GetTileTypeAt(coords);
            var updatedTile = new Tile(tileType, TileStatus.Flag);

            var updateGrid = grid.WithNewTileAt(coords, updatedTile);
            
            return new GameState(GameStatus.Playing, updateGrid, gameState.Coords);
        }
    }
}