using Domain.Enums;
using Domain.Values;

namespace Domain.Interfaces
{
    public interface IGrid
    {
        public int Width { get; }
        public int Height { get; }
        public TileStatus GetTileStatusAt(Coords coords);

        public TileType GetTileTypeAt(Coords coords);

        public Tile GetInvertTileStatus(Coords coords); //TODO: remove if poss
        
        public int GetNeighbouringMines(Coords coords);
        
        public void ReplaceTile(Coords coords, Tile updatedTile);

    }
}