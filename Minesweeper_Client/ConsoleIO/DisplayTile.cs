using System;

namespace Minesweeper_Client.ConsoleIO
{
    public sealed record DisplayTile (int Neighbours, ConsoleColor Color, char DisplayChar); 

}