using System;
using Application.SetupBehaviours.Interfaces;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Values;

namespace Application.SetupBehaviours.Factories
{
    public sealed class RandomGridSetupFactory : IGridSetupFactory
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _difficulty;
        
        public RandomGridSetupFactory(int width, int height, int difficulty)
        {
            _width = width;
            _height = height;
            _difficulty = difficulty; 
        }

        public IGrid CreateGrid() 
        {
            ValidateParameters();
            
            var tiles = new Tile[_height,_width];
            
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    var tileType = GetRandomTileType();
                    tiles[j, i] = new Tile(tileType);
                }
            }
            return new Grid(tiles);
        }

        private void ValidateParameters()
        {
            if (_width <= 1 || _height <= 1 || _difficulty <= 1)
            {
                throw new ApplicationException("Invalid input parameters for random generation.");
            }
        }

        private TileType GetRandomTileType() //TODO: stub random, provided by the constructor
        {
            var randomNum = new Random(); //approximates to: 1 out of every {Difficulty} tile will be a mine
            return randomNum.Next(_difficulty) < 1 ? TileType.Mine : TileType.Empty;
        }
    }
}