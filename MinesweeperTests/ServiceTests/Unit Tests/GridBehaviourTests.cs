using System;
using MinesweeperController.SetupBehaviours.Factories;
using MinesweeperService.Values;
using MinesweeperTests.Helpers;
using NUnit.Framework;

namespace MinesweeperTests.ServiceTests.Unit_Tests
{
    public class GridBehaviourTests
    {
        private const string  TestFolderPath = "Fakes/Grids/";
        
        private static readonly object[] BoundaryValuesForInputCoords = BoundaryValues.InputCoords;

        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void GetNeighbouringMines_InvalidCoordinate_ShouldReturnArgumentException(Coords inputCoords)
        {
            //Arrange 
            var gridFactory = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json");

            //Act
            var resultGrid = gridFactory.CreateGrid();
            
            //Assert
            Assert.Throws<ArgumentException>(() => resultGrid.GetNeighbouringMinesAt(inputCoords));
        }
        
        [Test]
        public void GetNeighbouringMines_ZeroNeighbouringMines_ShouldSuccessfullyReturnZero()
        {
            //Arrange 
            var gridFactory = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json");
            var resultGrid = gridFactory.CreateGrid();

            //Act
            var neighbouringMines = resultGrid.GetNeighbouringMinesAt(new Coords(2, 2));
            
            //Assert
            Assert.AreEqual(0, neighbouringMines);
        }
        
        [Test]
        public void GetNeighbouringMines_OneNeighbouringMine_ShouldSuccessfullyReturnOne()
        {
            //Arrange 
            var gridFactory = new JsonGridSetupFactory(TestFolderPath + "ThreeMines.json");
            var resultGrid = gridFactory.CreateGrid();

            //Act
            var neighbouringMines = resultGrid.GetNeighbouringMinesAt(new Coords(1, 2));
            
            //Assert
            Assert.AreEqual(1, neighbouringMines);
        }
        
        [Test]
        public void GetNeighbouringMines_TwoNeighbouringMines_ShouldSuccessfullyReturnTwo()
        {
            //Arrange 
            var gridFactory = new JsonGridSetupFactory(TestFolderPath + "ThreeMines.json");
            var resultGrid = gridFactory.CreateGrid();

            //Act
            var neighbouringMines = resultGrid.GetNeighbouringMinesAt(new Coords(3, 1));
            
            //Assert
            Assert.AreEqual(2, neighbouringMines);
        }
        
        [Test]
        public void GetNeighbouringMines_ThreeNeighbouringMines_ShouldSuccessfullyReturnThree()
        {
            //Arrange       

            var gridFactory = new JsonGridSetupFactory(TestFolderPath + "FourSquareMines_SmallGrid.json");
            var resultGrid = gridFactory.CreateGrid();

            //Act
            var neighbouringMines = resultGrid.GetNeighbouringMinesAt(new Coords(2, 0));
            
            //Assert
            Assert.AreEqual(3, neighbouringMines);
        }
        
        [Test]
        public void GetNeighbouringMines_EightNeighbouringMines_ShouldSuccessfullyReturnEight()
        {
            //Arrange 
            var gridFactory = new JsonGridSetupFactory(TestFolderPath + "AllMines.json");
            var resultGrid = gridFactory.CreateGrid();

            //Act
            var neighbouringMines = resultGrid.GetNeighbouringMinesAt(new Coords(3, 3));
            
            //Assert
            Assert.AreEqual(8, neighbouringMines);
        }
        
        [Test]
        public void GetNeighbouringMines_OneVerticallyWrappedNeighbouringMine_ShouldSuccessfullyReturnZero()
        {
            //Arrange 
            var gridFactory = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json");
            var resultGrid = gridFactory.CreateGrid();

            //Act
            var neighbouringMines = resultGrid.GetNeighbouringMinesAt(new Coords(2, 1));
            
            //Assert
            Assert.AreEqual(0, neighbouringMines);
        }
        
        [Test]
        public void GetNeighbouringMines_OneHorizontallyWrappedNeighbouringMine_ShouldSuccessfullyReturnZero()
        {
            
            //Arrange 
            var gridFactory = new JsonGridSetupFactory(TestFolderPath + "FourCornerMines_SmallGrid.json");
            var resultGrid = gridFactory.CreateGrid();

            //Act
            var neighbouringMines = resultGrid.GetNeighbouringMinesAt(new Coords(2, 2));
            
            //Assert
            Assert.AreEqual(0, neighbouringMines);
        }
    }
}