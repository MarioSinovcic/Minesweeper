using Domain.Values;

namespace Application.GameBehaviour.DTOs
{
    public sealed record InputDTO
    {
        public Coords SelectedTile { get; init; }
    }
}