using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public struct TileData
    {
        public Vector2 Position { get; set; }
        public string SpriteSheet { get; set; }
        public Rectangle SpriteSheetRect { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int xFrame { get; set; }
        public int yFrame { get; set; }
    }
}
