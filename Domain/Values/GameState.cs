using Domain.Enums;
using Domain.Values.Interfaces;

namespace Domain.Values
{
    public sealed record GameState (GameStatus GameStatus, IGrid Grid, Coords Coords); //make method like Redux (state, action) -> next state, too keep things immtuable 
}