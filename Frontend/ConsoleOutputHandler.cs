using System;
using Domain.Enums;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleOutputHandler : IOutputHandler
    {
        private const string HiddenTile = " ";
        private const string MineTile = "X";
        private const string VerticalSeparator = "|";

        public void DisplayGameState(GameState gameState)
        {
            Console.Clear();
            var grid = gameState.Grid;
            DisplayGrid(grid);
            
            switch (gameState.GameStatus)
            {
                case GameStatus.Playing:
                    break;
                case GameStatus.Win:
                    Console.WriteLine("YAY");
                    System.Environment.Exit(0);
                    break;
                case GameStatus.Loss:
                    Console.WriteLine("Boo");
                    System.Environment.Exit(0);
                    break;
                case GameStatus.Error:
                    Console.WriteLine("Error");
                    break;
                default:
                    DisplayGrid(grid);
                    break;
            }
        }

        private void DisplayGrid(Grid grid)
        {
            DisplayColNumbers(grid.Width);

            for (var i = 0; i < grid.Height; i++)
            {
                Console.Write($" {i}  ");
                for (var j = 0; j < grid.Width; j++)
                {
                    DisplayTile(grid, new Coords(j,i));
                }

                Console.WriteLine($"{VerticalSeparator}");
            }
        }

        private void DisplayColNumbers(int width)
        {
            var divider = "       0  ";
            for (var i = 1; i < width; i++)
            {
                divider += $"   {i}  ";
            }

            Console.WriteLine(divider);
        }

        private void DisplayTile(Grid grid, Coords coords)
        {
            if (grid.GetTileTypeAt(coords) == TileType.Mine )
            {
                Console.Write(grid.GetTileStatusAt(coords) == TileStatus.Shown
                    ? $"{VerticalSeparator}  {MineTile}  "
                    : $"{VerticalSeparator}  {HiddenTile}  ");
            }
            else
            {
                Console.Write(grid.GetTileStatusAt(coords) == TileStatus.Shown
                    ? $"{VerticalSeparator}  {grid.GetNeighbouringMines(coords)}  "
                    : $"{VerticalSeparator}  {HiddenTile}  ");
            }
        }
    }
}