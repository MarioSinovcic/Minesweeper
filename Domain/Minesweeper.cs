using Domain.Enums;
using Domain.Values;

namespace Domain
{
    public static class Minesweeper //TODO: setup mediator
    {
        //TODO: holds state, with command query opps <- priority
        //TODO: game state factory
        //TODO: IRules
        
        public static GameState PerformMove(Move move)
        {
            var updatedGameStatus = RuleEvaluator.EvaluateGameStatus(move.Grid, move.Coords);
            var updatedGrid = UpdateGrid(move);
            
            return new GameState(updatedGameStatus, updatedGrid, move.Coords);
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
                    ShowAllSurroundingEmptyTiles(grid, x, y);
                    break;
            }
            return move.Grid;
        }

        private static void ShowAllSurroundingEmptyTiles(Grid grid, int x, int y)
        {
            var width = grid.Width;
            var height = grid.Height;
            
            for (var xOff = -1 ; xOff < 2; xOff++)
            {
                for (var yOff = -1; yOff < 2; yOff++)
                {
                    var xCoord = x + xOff;
                    var yCoord = y + yOff;
                    if (xCoord <= -1 || xCoord >= width || yCoord <= -1 || yCoord >= height) continue;
                    if (!grid.Tiles[yCoord, xCoord].Type.Equals(TileType.Empty) ||
                        !grid.Tiles[yCoord, xCoord].Status.Equals(TileStatus.Hidden)) continue;
                    grid.Tiles[yCoord, xCoord] = grid.Tiles[yCoord, xCoord].ShowTile();
                    if (!(grid.GetNeighbouringMines(new Coords(xCoord, yCoord)) > 0))
                    {
                        ShowAllSurroundingEmptyTiles(grid, xCoord, yCoord);
                    }
                }
            }
        }
    }
}