using System;
using System.IO;
using Application.Behaviour.Setup;
using Domain.Entities;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Entity_Unit_Tests
{
    public class GridBehaviourTests
    {
        private const string  TestFolderPath = "/Minesweeper Tests/Grid Fakes/";
        private static string _currentPath = Directory.GetCurrentDirectory();
        
        private static readonly object[] BoundaryValuesForInputCoords =
        {
            new Coords{X = -9, Y = 2}, //case 1
            new Coords{X = -9,Y = -1}, //case 2
            new Coords{X = 2, Y = 500}, //case 3
            new Coords{X = 29, Y = 5}, //case 3
            new Coords{X = -3, Y = 50}, //case 4
            new Coords{X = 52, Y = -1}, //case 5
        };

        [SetUp]
        public void Setup()
        {
            _currentPath = _currentPath.Substring(0, _currentPath.IndexOf("/Minesweeper/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
        }
        
        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void ShouldThrowOutOffRangeExceptionIfInputIsOutOfBounds(Coords inputCoords)
        {
            var resultGrid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            
            Assert.Throws<IndexOutOfRangeException>(() => resultGrid.GetNeighbouringMines(inputCoords));
        }
        
        [Test]
        public void ShouldReturnZeroForATileWithNoNeighbouringMines()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            
//            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(new Coords{X = 2,Y = 2}));
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(new Coords{X = 3,Y = 0}));
        }
        
        [Test]
        public void ShouldReturnOneForATileWithOneNeighbouringMine()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "ThreeMines.json").CreateGrid();
            
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(new Coords{X = 1,Y = 2}));
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(new Coords{X = 4,Y = 0}));
        }
        
        [Test]
        public void ShouldReturnTwoForATileWithTwoNeighbouringMines()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "ThreeMines.json").CreateGrid();
            
            Assert.AreEqual(2, resultGrid.GetNeighbouringMines(new Coords{X = 3,Y = 1}));
        }
        
        [Test]
        public void ShouldWrapAroundHeightToDetectMines()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(new Coords{X = 0,Y = 2}));
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(new Coords{X = 0,Y = 1}));
        }
        
        [Test]
        public void ShouldWrapAroundWidthAndHeightToDetectMines()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            
            Assert.AreEqual(3, resultGrid.GetNeighbouringMines(new Coords{X = 2,Y = 2}));
        }
        
        [Test]
        public void ShouldReturnEightNeighboursAtMost()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "AllMines.json").CreateGrid();
            
            Assert.AreEqual(8, resultGrid.GetNeighbouringMines(new Coords{X = 3,Y = 3}));
        }
    }
}