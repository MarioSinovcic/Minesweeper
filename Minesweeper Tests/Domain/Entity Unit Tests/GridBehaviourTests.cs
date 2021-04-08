using System;
using System.IO;
using Application.Application.Behaviour.Setup;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain
{
    public class GridBehaviourTests
    {
        private const string  TestFolderPath = "/Application Tests/Grid Fakes/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        [SetUp]
        public void Setup()
        {
            _currentPath = _currentPath.Substring(0, _currentPath.IndexOf("/Application/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
        }
        
        [Test]
        public void ShouldThrowOutOffRangeExceptionIfInputIsOutOfBounds()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            
            Assert.Throws<IndexOutOfRangeException>(() => resultGrid.GetNeighbouringMines(3,2));
        }
        
        [Test]
        public void ShouldReturnZeroForATileWithNoNeighbouringMines()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(2,2));
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(0,3));
        }
        
        [Test]
        public void ShouldReturnOneForATileWithOneNeighbouringMine()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "ThreeMines.json").CreateGrid();
            
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(1,2));
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(0,4));
        }
        
        [Test]
        public void ShouldReturnTwoForATileWithTwoNeighbouringMines()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "ThreeMines.json").CreateGrid();
            
            Assert.AreEqual(2, resultGrid.GetNeighbouringMines(1,3));
        }
        
        [Test]
        public void ShouldWrapAroundHeightToDetectMines()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(2,0));
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(0,1));
        }
        
        [Test]
        public void ShouldWrapAroundWidthAndHeightToDetectMines()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            
            Assert.AreEqual(3, resultGrid.GetNeighbouringMines(2,2));
        }
        
        [Test]
        public void ShouldReturnEightNeighboursAtMost()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "AllMines.json").CreateGrid();
            
            Assert.AreEqual(8, resultGrid.GetNeighbouringMines(3,3));
        }
    }
}