using System.IO;
using MinesweeperController.SetupBehaviours.Factories;
using MinesweeperService.Enums;
using MinesweeperService.Values;
using MinesweeperTests.Helpers;
using NUnit.Framework;

namespace MinesweeperTests.ControllerTests.SetupUnitTests
{
    public class JsonGridSetupTests
    {
        private const string  TestFolderPath = "Fakes/Grids/"; 
        private static readonly object[] BoundaryValuesForInvalidGridFilePaths = BoundaryValues.InvalidGridFilePaths;
        
        [TestCaseSource(nameof(BoundaryValuesForInvalidGridFilePaths))]
        public void CreateGrid_WithInvalidFilePath_ShouldReturnArgumentException(string invalidPath)
        {
            //Arrange
            var pathname = invalidPath;
            
            //Act //Assert
            Assert.Throws<IOException>(() => new JsonGridSetupFactory(pathname).CreateGrid());
        }

        [Test]
        public void CreateGrid_WithOneCornerMine_ShouldReturnGridWithCorrectDimensions()
        {
            //Arrange
            var pathname = TestFolderPath + "OneCornerMine.json";
            
            //Act
            var resultGrid = new JsonGridSetupFactory(pathname).CreateGrid();
            
            //Assert
            Assert.AreEqual(7, resultGrid.Width);
            Assert.AreEqual(3, resultGrid.Height);
        }

        [Test]
        public void CreateGrid_WithFourCornerMines_ShouldReturnGridWithCorrectlyPlacedMines()
        {
            //Arrange
            var pathname = TestFolderPath + "FourCornerMines_SmallGrid.json";
            
            //Act
            var resultGrid = new JsonGridSetupFactory(pathname).CreateGrid();
            
            //Assert
            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(0,0)));
            Assert.AreEqual(TileType.Mine, resultGrid.GetTileTypeAt(new Coords(0,0)));
        }
        
        [Test]
        public void CreateGrid_WithFourCorner_ShouldReturnGridWithCorrectlyPlacedEmptyTiles()
        {
            //Arrange
            var pathname = TestFolderPath + "FourCornerMines_SmallGrid.json";
            
            //Act
            var resultGrid = new JsonGridSetupFactory(pathname).CreateGrid();
            
            //Assert
            Assert.AreEqual(TileType.Empty, resultGrid.GetTileTypeAt(new Coords(1,0)));
            Assert.AreEqual(TileType.Empty, resultGrid.GetTileTypeAt(new Coords(0,1)));
            Assert.AreEqual(TileType.Empty, resultGrid.GetTileTypeAt(new Coords(1,1)));
            Assert.AreEqual(TileType.Empty, resultGrid.GetTileTypeAt(new Coords(2,1)));
            Assert.AreEqual(TileType.Empty, resultGrid.GetTileTypeAt(new Coords(1,2)));
        }
        
        [Test]
        public void CreateGrid_WithFourCornerMines_ShouldReturnGridAllStatusesSetToHidden()
        {
            //Arrange
            var pathname = TestFolderPath + "FourCornerMines_SmallGrid.json";
            
            //Act
            var resultGrid = new JsonGridSetupFactory(pathname).CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid);

            //Assert
            foreach (var tile in tiles)
            {
                Assert.AreEqual(TileStatus.Hidden, tile.Status);
            }
        }
    }
}