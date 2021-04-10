using Application.Behaviour.Setup;
using Domain.DTOs;
using Domain.Enums;
using Frontend;

namespace Application
{
    class Program
    {
        //dependencies injection stuff
        
        private static void Main()
        {
            //var grid = new JsonGridSetup("/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/Minesweeper Tests/Grid Fakes/FourSquareMines.json").CreateGrid();
            var grid = new RandomGridSetup(10, 10, 15).CreateGrid();
            
            for (var i = 0; i < grid.Width; i++)
            {
                for (var j = 0; j < grid.Height; j++)
                {
                    if (grid.Tiles[j,i].Type == TileType.Empty)
                    {
                        grid.Tiles[j,i] = grid.Tiles[j, i].ShowTile();
                    }
                }
            }
            var gameState = new GameStateDTO {Grid = grid, Coords = null, GameStatus = GameStatus.Playing};

            var outputHandler = new ConsoleOutputHandler();
            
            //controller.setupGame() <- builder??
        
            //while (true)
            //{
            //var InputDTO = inputHandler.getInput();
            //var GameStateDTO = controller.handleMove(inputDTO);
            outputHandler.DisplayGameState(gameState);
            //}
        }

    }
}