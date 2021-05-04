using MinesweeperService.Enums;
using MinesweeperService.Values;
using MinesweeperService.Values.Interfaces;

namespace MinesweeperController.GameBehaviour
{
    internal static class SimpleGameStateFactory
    {
        internal static GameState CreateGameState(GameStatus gameStatus)
        {
                return new GameState(GameStatus.Error, null, null);
        }
        
        internal static GameState CreateGameState(IGrid grid)
        {
            return new GameState(GameStatus.FirstTurn, grid, null);
        }
        
        internal static GameState CreateGameState(GameStatus gameStatus, IGrid grid, Coords coords)
        {
            return new GameState(gameStatus, grid, coords);
        }
    }
}