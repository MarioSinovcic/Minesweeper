using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs
{
    public record GameStateDto 
    {
        public GameStatus GameStatus { get; init; } 
        public int[] PlayerMove { get; init; } 
        public Grid Grid { get; init; }
    }
}