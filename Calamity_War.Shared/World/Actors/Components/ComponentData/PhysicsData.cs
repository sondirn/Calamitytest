
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public class PhysicsData
    {
        public Vector2 Position { get; set; } 
        public Vector2 PrevPosition { get; set; }
        public bool Solid { get; set; }
        public Vector2 VSpeed
        {
            get
            {
                return new Vector2(0, Speed.Y);
            }
            set
            {
                
            }
            
        }
        public Vector2 Speed { get; set; }
        public Vector2 RealSpeed { get; set; }
        public Vector2 HSpeed
        {
            get
            {
                return new Vector2(Speed.X, 0);
            }
            set
            {
                
            }
        }
        public float Angle { get; set; }
        public Vector2 Acceleration { get; set; }
        public Vector2 Magnitude { get; set; }
        public bool Collidable { get; set; }
        public Vector2 ForwardVector { get; set; }
        public Direction Direction { get; set; }
        public Direction PreviousDirection { get; set; }

        public float BoundsX { get; set; }
        public float BoundsY { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float BoundsXOffSet { get; set; }
        public float BoundsYOffSet { get; set; }
        


        public RectangleF Bounds{ get; set; }
        
        public PhysicsData()
        {
            PrevPosition = Position;
            Bounds = new RectangleF(Position.X + BoundsX, Position.Y + BoundsY, Width, Height);
        }

        
        
        
    }
}
