using Domain.Enums;
using Domain.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Unit_Tests
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