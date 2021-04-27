using System;
using Minesweeper_Client.ConsoleIO.Input;
using Minesweeper_Controller.GameBehaviour.DTOs;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;

namespace Minesweeper_Client.ConsoleIO
{
    public class ConsoleInputHandler : IInputHandler
    {
        private const string InputPrompt = "Enter your co-ordinates: ";
        private const string ErrorPrompt = "Looks like that input wasn't valid, please try again: ";

        public InputDTO GetTurnInput()
        {
            Console.Write(InputPrompt);
            return GetValidatedInput();
        }

        private InputDTO GetValidatedInput() 
        {
            var input = Console.ReadLine()?.Trim();
            var strings = input?.Split(" ");

            while (strings != null)
            {
                int xCoord;
                int yCoord;
                if (strings[0] == "f" && strings.Length == 3)
                {
                    Console.WriteLine(strings[1] + strings[2]);
                    if (int.TryParse(strings[1], out xCoord) && int.TryParse(strings[2], out yCoord))
                    {
                        return new InputDTO(GameStatus.SetFlag, new Coords(xCoord, yCoord));
                    }
                }
                else if(strings.Length == 2)
                {
                    if (int.TryParse(strings[0], out xCoord) && int.TryParse(strings[1], out yCoord))
                    {
                        return new InputDTO(GameStatus.Playing, new Coords(xCoord, yCoord));
                    }
                }
                Console.Write(ErrorPrompt);
                strings = Console.ReadLine()?.Trim().Split(" ");
            }
            
            return new InputDTO(GameStatus.Error, null);
        }
    }
}