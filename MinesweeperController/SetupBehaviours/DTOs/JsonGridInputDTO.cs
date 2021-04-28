namespace MinesweeperController.SetupBehaviours.DTOs
{
    public sealed record JsonGridInputDTO(string MineTileChar, string EmptyTileChar, string[,] InitialGrid);
}