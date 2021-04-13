using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Entity_Unit_Tests
{
    public class TileBehaviourTests
    {
        [Test]
        public void ShouldChangeTileStatus_FromHiddenToShown()
        {
            var resultTile = new Tile(TileType.Empty);
            resultTile = resultTile.ShowTile();
            
            Assert.AreEqual(TileStatus.Shown, resultTile.Status);
        }
    }
}