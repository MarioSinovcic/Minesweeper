using Minesweeper_Service.Values.Interfaces;

namespace Minesweeper_Controller.SetupBehaviours.Interfaces
{
    public interface IGridSetupFactory
    {
        public IGrid CreateGrid();
    }
}