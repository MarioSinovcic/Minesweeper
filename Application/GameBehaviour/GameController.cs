using Application.SetupBehaviours.Factories;
using Domain;
using Domain.Enums;
using Domain.Values;

namespace Application.GameBehaviour
{
    public class GameController //TODO: add factory pattern, error handling, json deserializing ??
    {
        public GameState SetupGame()
        {
            var grid = new RandomGridSetupFromJsonFactory("SetupBehaviours/RandomGridSettings.json").CreateGrid(); //TODO: should be taken in from settings
            return new GameState(GameStatus.Playing, grid, null); //TODO: remove null if poss
        }

        public GameState HandleMove(Coords inputDto, GameState gameState) //TODO: create inputDTO
        {
            return Minesweeper.PerformMove(new Move(gameState.Grid, inputDto));
        }
    }
}