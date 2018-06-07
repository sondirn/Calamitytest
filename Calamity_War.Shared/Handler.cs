using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;
using System.Diagnostics;

namespace Calamity_War
{
    public class Handler
    {
        public SystemHolder systemHolder;
        private Actor actor;
        int test;
        IntRef reff;
        List<int> testtest;
        bool saved;
        
        public Handler()
        {
           
            systemHolder = new SystemHolder();
            testtest = new List<int>();
            test = 5;
            reff = new IntRef(ref test);
            saved = false;
        }

        public void Initialize()
        {
            systemHolder.Initialize();
            testtest.Add(reff.Value);
            ActorGenerationSystem.Instance.CreateActor("Player",
            new Physics(new PhysicsData
            {
                Position = new Vector2(0, 0),
                
                Width = 16,
                Height = 16,
                //BoundsY = 16,
                //BoundsX = 3,
                Speed = new Vector2(64, 64),
                Collidable = true
            }), new Sprite(new SpriteData
            {
                Color = Color.White,
                Width = 28,
                Height = 34,
                Origin = new Vector2(14, 12),
                Animated = true,
                Animating = false,
                Frames = 4,
                Y_Frame = 0,
                FrameSpeed = 64,
                TexturePath = "Textures/SkeletonConjurer",
                
            }),
            new StateComponent(new StateData{
                State = State.IDLE
            }),new PlayerController(new PlayerControllerData()));


            ActorGenerationSystem.Instance.CreateActor("Camera",
                new Physics(new PhysicsData
                {
                    Position = new Vector2(32, 32),
                    Width = 640,
                    Height = 360
                }), new Camera(new CameraData()));
            
            ActorGenerationSystem.Instance.CreateActor("Pasdf",
            new Physics(new PhysicsData
            {
                Position = new Vector2(32, 32),      
                Width = 9,
                Height = 8,

                Collidable = true
            }),
            new StateComponent(new StateData
            {
                State = State.WALL
            }));
            
            for(int x = 0; x < 2000; x++)
            {
                for(int y = 0; y < 2000; y++)
                {
                    TileRenderSystem.Instance.CreateTile(new Tile(new TileData
                    {
                        
                        SpriteSheet = "Tasdfasdfasdf",
                        Width = 16,
                        Height = 16,
                        xFrame = 0,
                        yFrame = 0
                    }), 0,x,y);
                }
            }
        }

        public void LoadContent()
        {
            systemHolder.LoadContent();
        }

        public void UnloadContent()
        {
            systemHolder.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            systemHolder.Update(gameTime);
            test++;
            Debug.WriteLine(((int)CameraSystem.Instance.Camera.Position.X + 640)/ 16);

            if (InputManager.Instance.JustClickedLeft())
            {
                foreach(KeyValuePair<int, Actor> a in ActorHandlingSystem.Instance.GetActorList())
                {
                    if(a.Value.ComponentExists<Physics>() && a.Value.ComponentExists<Sprite>())
                    {
                        var phys = a.Value.GetComponent<Physics>();
                        var spr = a.Value.GetComponent<Sprite>();
                        if (Utils.PointInRect(InputManager.Instance.GetMouseToWorld().X, InputManager.Instance.GetMouseToWorld().Y, new RectangleF(phys.Data.Position.X - spr.Data.Origin.X, phys.Data.Position.Y - spr.Data.Origin.Y, spr.Data.Width, spr.Data.Height)))
                        {
                            actor = a.Value;
                        }
                    }
                }
                
            }

            if(saved == false)
            {
                Calamity.Instance.ContentManager.SaveToXML(this, "TestHandler");
                saved = true;
            }



            if (InputManager.Instance.JustPressed(Input.DOWN))
            {
                actor.GetComponent<Physics>().Data.BoundsY++;
            }

            if (InputManager.Instance.JustPressed(Input.UP))
            {
                actor.GetComponent<Physics>().Data.BoundsY--;
            }

            if (InputManager.Instance.JustPressed(Input.LEFT))
            {
                actor.GetComponent<Physics>().Data.BoundsX--;
            }

            if (InputManager.Instance.JustPressed(Input.RIGHT))
            {
                actor.GetComponent<Physics>().Data.BoundsX++;
            }

            if (InputManager.Instance.JustPressed(Input.HIncrease))
            {
                actor.GetComponent<Physics>().Data.Height++;
            }

            if (InputManager.Instance.JustPressed(Input.HDecrease))
            {
                actor.GetComponent<Physics>().Data.Height--;
            }

            if (InputManager.Instance.JustPressed(Input.WDecrease))
            {
                actor.GetComponent<Physics>().Data.Width--;
            }

            if (InputManager.Instance.JustPressed(Input.WIncrease))
            {
                actor.GetComponent<Physics>().Data.Width++;
            }


        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            systemHolder.Draw(gameTime, spriteBatch);
            
        }

        public void DrawGUI(SpriteBatch spriteBatch, GameTime gameTime)
        {
            systemHolder.DrawGUI(gameTime, spriteBatch);
        }
    }
}
