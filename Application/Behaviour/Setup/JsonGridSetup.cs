using System;
using System.IO;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Newtonsoft.Json;

namespace Application.Behaviour.Setup
{
    public class JsonGridSetup : IGridSetup
    {
        private static string _pathname;

        public JsonGridSetup(string pathname)
        {
            _pathname = pathname;
        }
        
        public Grid CreateGrid() //TODO: correct use of static
        {
            ValidatePath(_pathname);
            
            using var jsonFile = new StreamReader(_pathname);
            var jsonInput = JsonConvert.DeserializeObject<JSONGridInputDTO>(jsonFile.ReadToEnd());

            var gridWidth = jsonInput.InitialGrid.GetLength(1);
            var gridHeight = jsonInput.InitialGrid.GetLength(0);
            var tiles = new Tile[gridHeight,gridWidth];
            
            for (var i = 0; i < gridWidth; i++)
            {
                for (var j = 0; j < gridHeight; j++)
                {
                    var tileType = GetTileType(jsonInput.InitialGrid[j,i], jsonInput.MineTileChar);
                    tiles[j, i] = new Tile{Type = tileType};
                }
            }
            return new Grid(tiles);
        }

        private static void ValidatePath(string pathname)
        {
            if (!File.Exists(pathname)) throw new IOException("Invalid path for grid creation.");
            try
            {
                using var jsonFile = new StreamReader(pathname);
                JsonConvert.DeserializeObject<JSONGridInputDTO>(jsonFile.ReadToEnd());
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