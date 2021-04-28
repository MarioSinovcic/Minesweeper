using MinesweeperService.Values.Interfaces;

namespace MinesweeperController.SetupBehaviours.Interfaces
{
    public interface IGridSetupFactory
    {
        public IGrid CreateGrid();
    }
}