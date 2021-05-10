using MinesweeperService.Enums;
using MinesweeperService.Rules.Interface;
using MinesweeperService.Values;

namespace MinesweeperService.Rules
{
    internal class ShowEmptyNumberedTileRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            var gameStatus = gameState.GameStatus;
            var grid = gameState.Grid;
            var coords = gameState.Coords;
            
            var neighbours = grid.GetNeighbouringMinesAt(coords);

            return gameStatus == GameStatus.Playing && neighbours > 0 && grid.GetTileTypeAt(coords) == TileType.Empty;
        }
        
        public GameState UpdateGameState(GameState gameState)
        {
            var updatedGrid = gameState.Grid;
            var coords = gameState.Coords;
            
            var updatedTile = updatedGrid.GetInvertedTileAt(coords);
            updatedGrid.ReplaceTileAt(coords, updatedTile);
            return new GameState(gameState.GameStatus, updatedGrid, coords);
        }
    }
}