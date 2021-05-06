using System;
using System.Linq;
using MinesweeperController.SetupBehaviours.Factories;
using MinesweeperService.Enums;
using MinesweeperTests.Helpers;
using NUnit.Framework;

namespace MinesweeperTests.ControllerTests.SetupUnitTests
{
    public class RandomGridSetupTests
    {
        private static readonly object[] InvalidGridParamsBoundaryValues = BoundaryValues.InvalidGridSetupParams;
        private static readonly object[] ValidGridParamsBoundaryValues = BoundaryValues.ValidGridSetupParameters;


        [TestCaseSource(nameof(InvalidGridParamsBoundaryValues))]
        public void CreateGrid_WithInvalidParameters_ShouldReturnApplicationException(int[] gridParams)
        {
            //Arrange
            var width = gridParams[0];
            var height = gridParams[1];
            var mineFrequency = gridParams[2];
            
            //Act //Assert
            Assert.Throws<ApplicationException>(() => new RandomGridSetupFactory(width, height, mineFrequency).CreateGrid());
        }

        [TestCaseSource(nameof(ValidGridParamsBoundaryValues))]
        public void CreateGrid_WithValidParameters_ShouldGridWithCorrectDimensions(int[] gridParams)
        {
            //Arrange
            var width = gridParams[0];
            var height = gridParams[1];
            var mineFrequency = gridParams[2];
            
            //Act 
            var resultGrid = new RandomGridSetupFactory(width,height,mineFrequency).CreateGrid();
            
            //Assert
            Assert.AreEqual(height, resultGrid.Height);
            Assert.AreEqual(width, resultGrid.Width);
        }

        [Test]
        public void CreateGrid_WithHighMineFrequency_ShouldGridWithSomeMines()
        {
            //Arrange
            var width = 4;
            var height = 4;
            var mineFrequency = 2;

            //Act
            var resultGrid = new RandomGridSetupFactory(width,height, mineFrequency).CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid);
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            
            //Assert
            Assert.True(mineCount > 0); 
        }
        
        [Test]
        public void CreateGrid_WithMediumMineFrequency_ShouldGridWithSomeMinesAndSomeEmptyTiles()
        {
            //Arrange
            var width = 10;
            var height = 10;
            var mineFrequency = 6;

            //Act
            var resultGrid = new RandomGridSetupFactory(width,height, mineFrequency).CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid).ToList();
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            var emptyCount = tiles.Count(x => x.Type == TileType.Empty);

            //Assert
            Assert.True(mineCount > 0); 
            Assert.True(emptyCount > 0);
        }
        
        
        [Test]
        public void CreateGrid_WithInvalidSettings_ShouldReturnApplicationException()
        {
            //Arrange
            var settingsFilePath = "Fakes/Settings/InvalidRandomGridSettings.json";
            
            //Act //Assert
            var error = Assert.Throws<ApplicationException>(() => new RandomGridSetupFromJsonFactory(settingsFilePath).CreateGrid());
            Assert.True(error.Message.Contains("Invalid input parameters")); 
        }
        
        [Test]
        public void CreateGrid_WithMissingSettingsParams_ShouldReturnApplicationException()
        {
            //Arrange
            var settingsFilePath = "Fakes/Settings/IncompleteRandomGridSettings.json";
            
            //Act //Assert
            var error = Assert.Throws<ApplicationException>(() => new RandomGridSetupFromJsonFactory(settingsFilePath).CreateGrid());
            Assert.True(error.Message.Contains("Invalid input parameters"));
        }

        [Test]
        public void CreateGrid_WithValidSettingsFile_ShouldGridWitCorrectDimensions()
        {
            //Arrange
            var settingsFilePath = "Helpers/Fakes/Settings/RandomLargeGridSettings.json";
            
            //Act
            var resultGrid = new RandomGridSetupFromJsonFactory(settingsFilePath).CreateGrid();
        
            //Assert
            Assert.AreEqual(99,resultGrid.Height);
            Assert.AreEqual(99,resultGrid.Width);
        }
        
        [Test] 
        public void CreateGrid_WithValidSettingsFile_ShouldGridWithSomeMines() 
        {
            //Arrange
            var settingsFilePath = "Helpers/Fakes/Settings/RandomSmallGridSettings.json";
            
            //Act
            var resultGrid = new RandomGridSetupFromJsonFactory(settingsFilePath).CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid).ToList();
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);

            //Assert
            Assert.True(mineCount > 0); 
        }
        
        [Test] public void CreateGrid_WithValidSettingsFile_ShouldGridWithSomeMinesAndSomeEmptyTiles() 
        {
            //Arrange
            var settingsFilePath = "Helpers/Fakes/Settings/RandomLargeGridSettings.json";
            
            //Act
            var resultGrid = new RandomGridSetupFromJsonFactory(settingsFilePath).CreateGrid();
            var tiles = TestExtensions.LoopThroughGrid(resultGrid).ToList();
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            var emptyCount = tiles.Count(x => x.Type == TileType.Empty);

            //Assert
            Assert.True(mineCount > 0); 
            Assert.True(emptyCount > 0);
        }
    }
}