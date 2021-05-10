using MinesweeperController.SetupBehaviours.Factories;
using MinesweeperService.Enums;
using MinesweeperService.Values;
using MinesweeperTests.Helpers;
using MinesweeperTests.Helpers.Stubs;
using NUnit.Framework;

namespace MinesweeperTests.ServiceTests.Component_Tests
{
    public class GameStatusTests
    {
        private const string TestFolderPath = "Fakes/Grids/";

        private static readonly object[] BoundaryValuesForInputCoords = BoundaryValues.InputCoords;

        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void GameStatusBehaviour_WithInvalidCoordinates_ShouldReturnErrorStatus(Coords boundaryValuesForInputCoords)
        {
            //Arrange
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.Playing;
            
            //Act
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, boundaryValuesForInputCoords);

            //Assert
            Assert.AreEqual(GameStatus.Error, resultState.GameStatus);
        }
        
        [Test]
        public void GameStatusBehaviour_WithOneCornerMine_SuccessfullyReturnLossStatusWhenMineSelected()
        {
            //Arrange
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 0);
            
            //Act
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(GameStatus.Loss, resultState.GameStatus);
        }

        [Test]
        public void GameStatusBehaviour_WithAllEmptyTiles_SuccessfullyReturnWinStatusWhenTileSelected() 
        {
            //Arrange
            var testGridPath = TestFolderPath + "AllEmpty.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 1);
            
            //Act
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(GameStatus.Win, resultState.GameStatus);
        }

        [Test] public void GameStatusBehaviour_WithWinningGrid_SuccessfullyReturnLossStatusWhenMineSelected()
        {
            //Arrange
            var grid = new WinningGridStub();
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 0);
            
            //Act
            var resultState = TestExtensions.PerformMove(grid, moveStatusType, moveCoords);
            
            //Assert
            Assert.AreEqual(GameStatus.Loss, resultState.GameStatus);
        }

        [Test]
        public void GameStatusBehaviour_WithOneCornerMine_SuccessfullyReturnPlayingAfterOneMove()
        {
            //Arrange
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 1);
            
            //Act
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(GameStatus.Playing, resultState.GameStatus);
        }
        
        [Test]
        public void GameStatusBehaviour_WithOneCornerMine_SuccessfullyReturnPlayingWhenEmptyTilesCanBeSelected()
        {
            //Arrange
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var coords = new Coords(1, 0);
            var updatedTile =  grid.GetInvertedTileAt(coords);
            grid.ReplaceTileAt(new Coords(2,0), updatedTile); 
            grid.ReplaceTileAt(new Coords(4,2), updatedTile);

            //Act
            var resultState = TestExtensions.PerformMove(grid, GameStatus.Playing, coords);

            //Assert
            Assert.AreEqual(GameStatus.Playing, resultState.GameStatus);
        }
    }
}