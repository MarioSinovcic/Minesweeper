using Application.GameBehaviour.DTOs;

namespace Frontend.Interfaces
{
    public interface IInputHandler
    {
        public InputDTO GetTurnInput();
    }
}