using Application.GameBehaviour;
using Frontend.ConsoleIO;

namespace Frontend
{
    internal static class Program
    {
        private static void Main() //TODO: don't die on the first turn
        {
            //TODO: introduce a dependency injection system?
            var ioFacade = new IOFacade( new ConsoleInputHandler(), new ConsoleOutputHandler());
            var gameController = new GameController();
            
            var gameState = gameController.SetupRandomGameFromJson("SetupBehaviours/RandomGridSettings.json");
                
            ioFacade.DisplayGameState(gameState);
            while (true) //Play around with this, enviro.exit is a smell, -> !gameStatus.playing
            //play around with intermidate state, sorta menu (maybe even a summary)
            {
                var inputDTO = ioFacade.GetTurnInput(); //TODO: should be inputDTO
                gameState = gameController.HandleMove(inputDTO, gameState); 
                ioFacade.DisplayGameState(gameState);
            }
        }

    }
}