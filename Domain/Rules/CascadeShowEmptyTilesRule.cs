using Domain.Enums;
using Domain.Rules.Interface;
using Domain.Values;
using Domain.Values.Interfaces;

namespace Domain.Rules
{
    public class CascadeShowEmptyTilesRule : IRule
    {
        public bool IsRuleApplicable(GameState gameState)
        {
            var gameStatus = gameState.GameStatus;
            var grid = gameState.Grid;
            var coords = gameState.Coords;
            
            var neighbours = grid.GetNeighbouringMines(coords);

            return gameStatus == GameStatus.Playing && neighbours == 0;
        }
        
        public GameState UpdateGameState(GameState gameState)
        {
            var updatedGrid = ShowAllSurroundingEmptyTiles(gameState.Grid, gameState.Coords);
            
            return new GameState(gameState.GameStatus, updatedGrid, gameState.Coords);
        }
        
        private static IGrid ShowAllSurroundingEmptyTiles(IGrid grid, Coords givenCoords)
        {
            var width = grid.Width;
            var height = grid.Height;

            if (grid.GetTileStatusAt(givenCoords) == TileStatus.Flag)
            {
                var updatedTile = new Tile(grid.GetTileTypeAt(givenCoords), TileStatus.Shown);
                grid.ReplaceTile(givenCoords, updatedTile);
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
                    var updatedTile = grid.GetInvertTileStatus(coords);
                    grid.ReplaceTile(coords, updatedTile);
                    
                    if (!(grid.GetNeighbouringMines(coords) > 0))
                    {
                        ShowAllSurroundingEmptyTiles(grid, coords);
                    }
                }
            }

            return grid;
        }
    }
}