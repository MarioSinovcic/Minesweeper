using Application.SetupBehaviours;
using Domain;
using Domain.Enums;
using Domain.Values;

namespace Application.GameBehaviour
{
    public class GameController //TODO: add factory pattern, error handling, json deserializing ??
    {
        public GameState SetupGame()
        {
            var grid = new RandomGridSetup(3, 3, 100).CreateGrid(); //TODO: should be taken in from settings
            return new GameState(GameStatus.Playing, grid, null); //TODO: create factory NewGameState(grid)
        }

        public GameState HandleMove(Coords inputDto, GameState gameState) //TODO: create inputDTO
        {
            return Minesweeper.PerformMove(new Move(gameState.Grid, inputDto));
        }
    }
}