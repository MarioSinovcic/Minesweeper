using System;
using MinesweeperController.GameBehaviour.DTOs;
using MinesweeperController.SetupBehaviours.Factories;
using MinesweeperService;
using MinesweeperService.Enums;
using MinesweeperService.Values;

namespace MinesweeperController.GameBehaviour
{
    public class GameController 
    {
        public GameState SetupRandomGameFromJson(string pathname)
        {
            
            
            try
            {
                var grid = new RandomGridSetupFromJsonFactory(pathname).CreateGrid(); 
                return SimpleGameStateFactory.CreateGameState(grid);
            }
            catch (Exception)
            {
                return SimpleGameStateFactory.CreateGameState(GameStatus.Error);
            }
        }
        
        public GameState SetupRandomGrid(int width, int height, int difficulty)
        {
            try
            {
                var grid = new RandomGridSetupFactory(width,height,difficulty).CreateGrid(); 
                return SimpleGameStateFactory.CreateGameState(grid);
            }
            catch (Exception)
            {
                return SimpleGameStateFactory.CreateGameState(GameStatus.Error);
            }
        }

        public GameState HandleMove(InputDTO inputDto, GameState gameState) 
        {
            var (gameStatus, selectedTile) = inputDto;
            
            if (gameStatus == GameStatus.Error)
            {
                return SimpleGameStateFactory.CreateGameState(GameStatus.Error);
            }

            var state = SimpleGameStateFactory.CreateGameState(gameStatus, gameState.Grid, selectedTile);
            var minesweeper = new Minesweeper(state);
            
            minesweeper.PerformMove();
            return minesweeper.GetGameState();
        }
    }
}