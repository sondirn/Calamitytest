using MonoGame.Extended;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public sealed class CollisionSystem : System
    {
        private static CollisionSystem instance = null;
        private static readonly object padlock = new object();
        private static QuadTree quadTree;
        private static List<Physics> actorPhysics;
        private static List<Physics> returnedPhysics;
        private static List<Physics> nonWalls;
     

        public static CollisionSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new CollisionSystem();
                    }
                    return instance;
                }
            }
        }


        public CollisionSystem()
        {
            actorPhysics = new List<Physics>();
            nonWalls = new List<Physics>();
            quadTree = new QuadTree(0, new RectangleF(0, 0, 10000, 10000));
        }

        internal void Update(GameTime gameTime)
        {
#if false
            quadTree.Clear();
                foreach (Physics physics in actorPhysics)
                {
                    physics.Data.Bounds = new RectangleF(physics.Data.Position.X + physics.Data.BoundsX, physics.Data.Position.Y + physics.Data.BoundsY, physics.Data.Width, physics.Data.Height);
                    quadTree.Insert(physics);

                }
            returnedPhysics = new List<Physics>();
            foreach(Physics actorp in nonWalls)
                {
                
                if (Vector2.Distance(actorp.Data.Position, CameraSystem.Instance.Camera.Position) < 1000 && !ActorRenderSystem.Instance.InDrawList(actorp.Owner))
                {
                    ActorRenderSystem.Instance.AddToDrawList(actorp.Owner);
                }
                else if (Vector2.Distance(actorp.Data.Position, CameraSystem.Instance.Camera.Position) > 1000 && ActorRenderSystem.Instance.InDrawList(actorp.Owner))
                {
                    ActorRenderSystem.Instance.RemoveDrawList(actorp.Owner);
                }
                returnedPhysics.Clear();
                    quadTree.Retrieve(returnedPhysics, actorp);
                    
                foreach (Physics actor in returnedPhysics)
                {
                    if(actor.Owner != actorp.Owner)
                    {
                        if (Utils.RectIntersect(actorp.Data.Bounds, actor.Data.Bounds))
                        {
                            actorp.Data.Position = actorp.Data.PrevPosition;
                        }
                    }
                
                }
            }
#endif
        }

        internal void AddActor(Actor actor)
        {
            
            actorPhysics.Add(actor.GetComponent<Physics>());
            if(actor.GetComponent<StateComponent>().Data.State != State.WALL)
            {
                nonWalls.Add(actor.GetComponent<Physics>());
            }
            
        }

        

        internal int GetCount()
        {
            return actorPhysics.Count;
        }

        internal List<Physics> GetActors()
        {
            return actorPhysics;
        }

    }
}
