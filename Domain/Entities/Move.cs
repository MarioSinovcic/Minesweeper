namespace Domain.Entities
{
    public sealed record Move
    {
        public Coords Coords { get; init; }
        public Grid Grid { get; init; }
    }
}