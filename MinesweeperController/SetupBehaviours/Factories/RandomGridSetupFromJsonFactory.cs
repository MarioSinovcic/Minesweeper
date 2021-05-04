using System;
using System.IO;
using MinesweeperController.SetupBehaviours.DTOs;
using MinesweeperController.SetupBehaviours.Interfaces;
using MinesweeperService.Values;
using MinesweeperService.Values.Interfaces;
using Newtonsoft.Json;

namespace MinesweeperController.SetupBehaviours.Factories
{
    public sealed class RandomGridSetupFromJsonFactory : IGridSetupFactory
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _difficulty;

        public RandomGridSetupFromJsonFactory(string settingsFilePath)
        {
            SetupValidator.ValidatePath(settingsFilePath);

            using var jsonFile = new StreamReader(settingsFilePath);
            var (width, height, difficulty) =
                JsonConvert.DeserializeObject<RandomGridSettingsDTO>(jsonFile.ReadToEnd());

            _width = width;
            _height = height;
            _difficulty = difficulty;
            SetupValidator.ValidateParameters(_width,_height,_difficulty);
        }

        public IGrid CreateGrid()
        {
            
            var tiles = new Tile[_height, _width];
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