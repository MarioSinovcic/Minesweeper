using System;
using Domain.Enums;
using Domain.Values.Interfaces;

namespace Domain.Values
{
    public record Grid : IGrid
    {
        private readonly Tile[,] _tiles;
        public int Width { get; }
        public int Height { get; }
        
        public Grid(Tile[,] tiles)
        {
            Height = tiles.GetLength(0);
            Width = tiles.GetLength(1);
            _tiles = tiles;
        }

        public TileStatus GetTileStatusAt(Coords coords)
        {
            var (x, y) = coords;
            return _tiles[y, x].Status;
        }
        
        public TileType GetTileTypeAt(Coords coords)
        {
            var (x, y) = coords;
            return _tiles[y, x].Type;
        }
        
        public Tile GetInvertTileStatus(Coords coords) //TODO: remove if poss
        {
            var (x, y) = coords;
            return _tiles[y, x].ShowTile();
        }
        
        public void ReplaceTile(Coords coords, Tile updatedTile) //TODO: remove to make immutable
        {
            var (x, y) = coords;
            _tiles[y, x] = updatedTile;
        }

        public int GetNeighbouringMines(Coords coords)
        {
            var x = coords.X;
            var y = coords.Y;

            if (y >= Height || x >= Width || y < 0 || x < 0)
            {
                throw new ArgumentException("X and Y co-ordinates must be within the grid's dimensions.");
            }
            
            var mines = 0;
            if (_tiles[y,x].Type == TileType.Mine)
            {
                mines--;
            }
            
            for (var xOff = -1 ; xOff < 2; xOff++)
            {
                for (var yOff = -1; yOff < 2; yOff++)
                {
                    var xCoord = x + xOff;
                    var yCoord = y + yOff;
                    if (xCoord <= -1 || xCoord >= Width || yCoord <= -1 || yCoord >= Height) continue;
                    if (_tiles[yCoord, xCoord].Type == TileType.Mine)
                    {
                        mines++;
                    }
                }
            }
            return mines;
        }
    }
}