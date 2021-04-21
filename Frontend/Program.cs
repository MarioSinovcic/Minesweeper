using Application.GameBehaviour;
using Frontend.ConsoleIO;

namespace Frontend
{
    internal static class Program
    {
        private static void Main()
        {
            //TODO: introduce a dependency injection system?
            var ioFacade = new IOFacade( new ConsoleInputHandler(), new ConsoleOutputHandler());
            var gameController = new GameController();
            
            var gameState = gameController.SetupRandomGameFromJson("SetupBehaviours/RandomGridSettings.json");
                
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