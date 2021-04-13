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
            var grid = new RandomGridSetup(10, 10, 5).CreateGrid();
            return new GameState(GameStatus.Playing, grid, null); //TODO: create factory NewGameState(grid)
        }

        public GameState HandleMove(Coords inputDto, GameState gameState)
        {
            return Minesweeper.PerformMove(new Move {Grid = gameState.Grid, Coords = inputDto});
        }
    }
}