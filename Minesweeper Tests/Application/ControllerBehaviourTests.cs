using System.IO;
using Application.GameBehaviour;
using NUnit.Framework;

namespace Minesweeper_Tests.Application
{
    public class ControllerBehaviourTests
    {
        private GameController _gameController;
        
        [SetUp]
        public void SetUpController()
        {
            _gameController = new GameController();
        }
        
        [Test]
        public void ShouldThrowError_IfSetupGridPathIsInvalid() //TODO: setup boundary values for this and for incorrect files
        {
            Assert.Throws<IOException>(() => _gameController.SetupRandomGameFromJson(""));
        }
    }
}