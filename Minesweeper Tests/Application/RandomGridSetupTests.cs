using System;
using System.Linq;
using Domain.Enums;
using Application.SetupBehaviours;
using Domain.Values;
using Minesweeper_Tests.Domain;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class RandomGridSetupTests
    {
        [Test]
        public void ShouldThrowApplicationException_ForInvalidParams()
        {
            Assert.Throws<ApplicationException>(() => new RandomGridSetup(-5, -0, 0).CreateGrid());
        }

        [Test]
        public void ShouldCreateGrid_WithCorrectDimensions()
        {
            var resultGrid = new RandomGridSetup(5,10,10).CreateGrid();
            Assert.AreEqual(10, resultGrid.Height);
            Assert.AreEqual(5, resultGrid.Width);
        }

        [Test]
        public void ShouldCreateGrid_WithCorrectDimensionsAndRandomMines()
        {
            var resultGrid = new RandomGridSetup(4,3,2).CreateGrid();
            var tiles = GridTestExtensions.LoopThroughGrid(resultGrid);
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            
            Assert.AreEqual(4, resultGrid.Width);
            Assert.AreEqual(3, resultGrid.Height);
            Assert.True(mineCount > 0); 
        }
        
        [Test]
        public void ShouldCreatedGrid_WithMinedAndEmptyTiles() //spy?? //TODO: refactor random tests
        {
            var resultGrid = new RandomGridSetup(10, 10, 10).CreateGrid();
            var tiles = GridTestExtensions.LoopThroughGrid(resultGrid);
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            var emptyCount = tiles.Count(x => x.Type == TileType.Empty);

            Assert.True(mineCount > 0); 
            Assert.True(emptyCount > 0);
        }
        
        [Test]
        public void ShouldGatherCorrectDimensionSettings_FromSettingsFile()
        {
            var resultGrid = new RandomGridSetup("Fakes/Settings/RandomGridSettings.json").CreateGrid();
        
            Assert.AreEqual(9,resultGrid.Height);
            Assert.AreEqual(10,resultGrid.Width);
        }
        
        [Test]
        public void ShouldThrowApplicationException_ForInvalidParamsInSettingsFile()
        {
            var error = Assert.Throws<ApplicationException>(() => new RandomGridSetup("Fakes/Settings/InvalidRandomGridSettings.json").CreateGrid());
            Assert.True(error.Message.Contains("Invalid input parameters")); 
        }
        
        [Test]
        public void ShouldThrowApplicationException_ForMissingParamsInSettingsFile()
        {
            var error = Assert.Throws<ApplicationException>(() => new RandomGridSetup("Fakes/Settings/IncompleteRandomGridSettings.json").CreateGrid());
            Assert.True(error.Message.Contains("Invalid input parameters"));
        }
    }
}