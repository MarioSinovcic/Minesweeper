using MinesweeperController.GameBehaviour.DTOs;

namespace MinesweeperClient.ConsoleIO.Input
{
    public interface IInputHandler
    {
        public InputDTO GetTurnInput();
    }
}