using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public sealed class ActorGenerationSystem : System
    {
        private static ActorGenerationSystem instance = null;
        private static readonly object padlock = new object();

        public static ActorGenerationSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new ActorGenerationSystem();
                    }
                    return instance;
                }
            }
        }

        public ActorGenerationSystem()
        {

        }

        internal void CreateActor(string name, params Component[] components)
        {
            var actor = new Actor(0, name);
            for(int i = 0; i < components.Length; i++)
            {
                components[i].Owner = actor;
                actor.AddComponent(components[i]);
            }
            ActorHandlingSystem.Instance.AddActor(actor);
        }

        internal void CreateActors(string name, int actorCount, params Component[] components)
        {
            for(int i = 0; i < actorCount; i++)
            {
                var actor = new Actor(0, name);
                for(int x = 0; x < components.Length; x++)
                {
                    actor.AddComponent(components[x]);
                }
                ActorHandlingSystem.Instance.AddActor(actor);
            }
        }

        
        

    }
}
