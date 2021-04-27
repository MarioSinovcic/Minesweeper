using Minesweeper_Controller.GameBehaviour;
using Minesweeper_Client.ConsoleIO;
using Minesweeper_Client.ConsoleIO.Output;

namespace Minesweeper_Client
{
    internal static class Program //rename all the folders, so that the customer can understand it (domain)
    {
        private static void Main() 
        {
            var ioFacade = new IOFacade( new ConsoleInputHandler(), new ConsoleOutputHandlerFacade()); 
            var gameController = new GameController();
            
            var gameState = gameController.SetupRandomGameFromJson("SetupBehaviours/RandomGridSettings.json");
                
            ioFacade.DisplayGameState(gameState);
            while (true)
            {
                var inputDTO = ioFacade.GetTurnInput(); 
                gameState = gameController.HandleMove(inputDTO, gameState); 
                ioFacade.DisplayGameState(gameState);
            }
        }

    }
}