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
        public Tile[,] Tiles { get; init; } //TODO: refactor for getting status/type for coords

        public int GetNeighbouringMines(Coords coords)
        {
            var x = coords.X;
            var y = coords.Y;

            if (y >= Height || x >= Width || y < 0 || x < 0)
            {
                throw new IndexOutOfRangeException("X and Y co-ordinates must be within the grid's dimensions.");
            }
            
            var mines = 0;
            if (Tiles[y,x].Type.Equals(TileType.Mine))
            {
                mines--;
            }
            
            for (var xoff = -1 ; xoff < 2; xoff++)
            {
                for (var yoff = -1; yoff < 2; yoff++)
                {
                    var xCoord = x + xoff;
                    var yCoord = y + yoff;
                    if (xCoord <= -1 || xCoord >= Width || yCoord <= -1 || yCoord >= Height) continue;
                    if (Tiles[yCoord, xCoord].Type.Equals(TileType.Mine))
                    {
                        mines++;
                    }
                }
            }
            return mines;
        }
    }
}