namespace Application.DTOs
{
    public record GridInputDto 
    {
        public string MineTileChar { get; init; }
        public string EmptyTileChar { get; init; }
        public string[,] InitialGrid { get; init; }
    }
}