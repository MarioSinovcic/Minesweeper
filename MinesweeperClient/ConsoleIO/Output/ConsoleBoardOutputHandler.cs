﻿using System;
using System.Collections.Generic;
using MinesweeperService.Enums;
using MinesweeperService.Values;

namespace MinesweeperClient.ConsoleIO.Output
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
                Environment.Exit(0); 
            }
        }

        private void DisplayGrid(Grid grid)
        {
            DisplayColNumbers(grid.Width);

            for (var i = 0; i < grid.Height; i++)
            {
                Console.Write(i > 9 ? $" {i} " : $"  {i} ");

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
                divider += i > 9 ? $"   {i} " : $"   {i}  ";
            }

            Console.WriteLine(divider);
        }

        private void DisplayTile(Grid grid, Coords coords)
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
                    HandleColouredTiles(grid.GetNeighbouringMinesAt(coords));
                }
                else
                {
                    Console.Write($"{VerticalSeparator}  {HiddenTile}  ");
                }
            }
        }

        private void HandleColouredTiles(int neighbours)
        {
            Console.Write($"{VerticalSeparator}  ");

            if (neighbours > DisplayTileTypes.Count) 
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