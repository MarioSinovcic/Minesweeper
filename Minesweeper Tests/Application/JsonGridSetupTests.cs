using System;
using System.IO;
using Domain;
using Domain.Enums;
using Minesweeper.Application.Behaviour.Setup;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class GridSetupTests
    {
        private const string  TestFolderPath = "/Minesweeper Tests/Grid Fakes/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        [SetUp]
        public void Setup()
        {
            _currentPath = _currentPath.Substring(0, _currentPath.IndexOf("/Minesweeper/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
        }
        
        [Test]
        public void ShouldThrowIoExceptionForNullPath()
        {
            Assert.Throws<IOException>(() => JsonGridSetup.CreateGrid(null));
        }
        
        [Test]
        public void ShouldThrowIoExceptionForInvalidPaths()
        {
            Assert.Throws<IOException>(() => JsonGridSetup.CreateGrid("/user/"));
        }
        
        [Test]
        public void ShouldCreateGridWithCorrectDimensions()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "OneCornerMine.json");
            
            Assert.AreEqual(7, resultGrid.Width); //TODO: ask about spying this, rather than making it public
            Assert.AreEqual(3, resultGrid.Height);
        }

        [Test]
        public void ShouldReadMinesInJsonCornersFileCorrectly()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "FourCornerMines_SmallGrid.json");
            var expected = new Tile(TileType.Mine); //TODO: ask about this | new Tile{Type = TileType.Mine}
            
            Assert.AreEqual(expected.Status, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(expected.Type, resultGrid.Tiles[0,0].Type);
        }
        
        [Test]
        public void ShouldReadEmptyTilesInJsonCornersFileCorrectly()
        {
            var resultGrid = JsonGridSetup.CreateGrid( _currentPath + "FourCornerMines_SmallGrid.json");
            var expected = new Tile(TileType.Empty);
            
            Assert.AreEqual(expected.Status, resultGrid.Tiles[1,1].Status);
            Assert.AreEqual(expected.Type, resultGrid.Tiles[1,1].Type);
        }
        
        [Test]
        public void ShouldSetAllTileStatusesToHidden()
        {
            var resultGrid = JsonGridSetup.CreateGrid(_currentPath + "FourCornerMines_LargeGrid.json");

            foreach (var tile in resultGrid.Tiles)
            {
                Assert.AreEqual(TileStatus.Hidden, tile.Status);
            }
        }
    }
}