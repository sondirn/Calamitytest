using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{ 
    public struct Tile
    {
        public TileData Data;

        public Tile(TileData Data)
        {
            this.Data = Data;
        }
    }
}
