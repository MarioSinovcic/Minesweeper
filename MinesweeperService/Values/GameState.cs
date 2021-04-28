using MinesweeperService.Enums;
using MinesweeperService.Values.Interfaces;

namespace MinesweeperService.Values
{
    public sealed record GameState (GameStatus GameStatus, IGrid Grid, Coords Coords); 

}