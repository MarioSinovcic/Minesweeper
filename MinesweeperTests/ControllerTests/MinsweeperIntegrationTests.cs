using MinesweeperController.GameBehaviour;
using MinesweeperController.GameBehaviour.DTOs;
using MinesweeperController.SetupBehaviours.Factories;
using MinesweeperService.Enums;
using MinesweeperService.Values;
using MinesweeperTests.Helpers;
using MinesweeperTests.Helpers.Stubs;
using NUnit.Framework;

namespace MinesweeperTests.ControllerTests
{
    public class ControllerBehaviourTests
    {
        private static readonly object[] BoundaryValuesForInvalidGridFilePaths = BoundaryValues.InvalidGridFilePaths;
        private static readonly object[] BoundaryValuesForInvalidGridSetupParams = BoundaryValues.InvalidGridSetupParams;
        private static readonly object[] BoundaryValuesForInputCoords = BoundaryValues.InputCoords;

        private const string TestFolderPath = "Fakes/Grids/";
        private GameController _gameController;

        [SetUp]
        public void SetUpController()
        {
            _gameController = new GameController();
        }
        
        [TestCaseSource(nameof(BoundaryValuesForInvalidGridFilePaths))]
        public void CreationBehaviour_WithIncorrectJsonPath_SuccessfullyDoesNotThrowError(string settingsFilePath)
        {
            //Arrange
            var invalidPath = settingsFilePath;
            
            //Act //Assert
            Assert.DoesNotThrow(() => _gameController.SetupRandomGameFromJson(invalidPath));
        }
        
        [TestCaseSource(nameof(BoundaryValuesForInvalidGridFilePaths))]
        public void CreationBehaviour_WithIncorrectJsonPath_SuccessfullyReturnsErrorState(string settingsFilePath) 
        {
            //Arrange
            var invalidPath = settingsFilePath;
            
            //Act
            var resultGameState = _gameController.SetupRandomGameFromJson(invalidPath);
            
            //Assert
            Assert.AreEqual(GameStatus.Error, resultGameState.GameStatus);
        }
                
        [Test]
        public void CreationBehaviour_WithValidSettingsFile_SuccessfullyReturnsFirstTurnState() 
        {
            //Arrange
            var settingsFilePath = "SetupBehaviours/RandomGridSettings.json";

            //Act
            var resultGameState = _gameController.SetupRandomGameFromJson(settingsFilePath);
            
            //Assert
            Assert.AreEqual(GameStatus.FirstTurn, resultGameState.GameStatus);
        }
        
        [TestCaseSource(nameof(BoundaryValuesForInvalidGridSetupParams))]
        public void CreationBehaviour_WithIncorrectDimensions_SuccessfullyDoesNotThrowError(int[] randomGridSetupParams)
        {
            //Arrange
            var width = randomGridSetupParams[0];
            var height = randomGridSetupParams[1];
            var mineFrequency = randomGridSetupParams[2];
            
            //Act //Assert
            Assert.DoesNotThrow(() => _gameController.SetupRandomGrid(width, height, mineFrequency));
        }
        
        [TestCaseSource(nameof(BoundaryValuesForInvalidGridSetupParams))]
        public void CreationBehaviour_WithIncorrectDimensions_SuccessfullyReturnErrorState(int[] randomGridSetupParams) 
        {
            //Arrange
            var width = randomGridSetupParams[0];
            var height = randomGridSetupParams[1];
            var mineFrequency = randomGridSetupParams[2];
            
            //Act 
            var resultGameState = _gameController.SetupRandomGrid(width, height, mineFrequency);
            
            //Assert
            Assert.AreEqual(GameStatus.Error, resultGameState.GameStatus);
        }
        
        [Test]
        public void CreationBehaviour_WithValidDimensions_SuccessfullyCreateGridWithCorrectState() 
        {
            //Arrange
            var width = 10;
            var height = 10;
            var mineFrequency = 10;
            
            //Act 
            var resultGameState = _gameController.SetupRandomGrid(width, height, mineFrequency);
            
            //Assert
            Assert.AreEqual(GameStatus.FirstTurn, resultGameState.GameStatus);
        }
        
        [Test]
        public void CreationBehaviour_WithValidDimensions_SuccessfullyCreateGridWithCorrectDimensions() 
        {
            //Arrange
            var width = 5;
            var height = 10;
            var mineFrequency = 10;
            
            //Act 
            var resultGameState = _gameController.SetupRandomGrid(width, height, mineFrequency);
            
            //Assert
            Assert.AreEqual(5, resultGameState.Grid.Width);
            Assert.AreEqual(10, resultGameState.Grid.Height);
        }
        
        [Test]
        public void MoveBehaviour_WithOneCornerMine_SuccessfullyPerformValidMove()
        {
            //Arrange
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 1);
            var inputDTO = new InputDTO(moveStatusType,  moveCoords);
            var previousGameState = new GameState(moveStatusType, grid, moveCoords);

            //Act
            var resultGameState = _gameController.HandleMove(inputDTO, previousGameState);
            
            //Assert
            Assert.AreEqual(GameStatus.Playing, resultGameState.GameStatus);
        }     
        
        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void MoveBehaviour_WithInvalidCoordinates_SuccessfullyDoesNotThrowError(Coords invalidCoords)  
        {
            //Arrange
            var grid = new JsonGridSetupFactory(TestFolderPath + "DiagonalMines.json").CreateGrid();
            var moveStatusType = GameStatus.Playing;
            var inputDTO = new InputDTO(moveStatusType,  invalidCoords);
            var previousGameState = new GameState(moveStatusType, grid, invalidCoords);

            //Act //Assert
            Assert.DoesNotThrow(() => _gameController.HandleMove(inputDTO, previousGameState));
        }       
        
        [TestCaseSource(nameof(BoundaryValuesForInputCoords))]
        public void MoveBehaviour_WithInvalidCoordinates_SuccessfullyReturnsErrorState(Coords invalidCoords)  
        {
            //Arrange
            var grid = new JsonGridSetupFactory(TestFolderPath + "DiagonalMines.json").CreateGrid();
            var moveStatusType = GameStatus.Playing;
            var inputDTO = new InputDTO(moveStatusType,  invalidCoords);
            var previousGameState = new GameState(moveStatusType, grid, invalidCoords);

            //Act
            var resultGameState = _gameController.HandleMove(inputDTO, previousGameState);
            
            //Assert
            Assert.AreEqual(GameStatus.Error, resultGameState.GameStatus);
        }     
        
        [Test]
        public void MoveBehaviour_WithErrorState_SuccessfullyReturnsNewErrorStateAfterMove()  
        {
            //Arrange
            var grid = new JsonGridSetupFactory(TestFolderPath + "OneCornerMine.json").CreateGrid();
            var moveStatusType = GameStatus.Error;
            var moveCoords = new Coords(3, 3);
            var inputDTO = new InputDTO(moveStatusType,  moveCoords);
            var previousGameState = new GameState(moveStatusType, grid, moveCoords);

            //Act
            var resultGameState = _gameController.HandleMove(inputDTO, previousGameState);
            
            //Assert
            Assert.AreEqual(GameStatus.Error, resultGameState.GameStatus);
        }     
        
        [Test]
        public void MoveBehaviour_WithAllTilesRevealed_SuccessfullyReturnsNewWinAfterMove()  
        {
            //Arrange
            var grid = new GridWithAllTilesRevealedStub();
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(0, 0);
            var inputDTO = new InputDTO(moveStatusType,  moveCoords);
            var previousGameState = new GameState(moveStatusType, grid, moveCoords);

            //Act
            var resultGameState = _gameController.HandleMove(inputDTO, previousGameState);
            
            //Assert
            Assert.AreEqual(GameStatus.Win, resultGameState.GameStatus);
        }    
        
        [Test]
        public void MoveBehaviour_WithAllTilesRevealed_SuccessfullyCascadesEmptyTiles()  
        {
            //Arrange
            var grid = new EmptyGridWithLeftTileRevealedStub();
            var moveStatusType = GameStatus.Playing;
            var moveCoords = new Coords(2, 0);
            var inputDTO = new InputDTO(moveStatusType,  moveCoords);
            var previousGameState = new GameState(moveStatusType, grid, moveCoords);

            //Act
            var resultGrid = _gameController.HandleMove(inputDTO, previousGameState).Grid;
            
            //Assert
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(0,0)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(1,0)));
            Assert.AreEqual(TileStatus.Shown, resultGrid.GetTileStatusAt(new Coords(2,0)));
        }     
    }
}