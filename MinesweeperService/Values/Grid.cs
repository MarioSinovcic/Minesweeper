using System;
using MinesweeperService.Enums;

namespace MinesweeperService.Values
{
    public record Grid 
    {
        private Tile[,] _tiles;
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
        
        public Grid WithRevealedTileAt(Coords coords)
        {
            var tileType = GetTileTypeAt(coords);
            var tile = new Tile(tileType, TileStatus.Shown);

            return WithNewTileAt(coords, tile);
        }
        
        public Grid WithNewTileAt(Coords coords, Tile updatedTile)
        {
            var (x, y) = coords;
            var tiles = (Tile[,]) _tiles.Clone();

            tiles[y, x] = updatedTile; 
            
            return this with {_tiles = tiles};
        }

        public int GetNeighbouringMinesAt(Coords coords)
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