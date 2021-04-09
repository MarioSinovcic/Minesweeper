using Domain.DTOs;
using Domain.Entities;

namespace Domain
{
    public class Minesweeper
    {
        public GameStateDTO PerformMove(Move move)
        {
            //gather gameState
            var updatedGameStatus = RuleEvaluator.EvaluateGameStatus(move.Grid, move.Coords);
            
            //manipulate Grid
            //var updatedGrid = UpdateGrid(move);
            
            return new GameStateDTO {GameStatus = updatedGameStatus, PlayerMove = move};
        }

        // private Grid UpdateGrid(Move move)
        // {
        //     var x = move.Coords.X;
        //     var y = move.Coords.Y;
        //
        //     var neighbours = move.Grid.GetNeighbouringMines(y, x);
        //
        //     if (neighbours == 0)
        //     {
        //         UpdateGrid()
        //     }
        // }
    }
}