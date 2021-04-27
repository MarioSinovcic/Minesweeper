using Minesweeper_Controller.GameBehaviour.DTOs;
using Minesweeper_Service.Values;
using Minesweeper_Client.ConsoleIO.Output;
using Minesweeper_Client.Interfaces;

namespace Minesweeper_Client.ConsoleIO
{
    public class IOFacade : IInputHandler, IOutputHandler 
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