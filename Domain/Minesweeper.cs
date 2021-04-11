using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Domain
{
    public class Minesweeper
    {
        public static GameStateDTO PerformMove(Move move)
        {
            //gather gameState
            var updatedGameStatus = RuleEvaluator.EvaluateGameStatus(move.Grid, move.Coords);
            
            //manipulate Grid
            var updatedGrid = UpdateGrid(move);
            
            return new GameStateDTO {GameStatus = updatedGameStatus, Grid = updatedGrid, Coords = move.Coords};
        }

        private static Grid UpdateGrid(Move move)
        {
            var x = move.Coords.X;
            var y = move.Coords.Y;
        
            var neighbours = move.Grid.GetNeighbouringMines(move.Coords);
            var grid = move.Grid;

            switch (neighbours)
            {
                case > 0:
                    grid.Tiles[y, x] = grid.Tiles[y, x].ShowTile();
                    return grid;
                case 0:
                    grid = ShowAllSurroundingEmptyTiles(grid, x, y);
                    break;
            }
            return move.Grid;
        }

        private static Grid ShowAllSurroundingEmptyTiles(Grid grid, int x,int y)
        {
            var width = grid.Width;
            var height = grid.Height;
            
            for (var xoff = -1 ; xoff < 2; xoff++)
            {
                for (var yoff = -1; yoff < 2; yoff++)
                {
                    var xCoord = x + xoff;
                    var yCoord = y + yoff;
                    if (xCoord <= -1 || xCoord >= width || yCoord <= -1 || yCoord >= height) continue;
                    if (grid.Tiles[yCoord,xCoord].Type.Equals(TileType.Empty) &&
                        grid.Tiles[yCoord,xCoord].Status.Equals(TileStatus.Hidden))
                    {
                        grid.Tiles[yCoord, xCoord] = grid.Tiles[yCoord, xCoord].ShowTile();
                        if (!(grid.GetNeighbouringMines(new Coords {X = xCoord, Y = yCoord}) > 0))
                        {
                            ShowAllSurroundingEmptyTiles(grid, xCoord, yCoord);
                        }
                    }
                }
            }
            return grid;
        }
    }
}