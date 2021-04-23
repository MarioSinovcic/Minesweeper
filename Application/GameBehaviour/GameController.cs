using System;
using Application.GameBehaviour.DTOs;
using Application.SetupBehaviours.Factories;
using Domain;
using Domain.Enums;
using Domain.Values;

namespace Application.GameBehaviour
{
    public class GameController //TODO: error handling, json deserializing ??
    {
        public GameState SetupRandomGameFromJson(string pathname)
        {
            try
            {
                var grid = new RandomGridSetupFromJsonFactory(pathname).CreateGrid(); 
                return GameStateSimpleFactory.CreateGameState(grid);
            }
            catch (Exception)
            {
                return GameStateSimpleFactory.CreateGameState(GameStatus.Error);
            }
        }
        
        public GameState SetupRandomGrid(int width, int height, int difficulty)
        {
            try
            {
                var grid = new RandomGridSetupFactory(width,height,difficulty).CreateGrid(); 
                return GameStateSimpleFactory.CreateGameState(grid);
            }
            catch (Exception)
            {
                return GameStateSimpleFactory.CreateGameState(GameStatus.Error);
            }
        }

        public GameState HandleMove(InputDTO inputDto, GameState gameState) 
        {
            var (gameStatus, selectedTile) = inputDto;
            
            if (gameStatus == GameStatus.Error)
            {
                return GameStateSimpleFactory.CreateGameState(GameStatus.Error);
            }

            var state = GameStateSimpleFactory.CreateGameState(gameStatus, gameState.Grid, selectedTile);
            var minesweeper = new Minesweeper(state);
            
            minesweeper.PerformMove();
            return minesweeper.GetGameState();
        }
    }
}