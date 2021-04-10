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
        
            if (neighbours > 0)
            {
                var grid = move.Grid;
                grid.Tiles[x, y] = grid.Tiles[x, y].ShowTile();
                return grid;
            }

            // var width = move.Grid.Width;
            // var height = move.Grid.Height;
            //
            // var mines = 0;
            //
            // if (neighbours == 0)
            // {
            //     for (var i = -1 ; i < 2; i++)
            //     {
            //         for (var j = -1; j < 2; j++)
            //         {
            //             var xCoord = (x + i + width) % width;
            //             var yCoord = (y + j + height) % height;
            //             if (get)
            //             {
            //                 
            //             }
            //
            //         }
            //     }
            // }

            return move.Grid;
        }
    }
}