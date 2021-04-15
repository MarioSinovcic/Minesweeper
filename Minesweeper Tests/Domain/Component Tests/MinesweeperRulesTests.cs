using Application.SetupBehaviours.Factories;
using Domain;
using Domain.Enums;
using Domain.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Component_Tests
{
    public class MinesweeperRulesTests
    {
        private const string TestFolderPath = "Fakes/Grids/";

        [Test]
        public void ShouldShowSingleTile_WithOneNeighbouringMine()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var move = new Move(grid, new Coords(1,0));
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(0,0)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(1,0)));
        }
        
        [Test]
        public void ShouldShowSingleTile_WithTwoNeighbouringMines()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "FourSquareMines_SmallGrid.json").CreateGrid();
            var move = new Move(grid, new Coords(2,0));
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(0,0)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(2,0)));
        }
        
        [Test]
        public void ShouldShowAllEmptyTiles_IfZeroNeighbouringMines() //refactor: doesn't test functionality properly, hard to find the point of failure
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "FourSquareMines_LargeGrid.json").CreateGrid();
            var move = new Move(grid, new Coords(3,3));
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(3,3)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(3,2)));
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
            var grid = new JsonGridSetupFactory(TestFolderPath + "DiagonalMines.json").CreateGrid();
            var move = new Move(grid, new Coords(1,0));
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
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
            var grid = new JsonGridSetupFactory(TestFolderPath + "FourSquareMines_SmallGrid.json").CreateGrid();
            var move = new Move(grid, new Coords(5,0));
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(3,3))); //test failure should be comms
        }
        
        [Test]
        public void ShouldShowAllEmptyTilesAndNumberedTiles_OnGridWithFiveMines()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "FiveMines_LargeGrid.json").CreateGrid();
            var move = new Move(grid, new Coords(0,3));
            var resultGrid = Minesweeper.PerformMove(move).Grid; //TODO: command query separation, perform move, get grid should be separate
            
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,3))); //TODO: grid method for getStatusAtCoords & getTypeAtCoords
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,2))); //Tile record not exposed
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(1,2)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(2,2)));
        }

    }
}