using Application.SetupBehaviours.Factories;
using Domain;
using Domain.Enums;
using Domain.Values;
using Minesweeper_Tests.Stubs;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Component_Tests
{
    public class GameStatusTests
    {
        private const string TestFolderPath = "Fakes/Grids/";

        private static readonly object[] BoundaryValuesForInputCoords =
        {
            new Coords(-4,2), //case 1
            new Coords(-9,-1), //case 2
            new Coords(-2,500), //case 3
            new Coords(30,-2), //case 4
            new Coords(4,245), //case 5
            new Coords(52,1), //case 6
        };

        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void ShouldReturnErrorState_IfInputIsOutOfBounds(Coords boundaryValuesForInputCoords)
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, boundaryValuesForInputCoords));
            minesweeper.PerformMove();
            var resultGameStatus = minesweeper.GetGameState().GameStatus;

            Assert.AreEqual(GameStatus.Error, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnLossState_IfMineSelected()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, new Coords(0, 0)));
            minesweeper.PerformMove();
            var resultGameStatus = minesweeper.GetGameState().GameStatus;

            Assert.AreEqual(GameStatus.Loss, resultGameStatus);
        }
        
        [Test]
        [Ignore("passes when run solo, but not when run w/ other tests")] //TODO: fix test
        public void ShouldReturnWinState_IfAllEmptyTilesAreShown() 
        {
            var grid = new WinningGridStub();
            var minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, new Coords(1, 1)));
            minesweeper.PerformMove();
            var resultGameStatus = minesweeper.GetGameState().GameStatus;
            
            Assert.AreEqual(GameStatus.Win, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnLossEven_IfAllEmptyTilesAreShown()
        {
            var grid = new WinningGridStub();
            var minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, new Coords(0, 0)));
            minesweeper.PerformMove();
            var resultGameStatus = minesweeper.GetGameState().GameStatus;
            
            Assert.AreEqual(GameStatus.Loss, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnPlaying_IfGameIsNotFinished()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, new Coords(1, 1)));
            minesweeper.PerformMove();
            var resultGameStatus = minesweeper.GetGameState().GameStatus;

            Assert.AreEqual(GameStatus.Playing, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnPlaying_IfGameIsNotFinishedInTheLateGame()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var coords = new Coords(1, 0);
            var updatedTile =  grid.GetInvertTileStatus(coords);
            grid.ReplaceTile(coords, updatedTile); 
            var minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, coords));
            minesweeper.PerformMove();
            var resultGameStatus = minesweeper.GetGameState().GameStatus;

            Assert.AreEqual(GameStatus.Playing, resultGameStatus);
        }
    }
}