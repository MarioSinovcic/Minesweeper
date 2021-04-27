using Minesweeper_Service.Enums;

namespace Minesweeper_Service.Values.Interfaces //take it out
{
    public interface IGrid
    {
        public int Width { get; }
        public int Height { get; }
        public TileStatus GetTileStatusAt(Coords coords);

        public TileType GetTileTypeAt(Coords coords);

        public Tile GetInvertTileStatus(Coords coords); 
        
        public int GetNeighbouringMines(Coords coords);
        
        public void ReplaceTile(Coords coords, Tile updatedTile);

    }
}