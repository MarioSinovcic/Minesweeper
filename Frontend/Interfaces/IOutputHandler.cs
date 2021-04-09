using Domain.DTOs;

namespace Frontend.Interfaces
{
    public interface IOutputHandler
    {
        public void DisplayGameState(GameStateDTO gameState);
    }
}