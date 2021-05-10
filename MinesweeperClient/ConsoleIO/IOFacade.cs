using MinesweeperController.GameBehaviour.DTOs;
using MinesweeperService.Values;
using MinesweeperClient.ConsoleIO.Input;
using MinesweeperClient.ConsoleIO.Output;

namespace MinesweeperClient.ConsoleIO
{
    internal class IOFacade : IInputHandler, IOutputHandler 
    {
        private readonly IInputHandler _inputHandler;
        private readonly IOutputHandler _outputHandler;
        
        public IOFacade(IInputHandler inputHandler, IOutputHandler outputHandler)
        {
            _inputHandler = inputHandler;
            _outputHandler = outputHandler;
        }

        public InputDTO GetTurnInput()
        {
            return _inputHandler.GetTurnInput();
        }

        public void DisplayGameState(GameState gameState)
        {
            _outputHandler.DisplayGameState(gameState);
        }
    }
}