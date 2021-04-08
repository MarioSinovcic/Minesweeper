using System;
using System.IO;
using Domain;
using Domain.Enums;
using Application.Application.Behaviour.Setup;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class JsonGridSetupTests
    {
        private const string  TestFolderPath = "/Application Tests/Grid Fakes/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        [SetUp]
        public void Setup()
        {
            _currentPath = _currentPath.Substring(0, _currentPath.IndexOf("/Application/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
        }
        
        [Test]
        public void ShouldThrowIoExceptionForNullPath()
        {
            Assert.Throws<IOException>(() => new JsonGridSetup(null).CreateGrid());
        }
        
        [Test]
        public void ShouldThrowIoExceptionForInvalidPaths()
        {
            Assert.Throws<IOException>(() => new JsonGridSetup("/user/").CreateGrid());
        }
        
        [Test]
        public void ShouldCreateGridWithCorrectDimensions()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(7, resultGrid.Width); //TODO: ask about spying this, rather than making it public
            Assert.AreEqual(3, resultGrid.Height);
        }

        [Test]
        public void ShouldReadMinesInJsonCornersFileCorrectly()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            var expected = new Tile{Type = TileType.Mine}; //TODO: ask about this | new Tile{Type = TileType.Mine}
            
            Assert.AreEqual(expected.Status, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(expected.Type, resultGrid.Tiles[0,0].Type);
        }
        
        [Test]
        public void ShouldReadEmptyTilesInJsonCornersFileCorrectly()
        {
            var resultGrid = new JsonGridSetup( _currentPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            var expected = new Tile(TileType.Empty);
            
            Assert.AreEqual(expected.Status, resultGrid.Tiles[1,1].Status);
            Assert.AreEqual(expected.Type, resultGrid.Tiles[1,1].Type);
        }
        
        [Test]
        public void ShouldSetAllTileStatusesToHidden()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "FourCornerMines_LargeGrid.json").CreateGrid();

            foreach (var tile in resultGrid.Tiles)
            {
                Assert.AreEqual(TileStatus.Hidden, tile.Status);
            }
        }
    }
}