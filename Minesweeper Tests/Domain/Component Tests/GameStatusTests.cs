using Application.Behaviour.Setup;
using Domain;
using Domain.Enums;
using Domain.Values;
using Minesweeper_Tests.Stubs;
using NUnit.Framework;

namespace Minesweeper_Tests.Domain.Component_Tests
{
    public class GameStatusTests
    {
        private const string TestFolderPath = "Fakes/Grids/";

        private static readonly object[] BoundaryValuesForInputCoords =
        {
            new Coords(-4,2), //case 1
            new Coords(-9,-1), //case 2
            new Coords(-2,500), //case 3
            new Coords(30,-2), //case 4
            new Coords(4,245), //case 5
            new Coords(52,1), //case 6
        };

        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void ShouldReturnErrorState_IfInputIsOutOfBounds(Coords boundaryValuesForInputCoords)
        {
            var grid = new JsonGridSetup(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, boundaryValuesForInputCoords);

            Assert.AreEqual(GameStatus.Error, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnLossState_IfMineSelected()
        {
            var grid = new JsonGridSetup(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords(0,0));

            Assert.AreEqual(GameStatus.Loss, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnWinState_IfAllEmptyTilesAreShown()
        {
            var grid = new WinningGridStub();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords(1,1));
            
            Assert.AreEqual(GameStatus.Win, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnLossEven_IfAllEmptyTilesAreShown()
        {
            var grid = new WinningGridStub();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords(0,0));
            
            Assert.AreEqual(GameStatus.Loss, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnPlaying_IfGameIsNotFinished()
        {
            var grid = new JsonGridSetup(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords(1,1));

            Assert.AreEqual(GameStatus.Playing, resultGameStatus);
        }
        
        [Test]
        public void ShouldReturnPlaying_IfGameIsNotFinishedInTheLateGame()
        {
            var grid = new JsonGridSetup(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var coords = new Coords(2, 0);
            var updatedTile =  grid.ShowHiddenTile(coords);
            grid.ReplaceTile(coords, updatedTile); //grid\.Tiles\[([0-9])\, ([0-9])\]\.ShowTile\(\)
            var resultGameStatus = RuleEvaluator.EvaluateGameStatus(grid, new Coords(1,1));

            Assert.AreEqual(GameStatus.Playing, resultGameStatus);
        }
    }
}