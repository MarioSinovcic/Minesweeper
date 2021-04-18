using Application.GameBehaviour.DTOs;
using Domain.Values;

namespace Frontend.Interfaces
{
    public interface IInputHandler
    {
        public InputDTO GetTurnInput();
    }
}