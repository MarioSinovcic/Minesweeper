using System.IO;
using Minesweeper_Controller.SetupBehaviours.Factories;
using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class JsonGridSetupTests
    {
        private const string  TestFolderPath = "Fakes/Grids/"; 
        
        [Test]
        public void ShouldThrowIoException_ForNullPath()
        {
            Assert.Throws<IOException>(() => new JsonGridSetupFactory(null).CreateGrid());
        }
        
        [Test]
        public void ShouldThrowIoException_ForInvalidPaths()
        {
            Assert.Throws<IOException>(() => new JsonGridSetupFactory("/user/").CreateGrid());
        }
        
        [Test]
        public void ShouldCreateGrid_WithCorrectDimensions()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(7, resultGrid.Width);
            Assert.AreEqual(3, resultGrid.Height);
        }

        [Test]
        public void ShouldReadMines_FromJsonCornersFileCorrectly()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.GetTileStatusAt(new Coords(0,0)));
            Assert.AreEqual(TileType.Mine, resultGrid.GetTileTypeAt(new Coords(0,0)));
        }
        
        [Test]
        public void ShouldReadEmptyTiles_FromJsonCornersFileCorrectly()
        {
            var resultGrid = new JsonGridSetupFactory( TestFolderPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            Assert.AreEqual(TileType.Empty, resultGrid.GetTileTypeAt(new Coords(1,1)));
        }
        
        [Test]
        public void ShouldSetAllTileStatuses_ToHidden()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "FourCornerMines_LargeGrid.json").CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid);

            foreach (var tile in tiles)
            {
                Assert.AreEqual(TileStatus.Hidden, tile.Status);
            }
        }
    }
}