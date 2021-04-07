using System.IO;
using Domain;
using Domain.Enums;
using Minesweeper.Application;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class GridSetupTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void ShouldThrowIOExceptionForNullPath()
        {
            Assert.Throws<IOException>(() => JsonGridSetup.CreateGrid(null));
        }
        
        [Test]
        public void ShouldThrowIOExceptionForInvalidPaths()
        {
            Assert.Throws<IOException>(() => JsonGridSetup.CreateGrid("/user/"));
        }

        [Test]
        public void ShouldReadMinesInJsonCornersFileCorrectly()
        {
            var resultGrid = JsonGridSetup.CreateGrid("/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/Minesweeper Tests/TestFakes/MinedFourCornersSmall.json");
            var expected = new Tile(TileType.Mine);
            
            Assert.AreEqual(expected.Status, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(expected.Type, resultGrid.Tiles[0,0].Type);
        }
        
        [Test]
        public void ShouldReadEmptyTilesInJsonCornersFileCorrectly()
        {
            var resultGrid = JsonGridSetup.CreateGrid("/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/Minesweeper Tests/TestFakes/MinedFourCornersSmall.json");
            var expected = new Tile(TileType.Empty);
            
            Assert.AreEqual(expected.Status, resultGrid.Tiles[1,1].Status);
            Assert.AreEqual(expected.Type, resultGrid.Tiles[1,1].Type);
        }
        
        [Test]
        public void ShouldSetAllTileStatusesToHidden()
        {
            var resultGrid = JsonGridSetup.CreateGrid("/Users/mario.sinovcic/Documents/Acceleration/Katas/Minesweeper/Minesweeper Tests/TestFakes/MinedFourCornersSmall.json");

            foreach (var tile in resultGrid.Tiles)
            {
                Assert.AreEqual(TileStatus.Hidden, tile.Status);
            }
        }
    }
}