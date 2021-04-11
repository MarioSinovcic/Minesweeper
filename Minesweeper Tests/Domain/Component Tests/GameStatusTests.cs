using System;
using System.IO;
using Application.Behaviour.Setup;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Minesweeper_Tests.Stubs;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Component_Tests
{
    public class GameStatusTests
    {
        private const string TestFolderPath = "/Minesweeper Tests/Grid Fakes/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        private static readonly object[] BoundaryValuesForInputCoords =
        {
            new Coords{X = -4, Y = 2}, //case 1
            new Coords{X = -9, Y = -1}, //case 2
            new Coords{X = -2, Y = 500}, //case 3
            new Coords{X = 30, Y = -2}, //case 4
            new Coords{X = 4, Y = 245}, //case 5
            new Coords{X = 52, Y = 1}, //case 6
        };

        [SetUp]
        public void Setup()
        {
            _currentPath = _currentPath.Substring(0, _currentPath.IndexOf("/Minesweeper/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
        }

        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void ShouldReturnErrorState_IfInputIsOutOfBounds(Coords boundaryValuesForInputCoords)
        {
            var grid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, boundaryValuesForInputCoords);

            Assert.AreEqual(GameStatus.Error, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnLossState_IfMineSelected()
        {
            var grid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords{X = 0, Y = 0});

            Assert.AreEqual(GameStatus.Loss, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnWinState_IfAllEmptyTilesAreShown()
        {
            var grid = new WinningGridStub(null);
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords{X = 1, Y = 1});
            
            Assert.AreEqual(GameStatus.Win, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnLossEven_IfAllEmptyTilesAreShown()
        {
            var grid = new WinningGridStub(null);
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords{X = 0, Y = 0});
            
            Assert.AreEqual(GameStatus.Loss, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnPlaying_IfGameIsNotFinished()
        {
            var grid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords{X = 1, Y = 1});

            Assert.AreEqual(GameStatus.Playing, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnPlaying_IfGameIsNotFinishedInTheLateGame()
        {
            var grid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            grid.Tiles[0, 2] = grid.Tiles[0, 2].ShowTile();
            grid.Tiles[0, 4] = grid.Tiles[0, 4].ShowTile();
            grid.Tiles[1, 1] = grid.Tiles[1, 1].ShowTile();
            grid.Tiles[1, 2] = grid.Tiles[1, 2].ShowTile();
            grid.Tiles[2, 2] = grid.Tiles[2, 2].ShowTile();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords{X = 1, Y = 1});

            Assert.AreEqual(GameStatus.Playing, resultGameStatus);
        }
    }
}