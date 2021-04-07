using Domain;
using Domain.Enums;

namespace Minesweeper.DTOs
{
    public sealed class GameState //should be a record
    {
        public GameStatus GameStatus { get; set; } //should be inits
        public Grid Grid { get; set; }
    }
}