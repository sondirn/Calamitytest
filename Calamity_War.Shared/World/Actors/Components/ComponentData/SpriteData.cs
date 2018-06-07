using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public class SpriteData
    {
        public Color Color { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Animated { get; set; }
        public int Frames { get; set; }
        public int CurrentFrame { get; set; }
        public int Y_Frame { get; set; }
        public bool Animating { get; set; }
        public byte FrameTimer { get; set; }
        public byte FrameSpeed { get; set; }
        public string TexturePath { get; set; }
        public Vector2 Origin { get; set; }
        public float OriginX { get; set; }
        public float OriginY { get; set; }

        public SpriteData()
        {
            Origin = new Vector2(OriginX, OriginY);
        }
    }
}
