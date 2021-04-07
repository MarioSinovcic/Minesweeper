using System;
using System.IO;
using Minesweeper.Application.Behaviour.Setup;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain
{
    public class JsonGridTests
    {
        private const string  TestFolderPath = "/Minesweeper Tests/Grid Fakes/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        [SetUp]
        public void Setup()
        {
            _currentPath = _currentPath.Substring(0, _currentPath.IndexOf("/Minesweeper/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
        }
        
        [Test]
        public void ShouldThrowOutOffRangeExceptionIfInputIsOutOfBounds()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "OneCornerMineSmallGrid.json");
            
            Assert.Throws<IndexOutOfRangeException>(() => resultGrid.GetNeighbouringMines(3,2));
        }
        
        [Test]
        public void ShouldReturnZeroForATileWithNoNeighbouringMines()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "OneCornerMineSmallGrid.json");
            
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(2,2));
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(0,3));
        }
        
        [Test]
        public void ShouldReturnOneForATileWithOneNeighbouringMine()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "ThreeMinesSmallGrid.json");
            
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(1,2));
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(0,4));
        }
        
        [Test]
        public void ShouldReturnTwoForATileWithTwoNeighbouringMines()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "ThreeMinesSmallGrid.json");
            
            Assert.AreEqual(2, resultGrid.GetNeighbouringMines(1,3));
        }
        
        [Test]
        public void ShouldWrapAroundHeightToDetectMines()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "OneCornerMineSmallGrid.json");
            
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(2,0));
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(0,1));
        }
        
        [Test]
        public void ShouldWrapAroundWidthAndHeightToDetectMines()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "MinedFourCornersSmall.json");
            
            Assert.AreEqual(3, resultGrid.GetNeighbouringMines(2,2));
        }
        
        [Test]
        public void ShouldReturnEightAtMost()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "AllMines.json");
            
            Assert.AreEqual(8, resultGrid.GetNeighbouringMines(3,3));
        }
    }
}