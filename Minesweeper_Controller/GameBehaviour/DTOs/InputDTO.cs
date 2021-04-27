using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;

namespace Minesweeper_Controller.GameBehaviour.DTOs
{
    public sealed record InputDTO(GameStatus GameStatus, Coords SelectedTile);
}