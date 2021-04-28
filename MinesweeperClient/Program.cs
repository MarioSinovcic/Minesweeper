using MinesweeperClient.ConsoleIO;
using MinesweeperClient.ConsoleIO.Input;
using MinesweeperClient.ConsoleIO.Output;
using MinesweeperController.GameBehaviour;

namespace MinesweeperClient
{
    internal static class Program 
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