using Application.GameBehaviour;
using Domain.Enums;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class ControllerBehaviourTests
    {
        private GameController _gameController;
        
        private static readonly object[] BoundaryValuesForInvalidGridSetupParams =
        {
            new []{-9, 2, 10}, //case 1
            new []{9, -2, 10}, //case 2
            new []{-10, -4, 10}, //case 3
            new []{9, -2, 10}, //case 3
            new []{0, 3, 10}, //case 4
            new []{2, 0, 10}, //case 5
            new []{5, 2, 0}, //case 6
            new []{9, 2, -10}, //case 7
            new []{0, 0, -0}, //case 8
        };
        
        [SetUp]
        public void SetUpController()
        {
            _gameController = new GameController();
        }
        
        [Test]
        public void ShouldNotThrowError_IfSetupGridPathIsInvalid() //TODO: setup boundary values for this and for incorrect files
        {
            Assert.DoesNotThrow(() => _gameController.SetupRandomGameFromJson(""));
        }
        
        [Test]
        public void ShouldReturnErrorGameStatus_IfSetupGridPathIsInvalid() 
        {
            var resultGameState = _gameController.SetupRandomGameFromJson("");
            
            Assert.AreEqual(GameStatus.Error, resultGameState.GameStatus);
        }
                
        [Test]
        public void ShouldReturnFirstTurnGameStatus_WhenGridIsSetup() 
        {
            var resultGameState = _gameController.SetupRandomGameFromJson("SetupBehaviours/RandomGridSettings.json");
            
            Assert.AreEqual(GameStatus.FirstTurn, resultGameState.GameStatus);
        }
        
        [TestCaseSource(nameof(BoundaryValuesForInvalidGridSetupParams))]
        public void ShouldNotThrowError_IfGridDimensionsAreInvalid(int[] randomGridSetupParams) 
        {
            Assert.DoesNotThrow(() => _gameController.SetupRandomGrid(randomGridSetupParams[0], randomGridSetupParams[1], randomGridSetupParams[2]));
        }
        
        [TestCaseSource(nameof(BoundaryValuesForInvalidGridSetupParams))]
        public void ShouldReturnErrorState_IfGridDimensionsAreInvalid(int[] randomGridSetupParams) 
        {
            var resultGameState = _gameController.SetupRandomGrid(randomGridSetupParams[0], randomGridSetupParams[1], randomGridSetupParams[2]);
            
            Assert.AreEqual(GameStatus.Error, resultGameState.GameStatus);
        }
        
        [Test]
        public void ShouldReturnGrid_WithCorrectGameState_IfGridSetupParamsAreValid() 
        {
            var resultGameState = _gameController.SetupRandomGrid(10,10,10);
            
            Assert.AreEqual(GameStatus.FirstTurn, resultGameState.GameStatus);
        }
        
        [Test]
        public void ShouldReturnGrid_WithCorrectGridDimensions_IfGridSetupParamsAreValid() 
        {
            var resultGameState = _gameController.SetupRandomGrid(5,10,10);
            
            Assert.AreEqual(5, resultGameState.Grid.Width);
            Assert.AreEqual(10, resultGameState.Grid.Height);
        }
    }
}