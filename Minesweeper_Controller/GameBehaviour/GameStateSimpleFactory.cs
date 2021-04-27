using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;
using Minesweeper_Service.Values.Interfaces;

namespace Minesweeper_Controller.GameBehaviour
{
    internal static class GameStateSimpleFactory
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