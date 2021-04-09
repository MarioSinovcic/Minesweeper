using System;
using Domain.DTOs;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleInputHandler : IOutputHandler
    {
        public void DisplayGameState(GameStateDTO gameState)
        {
            Console.WriteLine("yeehaw");
        }
    }
}