using System;
using System.Linq;
using Domain;
using Domain.Enums;
using Application.Application.Behaviour.Setup;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class RandomGridSetupTests
    {
        
        [Test]
        public void ShouldThrowApplicationExceptionForInvalidParams()
        {
            Assert.Throws<ApplicationException>(() => new RandomGridSetup(-5, -0, 0).CreateGrid());
        }

        [Test]
        public void ShouldCreateGridWithCorrectDimensions()
        {
            var resultGrid = new RandomGridSetup(5,10,10).CreateGrid();
            Assert.AreEqual(5, resultGrid.Width);
            Assert.AreEqual(10, resultGrid.Height);
        }
        
        [Test]
        public void ShouldHaveMinedAndEmptyTiles() //spy??
        {
            var resultGrid = new RandomGridSetup(10,10,10).CreateGrid();
            var tiles = resultGrid.Tiles.Cast<Tile>().ToList();
            var mineCount = tiles.Count(x => x.Type == TileType.Mine);
            var emptyCount = tiles.Count(x => x.Type == TileType.Empty);

            Assert.True(mineCount > 0); 
            Assert.True(emptyCount > 0);
        }
    }
}