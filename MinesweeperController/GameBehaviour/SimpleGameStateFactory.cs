using MinesweeperService.Enums;
using MinesweeperService.Values;

namespace MinesweeperController.GameBehaviour
{
    internal static class SimpleGameStateFactory
    {
        internal static GameState CreateGameState(GameStatus gameStatus)
        {
                return new GameState(GameStatus.Error, null, null);
        }
        
        internal static GameState CreateGameState(Grid grid)
        {
            return new GameState(GameStatus.FirstTurn, grid, null);
        }
        
        internal static GameState CreateGameState(GameStatus gameStatus, Grid grid, Coords coords)
        {
            return new GameState(gameStatus, grid, coords);
        }
    }
}