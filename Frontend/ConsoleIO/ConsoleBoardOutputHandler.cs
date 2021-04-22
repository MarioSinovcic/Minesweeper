using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Values;
using Domain.Values.Interfaces;
using Frontend.Interfaces;

namespace Frontend.ConsoleIO
{
    public class ConsoleBoardOutputHandler : IOutputHandler
    {
        private const string HiddenTile = " ";
        private const string MineTile = "¤";
        private const string FlagTile = ">";
        private const string VerticalSeparator = "|";
        
        private static readonly IList<DisplayTile> DisplayTileTypes = new List<DisplayTile>
        {
            new(0, ConsoleColor.Gray, '×'),
            new(1, ConsoleColor.DarkCyan, '1'),
            new(2, ConsoleColor.Yellow, '2'),
            new(3, ConsoleColor.Red, '3'),
        };

        private static readonly Dictionary<GameStatus, string> GameStateMessages = new()
        {
            {GameStatus.Win, "Well done you won!"},
            {GameStatus.Playing, ""},
            {GameStatus.FirstTurn, "Good luck!"},
            {GameStatus.Loss, "Oh no you lost, try again!"},
            {GameStatus.Error, "Looks like something went wrong with the game, please try again."},
        };

        public void DisplayGameState(GameState gameState)
        {
            var (gameStatus, grid, _) = gameState;

            Console.Clear();
            DisplayGrid(grid);
            Console.Write(GameStateMessages[gameStatus] + "\n");
            
            if (gameStatus == GameStatus.Win || gameStatus == GameStatus.Loss)
            {
                Environment.Exit(0); //TODO: code smell
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

        private void DisplayTile(IGrid grid, Coords coords) //TODO: this is pretty horrible
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
                if (grid.GetTileStatusAt(coords) == TileStatus.Shown)
                {
                    HandleColouredTiles(grid.GetNeighbouringMines(coords));
                }
                else
                {
                    Console.Write($"{VerticalSeparator}  {HiddenTile}  ");
                }
            }
        }

        private void HandleColouredTiles(int neighbours) //TODO: this is also gross
        {
            Console.Write($"{VerticalSeparator}  ");

            if (neighbours > DisplayTileTypes.Count()) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{neighbours}  ");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = DisplayTileTypes[neighbours].Color;
                Console.Write($"{DisplayTileTypes[neighbours].DisplayChar}  ");
                Console.ResetColor();

            }
        }
    }
}