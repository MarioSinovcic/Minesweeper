using Application.Controllers;
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
            var gameController = new GameController();

            var gameState = gameController.SetupGame(); //<- builder??
                
            outputHandler.DisplayGameState(gameState);
            while (true)
            {
                var coords = inputHandler.GetTurnInput(); //should be inputDTO
                gameState = gameController.HandleMove(coords, gameState); //TODO: setup mediating controller
                outputHandler.DisplayGameState(gameState);
            }
        }

    }
}