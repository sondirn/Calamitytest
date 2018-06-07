using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public sealed class SystemHolder
    {
        private static SystemHolder instance = null;
        private static readonly object padlock = new object();
        internal IDictionary<string, System> Systems;

        public static SystemHolder Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new SystemHolder();
                    }
                    return instance;
                }
            }
        }

        public SystemHolder()
        {
            Systems = new Dictionary<string, System>()
            {
                {"CameraSystem" , new CameraSystem()},
                {"ActorHandlingSystem", new ActorHandlingSystem() },
                {"ActorRenderSystem", new ActorRenderSystem() },
                {"ActorGenerationSystem", new ActorGenerationSystem() },
                {"ActorPlayerControlSystem", new ActorPlayerControlSystem() },
                {"CollisionSystem", new CollisionSystem() },
                {"EnemyAISystem", new EnemyAISystem() },
                {"TileRenderSystem", new TileRenderSystem() }
                
            };
        }

        public void Initialize()
        {
            CameraSystem.Instance.Initialize();
        }

        public void LoadContent()
        {
            ActorRenderSystem.Instance.LoadContent();
            TileRenderSystem.Instance.LoadContent();
        }

        public void UnloadContent()
        {
            
        }

        public void Update(GameTime gameTime)
        {
            ActorHandlingSystem.Instance.Update(gameTime);
            ActorPlayerControlSystem.Instance.Update(gameTime);
            ActorRenderSystem.Instance.Update(gameTime);
            CollisionSystem.Instance.Update(gameTime);
            CameraSystem.Instance.Update(gameTime);
            EnemyAISystem.Instance.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            TileRenderSystem.Instance.Draw(spriteBatch, gameTime);
            ActorRenderSystem.Instance.Draw(spriteBatch, gameTime);
        }

        public void DrawGUI(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
    }
}
