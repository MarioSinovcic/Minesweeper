namespace Domain
{
    public sealed class Grid
    {
        public Grid(Tile[,] tiles)
        {
            Tiles = tiles;
        }

        public int Width { get; }
        public int Height { get; }
        public Tile[,] Tiles { get; }
    }
}