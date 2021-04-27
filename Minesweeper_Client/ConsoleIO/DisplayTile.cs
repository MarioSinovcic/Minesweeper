using System;

namespace Minesweeper_Client.ConsoleIO
{
    internal sealed record DisplayTile (int Neighbours, ConsoleColor Color, char DisplayChar); 

}