using System;
using System.IO;
using Application.SetupBehaviours.DTOs;
using Application.SetupBehaviours.Interfaces;
using Domain.Enums;
using Domain.Values;
using Domain.Values.Interfaces;
using Newtonsoft.Json;

namespace Application.SetupBehaviours.Factories
{
    public sealed class RandomGridSetupFromJsonFactory : IGridSetupFactory
    {
        private readonly int _width;
        private readonly int _height;
        private readonly int _difficulty;

        public RandomGridSetupFromJsonFactory(string settingsFilePath)
        {
            ValidatePath(settingsFilePath);

            using var jsonFile = new StreamReader(settingsFilePath);
            var (width, height, difficulty) =
                JsonConvert.DeserializeObject<RandomGridSettingsDTO>(jsonFile.ReadToEnd());

            _width = width;
            _height = height;
            _difficulty = difficulty;
        }

        public IGrid CreateGrid()
        {
            ValidateParameters();
            var tiles = new Tile[_height, _width];

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

        private TileType GetRandomTileType()
        {
            var randomNum = new Random(); //approximates to: 1 out of every {Difficulty} tile will be a mine
            return randomNum.Next(_difficulty) < 1 ? TileType.Mine : TileType.Empty;
        }

        private void ValidateParameters()
        {
            if (_width <= 1 || _height <= 1 || _difficulty <= 1)
            {
                throw new ApplicationException("Invalid input parameters for random generation.");
            }
        }

        private static void ValidatePath(string pathname)
        {
            try
            {
                using var jsonFile = new StreamReader(pathname);
                JsonConvert.DeserializeObject<RandomGridSettingsDTO>(jsonFile.ReadToEnd());
            }
            catch (Exception e)
            {
                throw new IOException("Invalid path for grid creation.", e);
            }
        }
    }
}