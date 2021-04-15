using System;
using Application.SetupBehaviours.Factories;
using Domain.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Entity_Unit_Tests
{
    public class GridBehaviourTests
    {
        private const string  TestFolderPath = "Fakes/Grids/";

        private static readonly object[] BoundaryValuesForInputCoords =
        {
            new Coords(-9,2), //case 1
            new Coords(-9,-1), //case 2
            new Coords(2,500), //case 3
            new Coords(29,5), //case 3
            new Coords(-3,50), //case 4
            new Coords(52,-1), //case 5
        };

        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void ShouldThrowOutOffRangeException_IfInputIsOutOfBounds(Coords inputCoords)
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            
            Assert.Throws<IndexOutOfRangeException>(() => resultGrid.GetNeighbouringMines(inputCoords));
        }
        
        [Test]
        public void ShouldReturnZero_ForATileWithNoNeighbouringMines()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(new Coords(2,2)));
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(new Coords(3,0)));
        }
        
        [Test]
        public void ShouldReturnOneForATile_WithOneNeighbouringMine()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "ThreeMines.json").CreateGrid();
            
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(new Coords(1,2)));
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(new Coords(4,0)));
        }
        
        [Test]
        public void ShouldReturnTwo_ForATileWithTwoNeighbouringMines()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "ThreeMines.json").CreateGrid();
            
            Assert.AreEqual(2, resultGrid.GetNeighbouringMines(new Coords(3,1)));
        }
        
        [Test]
        public void ShouldNotWrapAround_HeightToDetectMines()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(new Coords(2,1)));
            Assert.AreEqual(1, resultGrid.GetNeighbouringMines(new Coords(0,1)));
        }
        
        [Test]
        public void ShouldNotWrapAround_WidthAndHeightToDetectMines()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            
            Assert.AreEqual(0, resultGrid.GetNeighbouringMines(new Coords(2,2)));
        }
        
        [Test]
        public void ShouldReturn_EightNeighboursAtMost()
        {
            var resultGrid = new JsonGridSetupFactory(TestFolderPath + "AllMines.json").CreateGrid();
            
            Assert.AreEqual(8, resultGrid.GetNeighbouringMines(new Coords(3,3)));
        }
    }
}