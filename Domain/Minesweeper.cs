using Domain.Enums;
using Domain.Interfaces;
using Domain.Values;

namespace Domain
{
    public static class Minesweeper //TODO: setup mediator
    {
        //TODO: holds state, with command query opps
        //TODO: gamestate factory
        public static GameState PerformMove(Move move)
        {
            var gameStatus = RuleEvaluator.EvaluateGameStatus(move.Grid, move.Coords); //TODO: fix DRY
            if (gameStatus == GameStatus.Error)
            {
                return new GameState(GameStatus.Error,move.Grid, move.Coords);
            }
            var updatedGrid = UpdateGrid(move);
            var updatedGameStatus = RuleEvaluator.EvaluateGameStatus(updatedGrid, move.Coords);

            return new GameState(updatedGameStatus, updatedGrid, move.Coords);
        }

        private static IGrid UpdateGrid(Move move)
        {
            var grid = move.Grid;
            var coords = move.Coords;
            var neighbours = grid.GetNeighbouringMines(coords);

            if (grid.GetTileTypeAt(coords) == TileType.Mine)
            {
                var updatedTile = grid.GetInvertTileStatus(coords);
                grid.ReplaceTile(coords, updatedTile);
                return grid;
            }
            
            switch (neighbours)
            {
                case > 0:
                    var updatedTile = grid.GetInvertTileStatus(coords);
                    grid.ReplaceTile(coords, updatedTile);
                    return grid;
                case 0:
                    ShowAllSurroundingEmptyTiles(grid, coords);
                    break;
            }
            return grid;
        }

        private static void ShowAllSurroundingEmptyTiles(IGrid grid, Coords givenCoords)
        {
            var width = grid.Width;
            var height = grid.Height;
            
            for (var xOff = -1 ; xOff < 2; xOff++)
            {
                for (var yOff = -1; yOff < 2; yOff++)
                {
                    var xCoord = givenCoords.X + xOff;
                    var yCoord = givenCoords.Y + yOff;
                    var coords = new Coords(xCoord, yCoord);
                    
                    if (xCoord <= -1 || xCoord >= width || yCoord <= -1 || yCoord >= height) continue;
                    if (grid.GetTileTypeAt(coords) != TileType.Empty ||
                        grid.GetTileStatusAt(coords) != TileStatus.Hidden) continue;
                    var updatedTile = grid.GetInvertTileStatus(coords);
                    grid.ReplaceTile(coords, updatedTile);
                    
                    if (!(grid.GetNeighbouringMines(coords) > 0))
                    {
                        ShowAllSurroundingEmptyTiles(grid, coords);
                    }
                }
            }
        }
    }
}