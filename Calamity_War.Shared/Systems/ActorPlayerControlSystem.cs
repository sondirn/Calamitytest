using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Calamity_War
{
    public sealed class ActorPlayerControlSystem : System
    {
        private static ActorPlayerControlSystem instance = null;
        private static readonly object padlock = new object();
        public static List<Actor> PlayerControlledList;
        

        public static ActorPlayerControlSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new ActorPlayerControlSystem();
                    }
                    return instance;
                }
            }
        }

        public ActorPlayerControlSystem()
        {
            PlayerControlledList = new List<Actor>();
        }

        internal void AddActor(Actor actor)
        {
            if (!PlayerControlledList.Contains(actor))
            {
                PlayerControlledList.Add(actor);
            }
        }

        internal void Update(GameTime gameTime)
        {
            
             
            foreach(Actor actor in PlayerControlledList)
            {
                
                Physics physics = actor.GetComponent<Physics>();
                Sprite sprite = actor.GetComponent<Sprite>();
                StateComponent state = actor.GetComponent<StateComponent>();

                physics.Data.PrevPosition = physics.Data.Position;

                var tempV = Vector2.Zero;
                var tempH = Vector2.Zero;

                if (InputManager.Instance.Pressed(Input.MOVE_UP) && state.Data.State != State.RUNNING)
                {
                    tempV = -physics.Data.VSpeed * Calamity.Instance.DeltaVector2;
                    physics.Data.Direction = Direction.UP;
                    sprite.Data.Y_Frame = (int)physics.Data.Direction;
                    state.Data.State = State.WALKING;
                }
                
                
                if (InputManager.Instance.Pressed(Input.MOVE_DOWN) && state.Data.State != State.RUNNING)
                {
                    tempV = physics.Data.VSpeed * Calamity.Instance.DeltaVector2;
                    physics.Data.Direction = Direction.DOWN;
                    sprite.Data.Y_Frame = (int)physics.Data.Direction;
                    state.Data.State = State.WALKING;
                }
                
                if (InputManager.Instance.Pressed(Input.MOVE_RIGHT) && state.Data.State != State.RUNNING)
                {
                    tempH = physics.Data.HSpeed * Calamity.Instance.DeltaVector2;
                    physics.Data.Direction = Direction.RIGHT;
                    sprite.Data.Y_Frame = (int)physics.Data.Direction;
                    state.Data.State = State.WALKING;
                }
                
                if (InputManager.Instance.Pressed(Input.MOVE_LEFT) && state.Data.State != State.RUNNING)
                {
                    tempH= -physics.Data.HSpeed * Calamity.Instance.DeltaVector2;
                    physics.Data.Direction = Direction.LEFT;
                    sprite.Data.Y_Frame = (int)physics.Data.Direction;
                    state.Data.State = State.WALKING;
                }

                if (InputManager.Instance.JustClickedRight())
                {
                    sprite.Data.Y_Frame = (int)physics.Data.Direction + 4;
                    sprite.Data.CurrentFrame = 0;
                    state.Data.State = State.RUNNING;
                }

                if(state.Data.State == State.RUNNING && sprite.Data.CurrentFrame == sprite.Data.Frames - 2)
                {
                    state.Data.State = State.IDLE;
                    sprite.Data.Y_Frame = (int)physics.Data.Direction;
                }

                if(tempH.X == 0 && tempV.Y == 0 && state.Data.State != State.RUNNING)
                {
                    state.Data.State = State.IDLE;
                }

                if(Math.Abs(tempH.X) != 0  && Math.Abs(tempV.Y) != 0)
                {
                    tempH.X *= 0.707f;
                    tempV.Y *= 0.707f;
                }

                if (InputManager.Instance.Pressed(Input.RUN))
                {
                    tempH *= 100;
                    tempV *= 100;
                }

                physics.Data.Position += new Vector2(tempH.X, tempV.Y);
                
            }
        }

        internal void RemoveActor(int Id)
        {
            if (PlayerControlledList.Contains(ActorHandlingSystem.Instance.GetActor(Id)))
            {
                PlayerControlledList.Remove(ActorHandlingSystem.Instance.GetActor(Id));
            }
        }

        internal List<Actor> GetPlayerControlledList()
        {
            return PlayerControlledList;
        }

        internal int GetActorCount()
        {
            return PlayerControlledList.Count;
        }

        internal Actor GetActor(int ID)
        {
            return ActorHandlingSystem.Instance.GetActor(ID);
        }

        
    }
}
