using System;
using Domain.Enums;

namespace Domain.Entities
{
    public record Grid
    {
        public Grid(Tile[,] tiles) //remove later
        {
            Height = tiles.GetLength(0);
            Width = tiles.GetLength(1);
            Tiles = tiles;
        }

        public int Width { get; }
        public int Height { get; } 
        public Tile[,] Tiles { get; init; }

        public int GetNeighbouringMines(Coords coords)
        {
            var y = coords.Y;
            var x = coords.X;
            
            if (y >= Height || x >= Width || y < 0 || x < 0)
            {
                throw new IndexOutOfRangeException("X and Y co-ordinates must be within the grid's dimensions.");
            }
            
            var mines = 0;
            if (Tiles[y,x].Type.Equals(TileType.Mine))
            {
                mines--;
            }
            
            for (var i = -1 ; i < 2; i++)
            {
                for (var j = -1; j < 2; j++)
                {
                    var xCoord = (x + i + Width) % Width;
                    var yCoord = (y + j + Height) % Height;
                    if (Tiles[yCoord,xCoord].Type.Equals(TileType.Mine))
                    {
                        mines++;
                    }
                }
            }
            return mines;
        }
    }
}