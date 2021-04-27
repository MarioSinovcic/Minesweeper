namespace Minesweeper_Controller.SetupBehaviours.DTOs
{
    public sealed record JsonGridInputDTO(string MineTileChar, string EmptyTileChar, string[,] InitialGrid);
}