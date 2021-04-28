using MinesweeperService.Enums;
using MinesweeperService.Values;

namespace MinesweeperController.GameBehaviour.DTOs
{
    public sealed record InputDTO(GameStatus GameStatus, Coords SelectedTile);
}