using System;
using System.IO;
using Application.Behaviour.Setup;
using Domain;
using Domain.Entities;
using Domain.Enums;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Component_Tests
{
    public class MinesweeperRulesTests
    {
        private const string TestFolderPath = "/Minesweeper Tests/Fakes/Grids/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        [SetUp]
        public void Setup()
        {
            _currentPath = _currentPath.Substring(0, _currentPath.IndexOf("/Minesweeper/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
        }

        [Test]
        public void ShouldShowSingleTile_WithOneNeighbouringMine()
        {
            var grid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords{X = 1, Y = 0}};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[0,1].Status);
        }
        
        [Test]
        public void ShouldShowSingleTile_WithTwoNeighbouringMines()
        {
            var grid = new JsonGridSetup(_currentPath + "FourSquareMines_SmallGrid.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords{X = 2, Y = 0}};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[0,2].Status);
        }
        
        [Test]
        public void ShouldShowAllEmptyTiles_IfZeroNeighbouringMines()
        {
            var grid = new JsonGridSetup(_currentPath + "FourSquareMines_LargeGrid.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords{X = 3, Y = 3}};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,3].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[2,3].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[1,3].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[4,3].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[5,3].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,4].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,2].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,5].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,1].Status);
        }
        
        [Test]
        public void ShouldShowAllEmptyTilesAndNumberedTiles_OnDiagonallyMinedGrid()
        {
            var grid = new JsonGridSetup(_currentPath + "DiagonalMines.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords{X = 1, Y = 0}};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.Tiles[1,5].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[0,1].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[1,3].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[2,2].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,0].Status);
        }
        
        [Test]
        public void ShouldShowAllEmptyTilesAndNumberedTiles_OnGridWithFourMines()
        {
            var grid = new JsonGridSetup(_currentPath + "FourSquareMines_SmallGrid.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords{X = 5, Y = 0}};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.Tiles[3,3].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[0,5].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[4,0].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,4].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[4,1].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[5,0].Status);
        }
        
        [Test]
        public void ShouldShowAllEmptyTilesAndNumberedTiles_OnGridWithFiveMines()
        {
            var grid = new JsonGridSetup(_currentPath + "FiveMines_LargeGrid.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords{X = 0, Y = 3}};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,0].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[2,0].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[2,1].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[2,2].Status);
        }

    }
}