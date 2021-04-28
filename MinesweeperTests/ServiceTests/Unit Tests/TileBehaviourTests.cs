using MinesweeperService.Enums;
using MinesweeperService.Values;
using NUnit.Framework;

namespace MinesweeperTests.ServiceTests.Unit_Tests
{
    public class TileBehaviourTests
    {
        [Test]
        public void GetNeighbouringMines_HiddenEmptyTile_ShouldSuccessfullyReturnShownTile()
        {
            //Arrange
            var resultTile = new Tile(TileType.Empty);
            
            //Act
            resultTile = resultTile.ShowTile();
            
            //Assert
            Assert.AreEqual(TileStatus.Shown, resultTile.Status);
        }
        
        [Test]
        public void GetNeighbouringMines_HiddenMineTile_ShouldSuccessfullyReturnShownTile()
        {
            //Arrange
            var resultTile = new Tile(TileType.Mine);
            
            //Act
            resultTile = resultTile.ShowTile();
            
            //Assert
            Assert.AreEqual(TileStatus.Shown, resultTile.Status);
        }
        
        [Test]
        public void GetNeighbouringMines_ShownHiddenTile_ShouldSuccessfullyReturnShownTile()
        {
            //Arrange
            var resultTile = new Tile(TileType.Mine, TileStatus.Shown);
            
            //Act
            resultTile = resultTile.ShowTile();
            
            //Assert
            Assert.AreEqual(TileStatus.Shown, resultTile.Status);
        }
    }
}