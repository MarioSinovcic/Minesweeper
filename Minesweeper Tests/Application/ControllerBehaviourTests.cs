using Application.GameBehaviour;
using Application.GameBehaviour.DTOs;
using Application.SetupBehaviours.Factories;
using Domain.Enums;
using Domain.Values;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class ControllerBehaviourTests
    {
        private const string TestFolderPath = "Fakes/Grids/";

        private static readonly object[] BoundaryValuesForInvalidGridSetupParams =
        {
            new[] {-9, 2, 10}, //case 1
            new[] {9, -2, 10}, //case 2
            new[] {-10, -4, 10}, //case 3
            new[] {0, 3, 10}, //case 4
            new[] {2, 0, 10}, //case 5
            new[] {5, 2, 0}, //case 6
            new[] {9, 2, -10}, //case 7
            new[] {0, 0, -0}, //case 8
            new[] {9, -2, 10} //case 9
        };
            
        private static readonly object[] BoundaryValuesForInputCoords =
        {
            new Coords(-4,2), //case 1
            new Coords(-9,-1), //case 2
            new Coords(-2,500), //case 3
            new Coords(30,-2), //case 4
            new Coords(4,245), //case 5
            new Coords(52,1), //case 6
        };
        
        private GameController _gameController;

        
        [SetUp]
        public void SetUpController()
        {
            _gameController = new GameController();
        }
        
        [Test]
        public void ShouldNotThrowError_IfSetupGridPathIsInvalid()
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
        
        [Test]
        public void ShouldPerformMoveOnGrid_WithPlayableGameState()
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 1);
            var inputDTO = new InputDTO(moveStatusType,  moveCoords);
            var previousGameState = new GameState(moveStatusType, grid, moveCoords);

            var resultGameState = _gameController.HandleMove(inputDTO, previousGameState);
            
            Assert.AreEqual(GameStatus.Playing, resultGameState.GameStatus);
        }     
        
        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void ShouldNotThrowError_WhenPerformingAMove_WithInvalidCoords(Coords invalidCoords)  
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "DiagonalMines.json").CreateGrid();
            var moveStatusType = GameStatus.Playing;
            var inputDTO = new InputDTO(moveStatusType,  invalidCoords);
            var previousGameState = new GameState(moveStatusType, grid, invalidCoords);

            Assert.DoesNotThrow(() => _gameController.HandleMove(inputDTO, previousGameState));
        }       
        
        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void ShouldReturnErrorState_WhenPerformingAMove_WithInvalidCoords(Coords invalidCoords)  
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "DiagonalMines.json").CreateGrid();
            var moveStatusType = GameStatus.Playing;
            var inputDTO = new InputDTO(moveStatusType,  invalidCoords);
            var previousGameState = new GameState(moveStatusType, grid, invalidCoords);

            var resultGameState = _gameController.HandleMove(inputDTO, previousGameState);
            
            Assert.AreEqual(GameStatus.Error, resultGameState.GameStatus);
        }     
        
        [Test]
        public void ShouldReturnErrorState_WhenPerformingAMove_WithAnErrorState()  
        {
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var moveStatusType = GameStatus.Error;
            var moveCoords = new Coords(3, 3);
            var inputDTO = new InputDTO(moveStatusType,  moveCoords);
            var previousGameState = new GameState(moveStatusType, grid, moveCoords);

            var resultGameState = _gameController.HandleMove(inputDTO, previousGameState);
            
            Assert.AreEqual(GameStatus.Error, resultGameState.GameStatus);
        }     
        
        
    }
}