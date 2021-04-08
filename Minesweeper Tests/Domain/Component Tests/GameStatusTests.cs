using System;
using System.IO;
using Application.Behaviour.Setup;
using Domain;
using Domain.Enums;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Component_Tests
{
    public class GameStatusTests
    {
        private const string TestFolderPath = "/Minesweeper Tests/Grid Fakes/";
        private static string _currentPath = Directory.GetCurrentDirectory();

        private static readonly object[] BoundaryValuesForInputCoords =
        {
            new[] {-9, 2}, //case 1
            new[] {-9, -1}, //case 2
            new[] {2, -500}, //case 3
            new[] {3, 50}, //case 4
            new[] {52, 1}, //case 5
        };

        [SetUp]
        public void Setup()
        {
            _currentPath =
                _currentPath.Substring(0, _currentPath.IndexOf("/Minesweeper/", StringComparison.Ordinal) + 13);
            _currentPath += TestFolderPath;
        }

        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void ShowReturnErrorStateIfInputIsOutOfBounds(int[] boundaryValuesForInputCoords)
        {
            var grid = new JsonGridSetup(_currentPath + "OneCornerMine.json").CreateGrid();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, boundaryValuesForInputCoords);

            Assert.AreEqual(GameStatus.Error, resultGameStatus);
        }
    }
}