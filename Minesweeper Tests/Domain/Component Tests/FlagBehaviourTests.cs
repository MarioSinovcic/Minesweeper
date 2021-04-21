using Application.SetupBehaviours.Factories;
using Domain;
using Domain.Enums;
using Domain.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Component_Tests
{
    public class FlagBehaviourTests
    {
        private const string TestFolderPath = "Fakes/Grids/";

        [Test] 
        public void ShouldSetFlag_AtCorrectCoordinates()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var coords = new Coords(1, 0);
            var minesweeper = new Minesweeper(new GameState(GameStatus.SetFlag, grid, coords));
            minesweeper.PerformMove();
            var resultGrid = minesweeper.GetGameState().Grid;

            Assert.AreEqual(TileStatus.Flag, resultGrid.GetTileStatusAt(coords));
        }
        
        [Test] 
        public void ShouldSetFlagOnTile_WithMine()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "DiagonalMines.json").CreateGrid();
            var coords = new Coords(2, 2);
            var minesweeper = new Minesweeper(new GameState(GameStatus.SetFlag, grid, coords));
            minesweeper.PerformMove();
            var resultGrid = minesweeper.GetGameState().Grid;

            Assert.AreEqual(TileStatus.Flag, resultGrid.GetTileStatusAt(coords));
        }
        
        [Test] 
        public void ShouldShowTileThatHasAFlagOnIt_WithAScoreOfOne()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var coords = new Coords(1, 0);
            var minesweeper = new Minesweeper(new GameState(GameStatus.SetFlag, grid, coords));
            minesweeper.PerformMove();
            minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, coords));
            minesweeper.PerformMove();
            var resultGrid = minesweeper.GetGameState().Grid;

            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(coords));
        }
        
        [Test] 
        public void ShouldNotLoseGame_IfFlagPlacedOnMine()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "DiagonalMines.json").CreateGrid();
            var coords = new Coords(5, 0);
            var minesweeper = new Minesweeper(new GameState(GameStatus.SetFlag, grid, coords));
            minesweeper.PerformMove();
            var resultGrid = minesweeper.GetGameState().Grid;
            var resultStatus = minesweeper.GetGameState().GameStatus;

            Assert.AreEqual(TileStatus.Flag, resultGrid.GetTileStatusAt(coords));
            Assert.AreEqual(GameStatus.Playing, resultStatus);
        }
        
        [Test] 
        public void ShouldWinGame_IfFlagPlacedOnMineAndAllOthersAreMines()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "AllMines.json").CreateGrid();
            var coords = new Coords(0, 0);
            var minesweeper = new Minesweeper(new GameState(GameStatus.SetFlag, grid, coords));
            minesweeper.PerformMove();
            var resultGrid = minesweeper.GetGameState().Grid;
            var resultStatus = minesweeper.GetGameState().GameStatus;

            Assert.AreEqual(TileStatus.Flag, resultGrid.GetTileStatusAt(coords));
            Assert.AreEqual(GameStatus.Win, resultStatus);
        }
        
        
        [Test] 
        public void ShouldLoseGame_IfCoordsSetOnFlaggedMine()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "AllMines.json").CreateGrid();
            var coords = new Coords(0, 0);
            var minesweeper = new Minesweeper(new GameState(GameStatus.SetFlag, grid, coords));
            minesweeper.PerformMove();
            minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, coords));
            minesweeper.PerformMove();
            var resultGrid = minesweeper.GetGameState().Grid;
            var resultStatus = minesweeper.GetGameState().GameStatus;

            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(coords));
            Assert.AreEqual(GameStatus.Loss, resultStatus);
        }
        
        [Test] 
        public void ShouldShowTileThatHasAFlagOnIt_WithAScoreOfZero()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "ThreeMines_LargeGrid.json").CreateGrid();
            var coords = new Coords(6, 6);
            var minesweeper = new Minesweeper(new GameState(GameStatus.SetFlag, grid, coords));
            minesweeper.PerformMove();
            minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, coords));
            minesweeper.PerformMove();
            var resultGrid = minesweeper.GetGameState().Grid;

            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(coords));
        }
    }
}