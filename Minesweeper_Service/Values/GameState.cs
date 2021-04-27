using Minesweeper_Service.Enums;
using Minesweeper_Service.Values.Interfaces;

namespace Minesweeper_Service.Values
{
    public sealed record GameState (GameStatus GameStatus, IGrid Grid, Coords Coords); 

}