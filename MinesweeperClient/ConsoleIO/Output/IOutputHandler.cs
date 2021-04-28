using MinesweeperService.Values;

namespace MinesweeperClient.ConsoleIO.Output
{
    public interface IOutputHandler
    {
        public void DisplayGameState(GameState gameState);
    }
}