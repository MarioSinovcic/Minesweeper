using System;
using Application.Application.Interfaces;
using Domain;
using Domain.Enums;

namespace Application.Application.Behaviour.Setup
{
    public class RandomGridSetup : IGridSetup
    {
        public RandomGridSetup(int width, int height, int difficulty) //take in this data via a config system
        {
            Width = width;
            Height = height;
            Difficulty = difficulty; 
        }

        public virtual int Width { get; private set; } = 8;
        public virtual int Height { get; private set; } = 8;
        public virtual int Difficulty { get; private set; } = 10;

        public Grid CreateGrid() //TODO: correct use of static
        {
            ValidateParameters();
            
            var tiles = new Tile[Height,Width];
            
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    var tileType = GetRandomTileType();
                    tiles[j, i] = new Tile(tileType);
                }
            }
            return new Grid(tiles);
        }

        private void ValidateParameters()
        {
            if (Width <= 1 || Height <= 1 || Difficulty <= 1)
            {
                throw new ApplicationException("Invalid input parameters for random generation.");
            }
        }

        private TileType GetRandomTileType()
        {
            var randomNum = new Random(); //approximates to: 1 out of every {Difficulty} tile will be a mine
            return randomNum.Next(Difficulty) < 1 ? TileType.Mine : TileType.Empty;
        }
    }
}