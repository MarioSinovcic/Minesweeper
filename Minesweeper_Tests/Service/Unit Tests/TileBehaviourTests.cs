using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Service.Unit_Tests
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