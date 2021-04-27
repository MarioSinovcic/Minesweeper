using System;
using System.Linq;
using Minesweeper_Controller.SetupBehaviours.Factories;
using Minesweeper_Service.Enums;
using NUnit.Framework;

namespace Minesweeper_Tests.Controller.SetupUnitTests
{
    public class RandomGridSetupTests
    {
        [Test]
        public void ShouldThrowApplicationException_ForInvalidParams()
        {
            Assert.Throws<ApplicationException>(() => new RandomGridSetupFactory(-5, -0, 0).CreateGrid());
        }

        [Test]
        public void ShouldCreateGrid_WithCorrectDimensions()
        {
            var resultGrid = new RandomGridSetupFactory(5,10,10).CreateGrid();
            Assert.AreEqual(10, resultGrid.Height);
            Assert.AreEqual(5, resultGrid.Width);
        }

        [Test]
        public void ShouldCreateGrid_WithCorrectDimensionsAndRandomMines()
        {
            var resultGrid = new RandomGridSetupFactory(4,3,2).CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid);
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            
            Assert.AreEqual(4, resultGrid.Width);
            Assert.AreEqual(3, resultGrid.Height);
            Assert.True(mineCount > 0); 
        }
        
        [Test]
        public void ShouldCreatedGrid_WithMinedAndEmptyTiles()
        {
            var resultGrid = new RandomGridSetupFactory(10, 10, 10).CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid).ToList();
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            var emptyCount = tiles.Count(x => x.Type == TileType.Empty);

            Assert.True(mineCount > 0); 
            Assert.True(emptyCount > 0);
        }
        
        [Test]
        public void ShouldGatherCorrectDimensionSettings_FromSettingsFile()
        {
            var resultGrid = new RandomGridSetupFromJsonFactory("Fakes/Settings/RandomGridSettings.json").CreateGrid();
        
            Assert.AreEqual(9,resultGrid.Height);
            Assert.AreEqual(10,resultGrid.Width);
        }
        
        [Test]
        public void ShouldThrowApplicationException_ForInvalidParamsInSettingsFile()
        {
            var error = Assert.Throws<ApplicationException>(() => new RandomGridSetupFromJsonFactory("Fakes/Settings/InvalidRandomGridSettings.json").CreateGrid());
            Assert.True(error.Message.Contains("Invalid input parameters")); 
        }
        
        [Test]
        public void ShouldThrowApplicationException_ForMissingParamsInSettingsFile()
        {
            var error = Assert.Throws<ApplicationException>(() => new RandomGridSetupFromJsonFactory("Fakes/Settings/IncompleteRandomGridSettings.json").CreateGrid());
            Assert.True(error.Message.Contains("Invalid input parameters"));
        }

        [Test] public void ShouldCreateGrid_WithCorrectDimensionsFromSettingsFile()
        {
            var resultGrid = new RandomGridSetupFromJsonFactory("Fakes/Settings/RandomGridSettings2.json").CreateGrid();
            Assert.AreEqual(9,resultGrid.Height);
            Assert.AreEqual(5,resultGrid.Width);
        }
        
        [Test] public void ShouldCreateGrid_WithMinesAndEmptyTiles() 
        {
            var resultGrid = new RandomGridSetupFromJsonFactory("Fakes/Settings/RandomGridSettings2.json").CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid).ToList();
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            var emptyCount = tiles.Count(x => x.Type == TileType.Empty);

            Assert.True(mineCount > 0); 
            Assert.True(emptyCount > 0);
        }
    }
}