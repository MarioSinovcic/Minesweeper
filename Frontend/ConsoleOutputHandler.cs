using System;
using Domain.Enums;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleOutputHandler : IOutputHandler
    {
        private const string HiddenTile = " ";
        private const string VerticalSeparator = "|";

        public void DisplayGameState(GameState gameState)
        {
            Console.Clear();
            var grid = gameState.Grid;

            DisplayGenerationDivider(grid.Width);

            for (var i = 0; i < grid.Height; i++)
            {
                Console.Write($" {i}  ");
                for (var j = 0; j < grid.Width; j++)
                {
                    DisplayCell(grid, new Coords(j,i));
                }

                Console.WriteLine($"{VerticalSeparator}");
            }
        }

        private void DisplayGenerationDivider(int width)
        {
            var divider = "       0  ";
            for (var i = 1; i < width; i++)
            {
                divider += $"   {i}  ";
            }

            Console.WriteLine(divider);
        }

        private void DisplayCell(Grid grid, Coords coords)
        {
            var tile = grid.Tiles[coords.Y, coords.X];

            Console.Write(tile.Status.Equals(TileStatus.Shown)
                ? $"{VerticalSeparator}  {grid.GetNeighbouringMines(coords)}  "
                : $"{VerticalSeparator}  {HiddenTile}  ");
        }
    }
}