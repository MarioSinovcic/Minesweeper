using Minesweeper_Service.Enums;
using Minesweeper_Service.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Service.Component_Tests
{
    public class FlagBehaviourTests
    {
        private const string TestFolderPath = "Fakes/Grids/";

        [Test] 
        public void ShouldSetFlag_AtCorrectCoordinates()
        {
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(1, 0);
            
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            Assert.AreEqual(TileStatus.Flag, resultState.Grid.GetTileStatusAt(moveCoords));
        }
        
        [Test] 
        public void ShouldSetFlagOnTile_WithMine()
        {
            var testGridPath = TestFolderPath + "DiagonalMines.json";
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(2, 2);

            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);
            
            Assert.AreEqual(TileStatus.Flag, resultState.Grid.GetTileStatusAt(moveCoords));
        }
        
        [Test] 
        public void ShouldShowTileThatHasAFlagOnIt_WithAScoreOfOne()
        {
            var testGridPath = TestFolderPath + "OneCornerMine.json";
            var firstMoveStatusType = GameStatus.SetFlag;
            var secondMoveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(1, 0);
            
            var firstMove = TestExtensions.PerformMove(testGridPath, firstMoveStatusType, moveCoords);
            var resultState = TestExtensions.PerformMove(firstMove.Grid, secondMoveStatusType, moveCoords);

            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
        }
        
        [Test] 
        public void ShouldNotLoseGame_IfFlagPlacedOnMine()
        {
            var testGridPath = TestFolderPath + "DiagonalMines.json";
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(5, 0);
            
            var resultState = TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            Assert.AreEqual(TileStatus.Flag, resultState.Grid.GetTileStatusAt(moveCoords));
            Assert.AreEqual(GameStatus.Playing, resultState.GameStatus);
        }
        
        [Test] 
        public void ShouldWinGame_IfFlagPlacedOnMineAndAllOthersAreMines()
        {
            var testGridPath = TestFolderPath + "AllMines.json";
            var moveStatusType = GameStatus.SetFlag;
            var moveCoords = new Coords(0, 0);
            
            var resultState= TestExtensions.PerformMove(testGridPath, moveStatusType, moveCoords);

            Assert.AreEqual(TileStatus.Flag, resultState.Grid.GetTileStatusAt(moveCoords));
            Assert.AreEqual(GameStatus.Win, resultState.GameStatus);
        }
        
        
        [Test] 
        public void ShouldLoseGame_IfCoordsSetOnFlaggedMine()
        {
            var testGridPath = TestFolderPath + "AllMines.json";
            var firstMoveStatusType = GameStatus.SetFlag;
            var secondMoveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 0);
            
            var firstMove = TestExtensions.PerformMove(testGridPath, firstMoveStatusType, moveCoords);
            var resultState = TestExtensions.PerformMove(firstMove.Grid, secondMoveStatusType, moveCoords);

            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
            Assert.AreEqual(GameStatus.Loss, resultState.GameStatus);
        }
        
        [Test] 
        public void ShouldShowTileThatHasAFlagOnIt_WithAScoreOfZero()
        {
            var testGridPath = TestFolderPath + "ThreeMines_LargeGrid.json";
            var firstMoveStatusType = GameStatus.SetFlag;
            var secondMoveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(6, 6);
            
            var firstMove = TestExtensions.PerformMove(testGridPath, firstMoveStatusType, moveCoords);
            var resultState = TestExtensions.PerformMove(firstMove.Grid, secondMoveStatusType, moveCoords);

            Assert.AreEqual(TileStatus.Shown, resultState.Grid.GetTileStatusAt(moveCoords));
            Assert.AreEqual(GameStatus.Playing, resultState.GameStatus);
        }
    }
}