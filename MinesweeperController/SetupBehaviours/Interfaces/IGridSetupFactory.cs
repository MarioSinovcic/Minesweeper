using MinesweeperService.Values.Interfaces;

namespace MinesweeperController.SetupBehaviours.Interfaces
{
    internal interface IGridSetupFactory
    {
        public IGrid CreateGrid();
    }
}