using System;
using Minesweeper_Controller.GameBehaviour.DTOs;
using Minesweeper_Controller.SetupBehaviours.Factories;
using Minesweeper_Service;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;

namespace Minesweeper_Controller.GameBehaviour
{
    public class GameController 
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