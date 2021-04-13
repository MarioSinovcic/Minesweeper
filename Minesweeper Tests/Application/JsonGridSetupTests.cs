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
        private const string  TestFolderPath = "/Minesweeper Tests/Fakes/Grids/";  //TODO: refactor, copy to output directory
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
            var resultGrid = new JsonGridSetup("Fakes/Grids/OneCornerMine.json").CreateGrid();
            
            Assert.AreEqual(7, resultGrid.Width);
            Assert.AreEqual(3, resultGrid.Height);
        }

        [Test]
        public void ShouldReadMines_FromJsonCornersFileCorrectly()
        {
            var resultGrid = new JsonGridSetup(_currentPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            var mineTile = new Tile(TileType.Mine);
            
            Assert.AreEqual(mineTile.Status, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(mineTile.Type, resultGrid.Tiles[0,0].Type);
        }
        
        [Test]
        public void ShouldReadEmptyTiles_FromJsonCornersFileCorrectly()
        {
            var resultGrid = new JsonGridSetup( _currentPath + "FourCornerMines_SmallGrid.json").CreateGrid();
            Assert.AreEqual(new Tile(TileType.Empty), resultGrid.Tiles[1,1]);
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