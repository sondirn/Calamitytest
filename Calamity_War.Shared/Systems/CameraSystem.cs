
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ViewportAdapters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public sealed class CameraSystem : System
    {
        private static CameraSystem instance = null;
        private static readonly object padlock = new object();
        public Matrix TransformationMatrix;
        private Actor actor;
        private Actor previousActor;
        private Physics physics;
        private Physics Followp;
        private Actor follow;
        private Camera camera;
        public Camera2D Camera;
        public RectangleF CameraRect;

        public static CameraSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new CameraSystem();
                    }
                    return instance;

                }
            }
        }

        public CameraSystem()
        {
            
            actor = null;
            CameraRect = new RectangleF(0, 0, 640, 360);
            camera = null;
            physics = null;
            previousActor = null;
            follow = null;
            Followp = null;
        }

        internal void Initialize()
        {
            
            var viewportAdapter = new BoxingViewportAdapter(Calamity.Instance.Window, Calamity.Instance.GraphicsDevice, (int)CameraRect.Width, (int)CameraRect.Height);
            Camera = new Camera2D(viewportAdapter);
            Camera.Position = new Vector2(0, 0);
            if(actor != null)
            {
                physics = actor.GetComponent<Physics>();
                camera = actor.GetComponent<Camera>();
            }
            if(follow != null)
            {
                Followp = follow.GetComponent<Physics>();
            }
            Camera.Zoom = 1f;
        }

        internal void Update(GameTime gameTime)
        {
            
            CameraRect.Position = Camera.Position;
            if (actor != null)
            {
                physics.Data.Position = Followp.Data.Position;

                Camera.LookAt(physics.Data.Position);

            }
            if(Camera.Position.X < 0)
            {
                Camera.Position = new Vector2(0, Camera.Position.Y);
            }
            if (Camera.Position.Y < 0)
            {
                Camera.Position = new Vector2(Camera.Position.X, 0);
            }
            if(Camera.Position.Y > TileRenderSystem.Instance.MapHeight - 360)
            {
                Camera.Position = new Vector2(Camera.Position.X, TileRenderSystem.Instance.MapHeight - 360);
            }
            if (Camera.Position.X > TileRenderSystem.Instance.MapWidth - 640)
            {
                Camera.Position = new Vector2(TileRenderSystem.Instance.MapWidth - 640,Camera.Position.Y);
            }


        }

        internal void Draw()
        {
            TransformationMatrix = Camera.GetViewMatrix();
        }

        internal void SetActor(Actor actor)
        {
            this.actor = actor;
            physics = actor.GetComponent<Physics>();
            camera = actor.GetComponent<Camera>();
        }

        internal void SetFollow(Actor actor)
        {
            follow = actor;
            Followp = actor.GetComponent<Physics>();
        }

        internal Actor GetActor()
        {
            return actor;
        }

        internal Actor GetFollowing()
        {
            return follow;
        }
    }
}
