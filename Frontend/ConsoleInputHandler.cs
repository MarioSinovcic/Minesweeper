using System;
using Domain.Entities;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleInputHandler : IInputHandler
    {
        public Coords GetTurnInput() //TODO: input validation
        {
            var input = Console.ReadLine(); 
            
            return new Coords{X = (int)Char.GetNumericValue(input[0]), Y = (int)Char.GetNumericValue(input[2])};
        }
    }
}