using Minesweeper_Controller.SetupBehaviours.Factories;
using Minesweeper_Service;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;
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
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.Playing;
            
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, boundaryValuesForInputCoords);

            Assert.AreEqual(GameStatus.Error, resultState.GameStatus);
        }
        
        [Test]
        public void ShouldReturnLossState_IfMineSelected()
        {
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 0);
            
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            Assert.AreEqual(GameStatus.Loss, resultState.GameStatus);
        }

        [Test]
        public void ShouldReturnWinState_IfAllTilesAreEmptyAndOneIsSelected() 
        {
            var testGridPath = TestFolderPath + "AllEmpty.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 1);
            
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            Assert.AreEqual(GameStatus.Win, resultState.GameStatus);
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
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 1);
            
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            Assert.AreEqual(GameStatus.Playing, resultState.GameStatus);
        }
        
        [Test]
        public void ShouldReturnPlaying_IfGameIsNotFinishedInTheLateGame()
        {

            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var coords = new Coords(1, 0);
            var updatedTile =  grid.GetInvertTileStatus(coords);
            grid.ReplaceTile(new Coords(2,0), updatedTile); 
            grid.ReplaceTile(new Coords(4,2), updatedTile); 
            var minesweeper = new Minesweeper(new GameState(GameStatus.Playing, grid, coords));
            minesweeper.PerformMove();
            var resultGameStatus = minesweeper.GetGameState().GameStatus;

            Assert.AreEqual(GameStatus.Playing, resultGameStatus);
        }
    }
}