using System;
using System.IO;
using Minesweeper_Controller.SetupBehaviours.DTOs;
using Minesweeper_Controller.SetupBehaviours.Interfaces;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;
using Minesweeper_Service.Values.Interfaces;
using Newtonsoft.Json;

namespace Minesweeper_Controller.SetupBehaviours.Factories
{
    public class JsonGridSetupFactory : IGridSetupFactory
    {
        private static string _pathname;

        public JsonGridSetupFactory(string pathname)
        {
            _pathname = pathname;
        }
        
        public IGrid CreateGrid()
        {
            ValidatePath(_pathname);
            
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

        private static void ValidatePath(string pathname)
        {
            try
            {
                using var jsonFile = new StreamReader(pathname);
                JsonConvert.DeserializeObject<JsonGridInputDTO>(jsonFile.ReadToEnd());
            }
            catch (Exception e)
            {
                throw new IOException("Invalid path for grid creation.", e);
            }
        }

        private static TileType GetTileType(string value, string mineTileChar)
        {
            return string.Equals(value, mineTileChar) ? TileType.Mine : TileType.Empty;
        }
    }
}