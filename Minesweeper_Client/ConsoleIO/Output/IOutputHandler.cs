using Minesweeper_Service.Values;

namespace Minesweeper_Client.ConsoleIO.Output
{
    public interface IOutputHandler
    {
        public void DisplayGameState(GameState gameState);
    }
}