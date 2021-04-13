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
        private const string TestFolderPath = "Fakes/Grids/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        [Test]
        public void ShouldShowSingleTile_WithOneNeighbouringMine()
        {
            var grid = new JsonGridSetup(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords(1,0)};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[0,1].Status);
        }
        
        [Test]
        public void ShouldShowSingleTile_WithTwoNeighbouringMines()
        {
            var grid = new JsonGridSetup(TestFolderPath + "FourSquareMines_SmallGrid.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords(2,0)};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.Tiles[0,0].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[0,2].Status);
        }
        
        [Test]
        public void ShouldShowAllEmptyTiles_IfZeroNeighbouringMines() //refactor: doesn't test functionality properly, hard to find the point of failure
        {
            var grid = new JsonGridSetup(TestFolderPath + "FourSquareMines_LargeGrid.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords(3,3)};
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
            var grid = new JsonGridSetup(TestFolderPath + "DiagonalMines.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords(1,0)};
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
            var grid = new JsonGridSetup(TestFolderPath + "FourSquareMines_SmallGrid.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords(5,0)};
            var resultGrid = Minesweeper.PerformMove(move).Grid;
            
            Assert.AreEqual(TileStatus.Hidden, resultGrid.Tiles[3,3].Status); //test failure should be comms
        }
        
        [Test]
        public void ShouldShowAllEmptyTilesAndNumberedTiles_OnGridWithFiveMines()
        {
            var grid = new JsonGridSetup(TestFolderPath + "FiveMines_LargeGrid.json").CreateGrid();
            var move = new Move {Grid = grid, Coords = new Coords(0,3)};
            var resultGrid = Minesweeper.PerformMove(move).Grid; //TODO: command query separation, perform move, get grid should be separate
            
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[3,0].Status); //TODO: grid method for getStatusAtCoords & getTypeAtCoords
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[2,0].Status); //Tile record not exposed
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[2,1].Status);
            Assert.AreEqual(TileStatus.Shown, resultGrid.Tiles[2,2].Status);
        }

    }
}