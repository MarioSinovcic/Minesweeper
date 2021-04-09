using Domain.Entities;
using Domain.Enums;

namespace Domain.DTOs
{
    public record GameStateDTO 
    {
        public GameStatus GameStatus { get; init; }
        public Move PlayerMove { get; init; }
    }
}