
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public class Camera : Component
    {
        public CameraData Data;

        public Camera(CameraData Data, string Name = "Camera") : base(Name)
        {
            this.Data = Data;
        }
    }
}
