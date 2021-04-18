using System;
using System.Collections.Generic;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Values;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleOutputHandler : IOutputHandler
    {
        private const string HiddenTile = " ";
        private const string MineTile = "X";
        private const string FlagTile = ">";
        private const string VerticalSeparator = "|";

        private static readonly Dictionary<GameStatus, string> GameStateMessages = new()
        {
            {GameStatus.Win, "Well done you won!"},
            {GameStatus.Playing, ""},
            {GameStatus.Loss, "Oh no you lost, try again!"},
            {GameStatus.Error, "Looks like something went wrong with the game, please restart."},
        };

        public void DisplayGameState(GameState gameState)
        {
            Console.Clear();
            
            var (gameStatus, grid, _) = gameState;
            DisplayGrid(grid);
            Console.Write(GameStateMessages[gameStatus]);
            
            if (gameStatus == GameStatus.Win || gameStatus == GameStatus.Loss)
            {
                Environment.Exit(0);
            }
        }

        private void DisplayGrid(IGrid grid)
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

        private void DisplayTile(IGrid grid, Coords coords) //TODO: this is genuinely horrible
        {
            if(grid.GetTileStatusAt(coords) == TileStatus.Flag)
            {
                Console.Write($"{VerticalSeparator}  {FlagTile}  ");
                return;
            }
            
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