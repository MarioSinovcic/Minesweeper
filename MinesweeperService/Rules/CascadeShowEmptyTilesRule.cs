using MinesweeperService.Enums;
using MinesweeperService.Rules.Interface;
using MinesweeperService.Values;

namespace MinesweeperService.Rules
{
    internal class CascadeShowEmptyTilesRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            var gameStatus = gameState.GameStatus;
            var grid = gameState.Grid;
            var coords = gameState.Coords;
            
            var neighbours = grid.GetNeighbouringMinesAt(coords);

            return gameStatus == GameStatus.Playing && neighbours == 0;
        }
        
        public GameState UpdateGameState(GameState gameState)
        {
            var updatedGrid = ShowAllSurroundingEmptyTiles(gameState.Grid, gameState.Coords);
            
            return new GameState(gameState.GameStatus, updatedGrid, gameState.Coords);
        }
        
        private static Grid ShowAllSurroundingEmptyTiles(Grid grid, Coords givenCoords)
        {
            var width = grid.Width;
            var height = grid.Height;

            if (grid.GetTileStatusAt(givenCoords) == TileStatus.Flag)
            {
                var updatedTile = new Tile(grid.GetTileTypeAt(givenCoords), TileStatus.Shown);
                grid.ReplaceTileAt(givenCoords, updatedTile);
            }
            
            for (var xOff = -1 ; xOff < 2; xOff++)
            {
                for (var yOff = -1; yOff < 2; yOff++)
                {
                    var xCoord = givenCoords.X + xOff;
                    var yCoord = givenCoords.Y + yOff;
                    var coords = new Coords(xCoord, yCoord);
                    
                    if (xCoord <= -1 || xCoord >= width || yCoord <= -1 || yCoord >= height) continue;
                    if (grid.GetTileTypeAt(coords) != TileType.Empty ||
                        grid.GetTileStatusAt(coords) != TileStatus.Hidden) continue;
                    var updatedTile = grid.GetInvertedTileAt(coords);
                    grid.ReplaceTileAt(coords, updatedTile);
                    
                    if (!(grid.GetNeighbouringMinesAt(coords) > 0))
                    {
                        ShowAllSurroundingEmptyTiles(grid, coords);
                    }
                }
            }

            return grid;
        }
    }
}