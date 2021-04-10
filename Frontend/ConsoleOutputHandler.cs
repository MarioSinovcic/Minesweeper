﻿using System;
using Domain.DTOs;
using Domain.Entities;
using Domain.Enums;
using Frontend.Interfaces;

namespace Frontend
{
    public class ConsoleOutputHandler : IOutputHandler
    {
        private const string HiddenTile = " ";
        private const string VerticalSeparator = "|";
        private const string GenerationDivider = "+";
        
        public void DisplayGameState(GameStateDTO gameState)
        {
            Console.Clear();
            var grid = gameState.Grid;
            
            DisplayGenerationDivider(grid.Width);
            
            for (var i = 0; i < grid.Height; i++)
            {
                for (var j = 0; j < grid.Width; j++)
                {
                    DisplayCell(grid, new Coords{X = j, Y = i});
                }
                Console.WriteLine($"{VerticalSeparator}");
            }
        }

        private void DisplayGenerationDivider(int width)
        {
            var divider = GenerationDivider;
            for (var i = 0; i < width*2; i++)
            {
                divider += $"  {GenerationDivider}";
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