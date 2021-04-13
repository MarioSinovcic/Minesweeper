using Application.Controllers;
using Frontend;

namespace Application
{
    internal static class Program
    {
        private static void Main()
        {
            var outputHandler = new ConsoleOutputHandler(); //TODO: introduce a dependency injection system?
            var inputHandler = new ConsoleInputHandler();
            var ioFacade = new IOFacade(inputHandler, outputHandler);
            var gameController = new GameController();
            
            var gameState = gameController.SetupGame(); //TODO: use builder??
                
            ioFacade.DisplayGameState(gameState);
            while (true)
            {
                var coords = ioFacade.GetTurnInput(); //TODO: should be inputDTO
                gameState = gameController.HandleMove(coords, gameState); 
                ioFacade.DisplayGameState(gameState);
            }
        }

    }
}