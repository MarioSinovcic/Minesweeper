using Domain.Enums;
using Domain.Interfaces;

namespace Domain.Values
{
    public sealed record GameState (GameStatus GameStatus, IGrid Grid, Coords Coords); 
}