using System;
using Application.GameBehaviour.DTOs;
using Domain.Enums;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleInputHandler : IInputHandler
    {
        private const string InputPrompt = "Enter your co-ordinates\"0 2\": ";
        
        public InputDTO GetTurnInput() //TODO: input validation
        {
            Console.Write(InputPrompt);
            var input = Console.ReadLine();

            Coords coords;
            if (input[0].Equals('f'))
            {
                coords = new Coords((int)Char.GetNumericValue(input[2]), (int)Char.GetNumericValue(input[4]));
                return new InputDTO(GameStatus.SetFlag, coords);
            }
            
            coords = new Coords((int)Char.GetNumericValue(input[0]), (int)Char.GetNumericValue(input[2]));
            return new InputDTO(GameStatus.Playing, coords);
        }
    }
}