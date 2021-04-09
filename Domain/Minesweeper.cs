using Domain.DTOs;
using Domain.Entities;

namespace Domain
{
    public class Minesweeper
    {
        public void PerformMove(Move move)
        {
            //gather gameState
            //var updatedGameStatus = RuleEvaluator.EvaluateGameStatus(move.Grid, move.Coords);
            
            //manipulate Grid
            //var updatedGrid = UpdateGrid(move.Grid, move.Coords);
            
            //return new GameStateDTO {GameStatus = gameStatus, Grid = grid, PlayerMove = selectedTileCoordinates};
        }

        // private Grid UpdateGrid(Grid grid, Coords selectedTileCoordinates)
        // {
        //     var x = selectedTileCoordinates.X;
        //     var y = selectedTileCoordinates.Y;
        //
        //     var neighbours = grid.GetNeighbouringMines(y, x);
        //     
        //     
        // }
    }
}