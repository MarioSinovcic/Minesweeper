using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Component_Tests
{
    public class MinesweeperRulesTests //get rid of all "should"s
    {
        private const string TestFolderPath = "Fakes/Grids/";

        [Test]
        public void ShouldShowSingleTile_WithOneNeighbouringMine()
        {
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 0);

            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            Assert.AreEqual(TileStatus.Hidden, resultState.Grid.GetTileStatusAt(new Coords(0, 0)));
            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
        }

        [Test]
        public void ShouldShowSingleTile_WithTwoNeighbouringMines()
        {
            
            var testGridPath = TestFolderPath + "FourSquareMines_SmallGrid.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(2, 0);

            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            Assert.AreEqual(TileStatus.Hidden, resultState.Grid.GetTileStatusAt(new Coords(0, 0)));
            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
        }
        
        
        [Test]
        public void ShouldShowAllEmptyTiles_IfZeroNeighbouringMines()
        {
            var testGridPath = TestFolderPath + "FourSquareMines_LargeGrid.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(3, 3);

            var resultGrid = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords).Grid;

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
        public void ShouldShowAllEmptyTilesAndNumberedTiles_OnDiagonallyMinedGrid()
        {
            var testGridPath = TestFolderPath + "DiagonalMines.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 0);

            var resultGrid = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords).Grid;

            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(5,1)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,0)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(1,0)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(3,1)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(2,2)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,3)));
        }

        [Test]
        public void ShouldShowAllEmptyTilesAndNumberedTiles_OnGridWithFourMines()
        {
            var testGridPath = TestFolderPath + "FourSquareMines_SmallGrid.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(5, 0);

            var resultGrid = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords).Grid;

            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(3, 3)));
        }

        [Test]
        public void ShouldShowAllEmptyTilesAndNumberedTiles_OnGridWithFiveMines()
        {
            var testGridPath = TestFolderPath + "FiveMines_LargeGrid.json";
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 3);

            var resultGrid = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords).Grid;
            
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,3))); 
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,2)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(1,2)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(2,2)));
        }

    }
}