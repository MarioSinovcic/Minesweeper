using Application.GameBehaviour.DTOs;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend
{
    public class IOFacade : IInputHandler, IOutputHandler //Questionable use of Interface Segregation Principle
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