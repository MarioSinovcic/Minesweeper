using System;
using System.IO;
using MinesweeperController.SetupBehaviours.DTOs;
using MinesweeperController.SetupBehaviours.Interfaces;
using MinesweeperService.Enums;
using MinesweeperService.Values;
using MinesweeperService.Values.Interfaces;
using Newtonsoft.Json;

namespace MinesweeperController.SetupBehaviours.Factories
{
    public class JsonGridSetupFactory : IGridSetupFactory
    {
        private static string _pathname;

        public JsonGridSetupFactory(string pathname)
        {
            _pathname = pathname;
            SetupValidator.ValidatePath(_pathname);
        }
        
        public IGrid CreateGrid()
        {
            using var jsonFile = new StreamReader(_pathname);
            var jsonInput = JsonConvert.DeserializeObject<JsonGridInputDTO>(jsonFile.ReadToEnd());

            var gridWidth = jsonInput.InitialGrid.GetLength(1);
            var gridHeight = jsonInput.InitialGrid.GetLength(0);
            var tiles = new Tile[gridHeight,gridWidth];
            
            for (var i = 0; i < gridWidth; i++)
            {
                for (var j = 0; j < gridHeight; j++)
                {
                    var tileType = GetTileType(jsonInput.InitialGrid[j,i], jsonInput.MineTileChar);
                    tiles[j, i] = new Tile(tileType);
                }
            }
            return new Grid(tiles);
        }

        private static TileType GetTileType(string value, string mineTileChar)
        {
            return string.Equals(value, mineTileChar) ? TileType.Mine : TileType.Empty;
        }
    }
}