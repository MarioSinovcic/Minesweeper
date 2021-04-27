using System;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;

namespace Minesweeper_Client.ConsoleIO.Output
{
    public class ConsoleOutputHandlerFacade : IOutputHandler
    {
        private readonly IOutputHandler _gridHandler = new ConsoleBoardOutputHandler();
        private readonly IOutputHandler _firstTurnHandler = new ConsoleInitialStateOutputHandler();

        public void DisplayGameState(GameState gameState)
        {
            if (gameState.GameStatus == GameStatus.FirstTurn)
            {
                _firstTurnHandler.DisplayGameState(gameState);
                Console.Write("Press the enter key to get started: ");
                Console.ReadLine();
            }
            _gridHandler.DisplayGameState(gameState);
        }
    }
}