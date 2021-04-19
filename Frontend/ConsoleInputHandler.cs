using System;
using Application.GameBehaviour.DTOs;
using Domain.Enums;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleInputHandler : IInputHandler
    {
        private const string InputPrompt = "Enter your co-ordinates e.g.,\"0 2\": ";
        private const string ErrorPrompt = "Looks like that input wasn't valid, please try again: ";
        
        public InputDTO GetTurnInput() //TODO: implement L
        {
            var input = GetValidatedInput();

            Coords coords;
            if (input[0].Equals('f'))
            {
                coords = new Coords((int)Char.GetNumericValue(input[2]), (int)Char.GetNumericValue(input[4]));
                return new InputDTO(GameStatus.SetFlag, coords);
            }
            
            coords = new Coords((int)Char.GetNumericValue(input[0]), (int)Char.GetNumericValue(input[2]));
            return new InputDTO(GameStatus.Playing, coords);
        }

        private string GetValidatedInput() //TODO: larger grids are broken (use regex)
        {
            Console.Write(InputPrompt);
            var input = Console.ReadLine()?.Trim();
            
            while (true)
            {
                if (input != null && input.Length >= 3 && input.Length <= 5)
                {
                    if (input.Length <= 5 && input[0] == 'f' && Char.IsDigit(input[2]) && Char.IsDigit(input[4]))
                    {
                        return input;
                    }

                    if (input.Length <= 3 && Char.IsDigit(input[0]) && Char.IsDigit(input[2]))
                    {
                        return input;
                    }
                }

                Console.Write(ErrorPrompt);
                input = Console.ReadLine()?.Trim();
            }
        }
    }
}