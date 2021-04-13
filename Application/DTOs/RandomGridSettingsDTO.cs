namespace Application.DTOs
{
    public sealed record RandomGridSettingsDTO
    {
        public int Width { get; init; }
        public int Height { get; init; }
        public int Difficulty { get; init; }
    }
}