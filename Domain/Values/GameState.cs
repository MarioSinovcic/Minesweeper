using Domain.Enums;

namespace Domain.Values
{
    public sealed record GameState (GameStatus GameStatus, Grid Grid, Coords Coords); 
    //consider value vs entity type vs aggregate 
    //TODO: handle GameStateDTO
    
}