using Application.Behaviour.Setup;
using Domain;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Controllers
{
    public class GameController //TODO: add factory pattern, error handling, json deserializing ??
    {
        public GameStateDTO SetupGame()
        {
            var grid = new RandomGridSetup(10, 10, 5).CreateGrid();
            return new GameStateDTO {Grid = grid, Coords = null, GameStatus = GameStatus.Playing};
        }

        public GameStateDTO HandleMove(Coords inputDto, GameStateDTO gameState)
        {
            return Minesweeper.PerformMove(new Move {Grid = gameState.Grid, Coords = inputDto});
        }
    }
}