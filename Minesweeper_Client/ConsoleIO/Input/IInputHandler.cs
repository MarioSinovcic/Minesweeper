using Minesweeper_Controller.GameBehaviour.DTOs;

namespace Minesweeper_Client.ConsoleIO.Input
{
    public interface IInputHandler
    {
        public InputDTO GetTurnInput();
    }
}