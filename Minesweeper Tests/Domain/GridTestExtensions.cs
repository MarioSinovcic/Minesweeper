using System.Collections.Generic;
using Domain.Interfaces;
using Domain.Values;

namespace Minesweeper_Tests.Domain
{
    public static class GridTestExtensions
    {
        public static List<Tile> LoopThroughGrid(IGrid grid)
        {
            var tiles = new List<Tile>();
            
            for(var i =0; i<grid.Height;i++){
                for(var j =0; j<grid.Width;j++)
                {
                    var tileType = grid.GetTileTypeAt(new Coords(j, i));
                    var tileStatus = grid.GetTileStatusAt(new Coords(j, i));
                    
                    tiles.Add(new Tile(tileType, tileStatus));
                }
            }
            return tiles;
        }
    }
}