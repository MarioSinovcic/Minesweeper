using System;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleInputHandler : IInputHandler
    {
        public Coords GetTurnInput() //TODO: input validation
        {
            Console.Write("Enter your co-ordinates\"0 2\": ");
            var input = Console.ReadLine(); 
            
            return new Coords((int)Char.GetNumericValue(input[0]), (int)Char.GetNumericValue(input[2]));
        }
    }
}