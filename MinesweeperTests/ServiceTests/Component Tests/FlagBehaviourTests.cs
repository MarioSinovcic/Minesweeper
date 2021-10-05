using MinesweeperService.Enums;
using MinesweeperService.Values;
using MinesweeperTests.Helpers;
using MinesweeperTests.Helpers.Stubs;
using NUnit.Framework;

namespace MinesweeperTests.ServiceTests.Component_Tests
{
    public class FlagBehaviourTests
    {
        private const string TestFolderPath = "Fakes/Grids/";

        [Test] 
        public void FlagBehaviour_GridWithOneCornerMine_SuccessfullySetsFlagOnEmptyTile() 
        {
            //Arrange 
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(1, 0);
            
            //Act
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            //Assert 
            Assert.AreEqual(TileStatus.Flag, resultState.Grid.GetTileStatusAt(moveCoords));
        }
        
        [Test] 
        public void FlagBehaviour_GridWithOneCornerMine_SuccessfullySetsFlagOnMinedTile() 
        {
            //Arrange
            var testGridPath = TestFolderPath + "DiagonalMines.json";
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(2, 2);

            //Act
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);
            
            //Assert
            Assert.AreEqual(TileStatus.Flag, resultState.Grid.GetTileStatusAt(moveCoords));
        }
        
        [Test] 
        public void FlagBehaviour_GridWithOneCornerMine_SuccessfullyRevealFlaggedTile()
        {
            //Arrange
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var firstMoveStatusType = GameStatus.SetFlag;
            var secondMoveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 0);
            
            //Act
            var firstMove = TestExtensions.PerformMove(testGridPath, firstMoveStatusType, moveCoords);
            var resultState = TestExtensions.PerformMove(firstMove.Grid, secondMoveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
        }
        
        [Test] 
        public void GameBehaviour_GridWithThreeMines_SuccessfullySetFlagAndRevealTile()
        {
            //Arrange
            var testGridPath = TestFolderPath + "ThreeMines_LargeGrid.json";
            var firstMoveStatusType = GameStatus.SetFlag;
            var secondMoveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(6, 6);
            
            //Act
            var firstMove = TestExtensions.PerformMove(testGridPath, firstMoveStatusType, moveCoords);
            var resultState = TestExtensions.PerformMove(firstMove.Grid, secondMoveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
            Assert.AreEqual(GameStatus.Playing, resultState.GameStatus);
        }
        
        [Test] 
        public void GameBehaviour_GridWithThreeMines_SuccessfullySetFlagAndRevealMine()
        {
            //Arrange
            var testGridPath = TestFolderPath + "ThreeMines_LargeGrid.json";
            var firstMoveStatusType = GameStatus.SetFlag;
            var secondMoveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 0);
            
            //Act
            var firstMove = TestExtensions.PerformMove(testGridPath, firstMoveStatusType, moveCoords);
            var resultState = TestExtensions.PerformMove(firstMove.Grid, secondMoveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
            Assert.AreEqual(GameStatus.Loss, resultState.GameStatus);
        }
        
        [Test] 
        public void GameBehaviour_GridWithDiagonalMines_SuccessfullyFlagMineWithoutLosingGame()
        {
            //Arrange
            var testGridPath = TestFolderPath + "DiagonalMines.json";
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(5, 0);
            
            //Act 
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(GameStatus.Playing, resultState.GameStatus);
        }
        
        [Test] 
        public void GameBehaviour_GridWithAllMines_SuccessfullyWinGameWithFlaggedMine()
        {
            //Arrange
            var testGridPath = TestFolderPath + "AllMines.json";
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(0, 0);
            
            //Act
            var resultState= TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(GameStatus.Win, resultState.GameStatus);
        }
        
        [Test] 
        public void GameBehaviour_GridWithAllMines_SuccessfullyLossGameWhenFlaggedMineRevealed()
        {
            //Arrange
            var testGridPath = TestFolderPath + "AllMines.json";
            var firstMoveStatusType = GameStatus.SetFlag;
            var secondMoveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 0);
            
            //Act
            var firstMove = TestExtensions.PerformMove(testGridPath, firstMoveStatusType, moveCoords);
            var resultState = TestExtensions.PerformMove(firstMove.Grid, secondMoveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(GameStatus.Loss, resultState.GameStatus);
        }
        
        [Test] 
        [Ignore("Path incorrect")]
        public void GameStatusBehaviour_WithWinningGrid_SuccessfullyReturnPlayingStatusIfTileFlagged()
        {
            //Arrange
            var grid = new WinningGridStub();
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(0, 0);
            
            //Act
            var resultState = TestExtensions.PerformMove(grid, moveStatusType, moveCoords);
            
            //Assert
            Assert.AreEqual(GameStatus.Playing, resultState.GameStatus);
        }
    }
}