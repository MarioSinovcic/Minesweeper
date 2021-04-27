using Minesweeper_Service.Enums;
using Minesweeper_Service.Rules.Interface;
using Minesweeper_Service.Values;

namespace Minesweeper_Service.Rules
{
    public class ShowEmptyNumberedTileRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            var gameStatus = gameState.GameStatus;
            var grid = gameState.Grid;
            var coords = gameState.Coords;
            
            var neighbours = grid.GetNeighbouringMines(coords);

            return gameStatus == GameStatus.Playing && neighbours > 0 && grid.GetTileTypeAt(coords) == TileType.Empty;
        }
        
        public GameState UpdateGameState(GameState gameState)
        {
            var updatedGrid = gameState.Grid;
            var coords = gameState.Coords;
            
            var updatedTile = updatedGrid.GetInvertTileAt(coords);
            updatedGrid.ReplaceTile(coords, updatedTile);
            return new GameState(gameState.GameStatus, updatedGrid, coords);
        }
    }
}