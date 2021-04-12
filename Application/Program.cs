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
            var outputHandler = new ConsoleOutputHandler();
            var inputHandler = new ConsoleInputHandler();
            
            var grid = new RandomGridSetup(10, 10, 5).CreateGrid();
            var gameState = new GameStateDTO {Grid = grid, Coords = null, GameStatus = GameStatus.Playing};
            //controller.setupGame() <- builder??
            
            outputHandler.DisplayGameState(gameState);
            while (true)
            {
                var coords = inputHandler.GetTurnInput();
                gameState = Minesweeper.PerformMove(new Move {Grid = grid, Coords = coords}); //TODO: setup mediating controller
                outputHandler.DisplayGameState(gameState);
            }
        }

    }
}