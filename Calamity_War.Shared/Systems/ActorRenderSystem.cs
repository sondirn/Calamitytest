using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System;
using System.Linq;
using MonoGame.Extended;

namespace Calamity_War
{
    public sealed class ActorRenderSystem : System
    {
        public static IDictionary<string, Texture2D> textures;
        private static ActorRenderSystem instance = null;
        private static readonly object padlock = new object();
        private static List<Actor> DrawList;
        private static Dictionary<int, Actor> DrawListLookUp;
        public static List<Actor> allActors;
        public bool DebugMode = false;
       
        
        
        

        public static ActorRenderSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new ActorRenderSystem();
                    }
                    return instance;
                }
            }
        }

        public ActorRenderSystem()
        {
            allActors = new List<Actor>();
            DrawList = new List<Actor>();
            DrawListLookUp = new Dictionary<int, Actor>();
            textures = new Dictionary<string, Texture2D>();
            
        }

        internal void LoadContent()
        {
            
            foreach (Actor value in DrawList)
            {
                
                
                string path = value.GetComponent<Sprite>().Data.TexturePath;
                if (!textures.ContainsKey(path))
                {
                    Texture2D texture = Calamity.Instance.ContentManager.GetTexture2D(path);
                    textures.Add(path, texture);
                }
                
                
                
            }
        }

        internal void AddActor(Actor actor)
        {
            allActors.Add(actor);
            Actors.AddActor(actor);

            
                DrawList.Add(actor);
                DrawListLookUp.Add(actor.Id, actor);
            
            if (!textures.ContainsKey(actor.GetComponent<Sprite>().Data.TexturePath))
            {
                textures.Add(actor.GetComponent<Sprite>().Data.TexturePath, Calamity.Instance.ContentManager.GetTexture2D(actor.GetComponent<Sprite>().Data.TexturePath));
            }
        }

        internal bool InDrawList(Actor actor)
        {
            return DrawListLookUp.ContainsKey(actor.Id);
        }

        internal void RemoveActor(int Id)
        {
            Actors.actors.Remove(Id);
            if (DrawList.Contains(ActorHandlingSystem.Instance.GetActor(Id)))
            {
                DrawList.Remove(ActorHandlingSystem.Instance.GetActor(Id));
                DrawListLookUp.Remove(Id);
            }
            

        }

        internal void Update(GameTime gameTime)
        {
            if (InputManager.Instance.JustPressed(Input.BACK))
            {
                DebugMode = !DebugMode;
            }
            
            DrawList.Sort(delegate (Actor x, Actor y)
            {
                var actorx = x.GetComponent<Physics>().Data.Position.Y;
                var actory = y.GetComponent<Physics>().Data.Position.Y;
                return actorx.CompareTo(actory);
            });
            foreach (Actor Value in DrawList)
            {
                
                
                
                if (Value.GetComponent<StateComponent>().Data.State != State.IDLE)
                {
                    var sprite = Value.GetComponent<Sprite>();
                sprite.Data.FrameTimer++;
                if(sprite.Data.FrameTimer >= sprite.Data.FrameSpeed)
                {
                    sprite.Data.CurrentFrame++;
                    sprite.Data.FrameTimer = 0;
                }
                if(sprite.Data.CurrentFrame >= sprite.Data.Frames)
                {
                    sprite.Data.CurrentFrame = 0;
                }
                }
                else
                {
                    Value.GetComponent<Sprite>().Data.CurrentFrame = 0;
                }
             
                
            }
        }

        internal void AddToDrawList(Actor actor)
        {
            
                DrawList.Add(actor);
                DrawListLookUp.Add(actor.Id, actor);
            
        }

        internal void RemoveDrawList(Actor actor)
        {
            
                DrawList.Remove(actor);
                DrawListLookUp.Remove(actor.Id);
            
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach(Actor Value in DrawList)
            {

                var sprite = Value.GetComponent<Sprite>();
                var physics = Value.GetComponent<Physics>();
                spriteBatch.Draw(textures[sprite.Data.TexturePath], physics.Data.Position - (new Vector2(sprite.Data.Width, sprite.Data.Height) - sprite.Data.Origin), new Rectangle(sprite.Data.CurrentFrame * sprite.Data.Width, sprite.Data.Y_Frame * sprite.Data.Height, sprite.Data.Width, sprite.Data.Height), sprite.Data.Color);
                
            }

            if (DebugMode)
            {
                foreach(Physics physics in CollisionSystem.Instance.GetActors())
                spriteBatch.DrawRectangle(new RectangleF(physics.Data.Position.X + physics.Data.BoundsX, physics.Data.Position.Y + physics.Data.BoundsY, physics.Data.Width, physics.Data.Height), Color.Yellow, 1);
            }
        }

        internal int DrawListCount()
        {
            return DrawList.Count;
        }

        private static bool TextureExists(string textureName)
        {
            if(textures[textureName] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void CheckInRange(Actor actor)
        {
            
        }
            
        
    }
}
