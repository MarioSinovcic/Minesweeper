using System;
using System.IO;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;

namespace Application.Behaviour.Setup
{
    public sealed class RandomGridSetup : IGridSetup
    {
        private int Width { get; } = 8;
        private int Height { get; } = 8;
        private int Difficulty { get; } = 10;
        
        public RandomGridSetup(int width, int height, int difficulty)
        {
            Width = width;
            Height = height;
            Difficulty = difficulty; 
        }

        public RandomGridSetup(string settingsFilePath)
        {
            ValidatePath(settingsFilePath);
            
            using var jsonFile = new StreamReader(settingsFilePath);
            var jsonInput = JsonConvert.DeserializeObject<RandomGridSettingsDTO>(jsonFile.ReadToEnd());
            
            Width = jsonInput.Width;
            Height = jsonInput.Height;
            Difficulty = jsonInput.Difficulty; 
        }

        public Grid CreateGrid() 
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

        private TileType GetRandomTileType() //TODO: stub random, provided by the constructor
        {
            var randomNum = new Random(); //approximates to: 1 out of every {Difficulty} tile will be a mine
            return randomNum.Next(Difficulty) < 1 ? TileType.Mine : TileType.Empty;
        }
        
        private static void ValidatePath(string pathname) //DRY validation: make json diser.
        {
            if (!File.Exists(pathname)) throw new IOException("Invalid path for grid creation.");
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