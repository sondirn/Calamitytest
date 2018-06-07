using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public sealed class EnemyAISystem : System
    {
        private static EnemyAISystem instance = null;
        private static readonly object padlock = new object();
        private static List<Actor> actors;

        public static EnemyAISystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new EnemyAISystem();
                    }
                    return instance;
                }
            }
        }

        public EnemyAISystem()
        {
            actors = new List<Actor>();
        }

        internal void Update(GameTime gameTime)
        {
            
        }


        internal void AddActor(Actor actor)
        {
            actors.Add(actor);
        }
    }
}
