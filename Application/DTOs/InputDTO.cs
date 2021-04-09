using Domain.Entities;

namespace Application.DTOs
{
    public sealed record InputDTO
    {
        public Coords SelectedTile { get; init; }
    }
}