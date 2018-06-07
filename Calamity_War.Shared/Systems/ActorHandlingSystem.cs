using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Calamity_War
{
    public sealed class ActorHandlingSystem : System
    {
        private static ActorHandlingSystem instance = null;
        private static readonly object padlock = new object();
        public static Queue<int> OutPhaseList;
        public static bool HasOutPhase;
        

        public static ActorHandlingSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new ActorHandlingSystem();
                    }
                    return instance;
                }
            }
        }

        public ActorHandlingSystem()
        {
            Actors = new ActorPool();
            OutPhaseList = new Queue<int>();
        }


        internal void Update(GameTime gameTime)
        {
            
        }

        internal void AddActor(Actor actor)
        {
            if(OutPhaseList.Count == 0)
            {
                actor.Id = Actors.GetCount();
                foreach(Component component in actor.components)
                {
                    component.Owner = actor;
                }
                Actors.AddActor(actor);
                SortActor(actor);
            }
            if (OutPhaseList.Count > 0)
            {
                var tempID = OutPhaseList.Dequeue();
                actor.Id = tempID;
                Actors.actors[tempID] = actor;
                foreach (Component component in actor.components)
                {
                    component.Owner = actor;
                }
                SortActor(actor);
                
            }
            
        }

        

        internal void SortActor(Actor actor)
        {
            
            if (actor.ComponentExists<Physics>() && actor.ComponentExists<StateComponent>())
            {
                CollisionSystem.Instance.AddActor(actor);
            }
            if (actor.ComponentExists<Physics>() && actor.ComponentExists<Sprite>() && actor.ComponentExists<StateComponent>())
            {
                ActorRenderSystem.Instance.AddActor(actor);
            }
            if (actor.ComponentExists<PlayerController>())
            {
                ActorPlayerControlSystem.Instance.AddActor(actor);
            }
            if (actor.ComponentExists<Camera>())
            {
                CameraSystem.Instance.SetActor(actor);
                CameraSystem.Instance.SetFollow(GetActor(0));
                
            }
            
            
            
        }

        internal void AddActorOutPhase(Actor actor)
        {
            
            OutPhaseList.Enqueue(actor.Id);
        }

        internal Actor GetActor(int Id)
        {
            return Actors.actors[Id];
        }

        internal void RemoveFromSystems(int Id)
        {
            ActorRenderSystem.Instance.RemoveActor(Id);
            ActorPlayerControlSystem.Instance.RemoveActor(Id);

        }

        internal IDictionary<int, Actor> GetActorList()
        {
            return Actors.actors;
        }

        internal Queue<int> GetOutPhaseList()
        {
            return OutPhaseList;
        }

        internal int GetActorCount()
        {
            return Actors.actors.Count;
        }
    }
}
