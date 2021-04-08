namespace Application.DTOs
{
    public class GridInputDto //should be a record
    {
        public string MineTileChar { get; set; } //should be inits
        public string EmptyTileChar { get; set; }
        public string[,] InitialGrid { get; set; }
    }
}