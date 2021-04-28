using MinesweeperService.Enums;
using MinesweeperService.Values;
using MinesweeperTests.Helpers;
using NUnit.Framework;

namespace MinesweeperTests.ServiceTests.Component_Tests
{
    public class MinesweeperRulesTests 
    {
        private const string TestFolderPath = "Fakes/Grids/";

        [Test]
        public void Minesweeper_WithOneCornerMine_SuccessfullyShownTileWithOneNeighbouringMine()
        {
            //Arrange
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 0);

            //Act
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            //Assert
            Assert.AreEqual(TileStatus.Hidden, resultState.Grid.GetTileStatusAt(new Coords(0, 0)));
            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
        }

        [Test]
        public void Minesweeper_WithOneCornerMine_SuccessfullyShownTileWithTwoNeighbouringMines()
        {
            //Arrange
            var testGridPath = TestFolderPath + "FourSquareMines_SmallGrid.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(2, 0);

            //Assert
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            //Act
            Assert.AreEqual(TileStatus.Hidden, resultState.Grid.GetTileStatusAt(new Coords(0, 0)));
            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
        }
        
        
        [Test]
        public void Minesweeper_WithFourSquareMines_SuccessfullyCascadeEmptyTiles()
        {
            //Arrange
            var testGridPath = TestFolderPath + "FourSquareMines_LargeGrid.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(3, 3);

            //Act
            var resultGrid = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords).Grid;

            //Assert
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(3,3)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(3,1)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(3,4)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(3,5)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(4,3)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(2,3)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(5,3)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(1,3)));
        }
        
        [Test]
        public void Minesweeper_WithDiagonalMines_SuccessfullyCascadeEmptyTiles()
        {
            //Arrange
            var testGridPath = TestFolderPath + "DiagonalMines.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 0);

            //Act
            var resultGrid = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords).Grid;

            //Assert
            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(5,1)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,0)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(1,0)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(3,1)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(2,2)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,3)));
        }

        [Test]
        public void Minesweeper_WithFourSquareMines_SuccessfullyCascadeEmptyAndNumberTiles()
        {
            //Arrange
            var testGridPath = TestFolderPath + "FourSquareMines_SmallGrid.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(5, 0);

            //Act
            var resultGrid = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords).Grid;

            //Assert
            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(3, 3)));
        }

        [Test]
        public void Minesweeper_WithFiveMines_SuccessfullyCascadeEmptyAndNumberTiles()
        {
            //Arrange
            var testGridPath = TestFolderPath + "FiveMines_LargeGrid.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 3);

            //Act
            var resultGrid = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords).Grid;
            
            //Assert
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,3))); 
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,2)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(1,2)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(2,2)));
        }

    }
}