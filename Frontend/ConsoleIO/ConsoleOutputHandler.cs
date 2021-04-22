using System;
using Domain.Enums;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend.ConsoleIO
{
    public class ConsoleOutputHandler : IOutputHandler
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