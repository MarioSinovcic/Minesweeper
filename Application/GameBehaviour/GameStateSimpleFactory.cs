using Domain.Enums;
using Domain.Values;
using Domain.Values.Interfaces;

namespace Application.GameBehaviour
{
    internal static class GameStateSimpleFactory
    {
        public static GameState CreateGameState(GameStatus gameStatus)
        {
                return new GameState(GameStatus.Error, null, null);
        }
        
        public static GameState CreateGameState(IGrid grid)
        {
            return new GameState(GameStatus.FirstTurn, grid, null);
        }
        
        public static GameState CreateGameState(GameStatus gameStatus, IGrid grid, Coords coords)
        {
            return new GameState(gameStatus, grid, coords);
        }
    }
}