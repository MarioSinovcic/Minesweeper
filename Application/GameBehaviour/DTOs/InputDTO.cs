using Domain.Enums;
using Domain.Values;

namespace Application.GameBehaviour.DTOs
{
    public sealed record InputDTO(GameStatus GameStatus, Coords SelectedTile);
}