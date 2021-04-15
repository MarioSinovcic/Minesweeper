using Domain.Interfaces;

namespace Application.SetupBehaviours.Interfaces
{
    public interface IGridSetupFactory
    {
        public IGrid CreateGrid();
    }
}