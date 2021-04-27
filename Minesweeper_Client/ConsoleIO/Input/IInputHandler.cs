using Minesweeper_Controller.GameBehaviour.DTOs;

namespace Minesweeper_Client.Interfaces
{
    public interface IInputHandler
    {
        public InputDTO GetTurnInput();
    }
}