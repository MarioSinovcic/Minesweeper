namespace Application.DTOs
{
    public sealed record JSONGridInputDTO 
    {
        public string MineTileChar { get; init; }
        public string EmptyTileChar { get; init; }
        public string[,] InitialGrid { get; init; }
    }
}