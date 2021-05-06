using MinesweeperService.Enums;

namespace MinesweeperService.Values
{
    public sealed record GameState (GameStatus GameStatus, Grid Grid, Coords Coords); 

}