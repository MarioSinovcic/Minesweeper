using Application.GameBehaviour.DTOs;
using Application.SetupBehaviours.Factories;
using Domain;
using Domain.Enums;
using Domain.Values;

namespace Application.GameBehaviour
{
    public class GameController //TODO: add factory pattern, error handling, json deserializing ??
    {
        public GameState SetupRandomGameFromJson(string pathname)
        {
            var grid = new RandomGridSetupFromJsonFactory(pathname).CreateGrid();
            return new GameState(GameStatus.Playing, grid, null); //TODO: remove null if poss (game state factory)
        }

        public GameState HandleMove(InputDTO inputDto, GameState gameState) //TODO: create inputDTO
        {
            if (inputDto.GameStatus == GameStatus.Error)
            {
                return new GameState(GameStatus.Error, null, null);
            }
            
            var state = new GameState(inputDto.GameStatus, gameState.Grid, inputDto.SelectedTile);
            var minesweeper = new Minesweeper(state);
            minesweeper.PerformMove();
            return minesweeper.GetGameState();
        }
    }
}