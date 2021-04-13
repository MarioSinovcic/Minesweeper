using System;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleInputHandler : IInputHandler
    {
        public Coords GetTurnInput() //TODO: input validation
        {
            var input = Console.ReadLine(); 
            
            return new Coords((int)Char.GetNumericValue(input[0]), (int)Char.GetNumericValue(input[2]));
        }
    }
}