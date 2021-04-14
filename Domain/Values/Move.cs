using Domain.Interfaces;

namespace Domain.Values
{
    public sealed record Move(IGrid Grid, Coords Coords);
}