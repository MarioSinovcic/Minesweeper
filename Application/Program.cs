using Application.Behaviour.Setup;
using Domain;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Frontend;

namespace Application
{
    class Program
    {
        //dependencies injection stuff
        
        private static void Main()
        {
            var grid = new JsonGridSetup("/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/Minesweeper Tests/Grid Fakes/FiveMines_LargeGrid.json").CreateGrid();
            //var grid = new RandomGridSetup(10, 10, 15).CreateGrid();
            grid = Minesweeper.PerformMove(new Move {Grid = grid, Coords = new Coords {X = 0, Y = 3}}).Grid;
            
            var gameState = new GameStateDTO {Grid = grid, Coords = null, GameStatus = GameStatus.Playing};

            var outputHandler = new ConsoleOutputHandler();
            
            //controller.setupGame() <- builder??
        
            //while (true)
            //{
            //var InputDTO = inputHandler.getInput();
            //var GameStateDTO = controller.handleMove(inputDTO);
            outputHandler.DisplayGameState(gameState);
            //}
        }

    }
}