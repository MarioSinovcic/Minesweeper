using Domain.Values.Interfaces;

namespace Application.SetupBehaviours.Interfaces
{
    public interface IGridSetupFactory
    {
        public IGrid CreateGrid();
    }
}