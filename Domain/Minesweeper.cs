using Domain.Enums;
using Domain.Interfaces;
using Domain.Values;

namespace Domain
{
    public class Minesweeper //TODO: setup mediator
    {
        private readonly GameStatus _gameStatus;
        private readonly IGrid _grid;
        private readonly Coords _coords;

        private GameState _gameState;
        
        public Minesweeper(GameState gameState)
        {
            _gameStatus = gameState.GameStatus;
            _grid = gameState.Grid;
            _coords = gameState.Coords;
        }
        
        //TODO: holds state, with command query opps
        //TODO: gamestate factory
        public void PerformMove()
        {
            var gameStatus = RuleEvaluator.ValidateInput(_grid, _coords, _gameStatus); //TODO: fix DRY
            if (gameStatus == GameStatus.Error)
            {
                _gameState = new GameState(GameStatus.Error,_grid, _coords);
            }
            else
            {
                var updatedGrid = UpdateGrid();
                var updatedGameStatus = RuleEvaluator.EvaluateGameStatus(updatedGrid, _coords, _gameStatus);

                _gameState = new GameState(updatedGameStatus, updatedGrid, _coords);
            }
        }

        public GameState GetGameState()
        {
            return _gameState;
        }

        private IGrid UpdateGrid() //TODO: IRules behaviour
                                        //- bool = Rule.IsRuleActive(GameState)
                                        //- GameState = Rule.UpdateBasedOnRule(GameState)
        {
            if (_gameStatus == GameStatus.SetFlag)
            {
                var tileType = _grid.GetTileTypeAt(_coords);
                _grid.ReplaceTile(_coords, new Tile(tileType, TileStatus.Flag));
                return _grid;
            }

            if (_gameStatus == GameStatus.Playing && _grid.GetTileStatusAt(_coords) == TileStatus.Flag)
            {
                var tileType = _grid.GetTileTypeAt(_coords);
                _grid.ReplaceTile(_coords, new Tile(tileType, TileStatus.Shown));
            }
            
            
            var neighbours = _grid.GetNeighbouringMines(_coords);

            if (_grid.GetTileTypeAt(_coords) == TileType.Mine)
            {
                var updatedTile = _grid.GetInvertTileStatus(_coords);
                _grid.ReplaceTile(_coords, updatedTile);
                return _grid;
            }

            switch (neighbours)
            {
                case > 0:
                    var updatedTile = _grid.GetInvertTileStatus(_coords);
                    _grid.ReplaceTile(_coords, updatedTile);
                    return _grid;
                case 0:
                    ShowAllSurroundingEmptyTiles(_grid, _coords);
                    break;
            }

            return _grid;
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