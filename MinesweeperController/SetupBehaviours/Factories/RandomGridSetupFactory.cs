using System;
using System.Dynamic;
using MinesweeperController.SetupBehaviours.Interfaces;
using MinesweeperService.Values;
using MinesweeperService.Values.Interfaces;

namespace MinesweeperController.SetupBehaviours.Factories
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
            SetupValidator.ValidateParameters(_width,_height, _difficulty);
        }

        public IGrid CreateGrid() 
        {
            var tiles = new Tile[_height,_width];
            
            for (var i = 0; i < _width; i++)
            {
                for (var j = 0; j < _height; j++)
                {
                    var tileType = Tile.GetRandomTileType(_difficulty);
                    tiles[j, i] = new Tile(tileType);
                }
            }
            return new Grid(tiles);
        }
    }
}