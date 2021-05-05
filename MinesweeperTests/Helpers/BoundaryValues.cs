using MinesweeperService.Values;

namespace MinesweeperTests.Helpers
{
    public static class BoundaryValues
    {
        internal static readonly object[] InvalidGridFilePaths =
        {
            "",
            "/",
            "o/a",
            "6",
            null
        };
        
        internal static readonly object[] InvalidGridSetupParams =
        {
            new[] {-9, 2, 10}, 
            new[] {9, -2, 10}, 
            new[] {-10, -4, 10}, 
            new[] {0, 3, 10}, 
            new[] {2, 0, 10}, 
            new[] {5, 2, 0}, 
            new[] {9, 2, -10}, 
            new[] {0, 0, -0}, 
            new[] {9, -2, 10} 
        };

        internal static readonly object[] InputCoords =
        {
            new Coords(-4,2), 
            new Coords(-9,-1), 
            new Coords(-2,500), 
            new Coords(30,-2), 
            new Coords(4,245), 
            new Coords(52,1),
        };
        
        internal static readonly object[] ValidGridSetupParameters =
        {
            new[] {9, 2, 3}, 
            new[] {2, 2, 2}, 
            new[] {1, 4, 2}, 
            new[] {99, 4, 10}, 
            new[] {1, 1, 10000}, 
            new[] {1, 1, 1}, 
            new[] {50, 1, 80}, 
            new[] {99, 99, 10}, 
        };
    }
}