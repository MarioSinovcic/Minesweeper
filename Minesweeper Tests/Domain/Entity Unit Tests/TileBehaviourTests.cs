using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Entity_Unit_Tests
{
    public class TileBehaviourTests
    {
        [Test]
        public void ShouldChangeFromHiddenToShown()
        {
            var resultTile = new Tile {Type = TileType.Empty};
            resultTile = resultTile.ShowTile();
            
            Assert.AreEqual(TileStatus.Shown, resultTile.Status);
        }
    }
}