using System;
using System.IO;
using Application.Behaviour.Setup;
using Domain.Enums;
using Domain.Entities;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class JsonGridSetupTests
    {
        private const string  TestFolderPath = "/Minesweeper Tests/Fakes/Grids/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        [SetUp]
        public void Setup()
        {
            _currentPath = _currentPath.Substring(0, _currentPath.IndexOf("/Minesweeper/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
            Console.WriteLine(_currentPath);
        }
        
        [Test]
        public void ShouldThrowIoException_ForNullPath()
        {
            Assert.Throws<IOException>(() => new JsonGridSetup(null).CreateGrid());
        }
        
        [Test]
        public void ShouldThrowIoException_ForInvalidPaths()
        {
            Assert.Throws<IOException>(() => new JsonGridSetup("/user/").CreateGrid());
        }
        
        [Test]
        public void ShouldCreateGrid_WithCorrectDimensions()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(7, resultGrid.Width);
            Assert.AreEqual(3, resultGrid.Height);
        }

        [Test]
        public void ShouldReadMines_FromJsonCornersFileCorrectly()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            var expected = new Tile{Type = TileType.Mine};
            
            Assert.AreEqual(expected.Status, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(expected.Type, resultGrid.Tiles[0,0].Type);
        }
        
        [Test]
        public void ShouldReadEmptyTiles_FromJsonCornersFileCorrectly()
        {
            var resultGrid = new JsonGridSetup( _currentPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            var expected = new Tile{Type = TileType.Empty};
            
            Assert.AreEqual(expected.Status, resultGrid.Tiles[1,1].Status);
            Assert.AreEqual(expected.Type, resultGrid.Tiles[1,1].Type);
        }
        
        [Test]
        public void ShouldSetAllTileStatuses_ToHidden()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "FourCornerMines_LargeGrid.json").CreateGrid();

            foreach (var tile in resultGrid.Tiles)
            {
                Assert.AreEqual(TileStatus.Hidden, tile.Status);
            }
        }
    }
}