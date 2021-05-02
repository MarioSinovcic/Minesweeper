using MinesweeperService.Enums;

namespace MinesweeperService.Values.Interfaces 
{
    public interface IGrid
    {
        public int Width { get; }
        public int Height { get; }
        public TileStatus GetTileStatusAt(Coords coords);

        public TileType GetTileTypeAt(Coords coords);

        public Tile GetInvertTileAt(Coords coords); 
        
        public int GetNeighbouringMines(Coords coords);
        
        public void ReplaceTile(Coords coords, Tile updatedTile);

    }
}