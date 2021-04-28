using System;

namespace MinesweeperClient.ConsoleIO
{
    internal sealed record DisplayTile (int Neighbours, ConsoleColor Color, char DisplayChar); 

}