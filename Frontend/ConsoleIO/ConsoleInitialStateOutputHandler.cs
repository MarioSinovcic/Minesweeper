using System;
using Domain.Enums;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend.ConsoleIO
{
    public class ConsoleInitialStateOutputHandler : IOutputHandler
    {

        public void DisplayGameState(GameState gameState)
        {
            Console.Clear();

            var gameStatus = gameState.GameStatus;

            if (gameStatus == GameStatus.FirstTurn)
            {
                DisplayGameRules();
            }
        }

        private void DisplayGameRules()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Welcome to Minesweeper!");
            Console.ResetColor();
            
            Console.WriteLine("Rules:");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" - show tiles by typing the x and y value with a space in between (e.g., \"3 5\")");
            Console.WriteLine(" - place flags by typing \"f\" then the x and y value with a space in between (e.g., \"f 2 1\")");
            Console.WriteLine(" - win by showing all tiles that aren't mines.");
            Console.ResetColor();
        }
    }
}