using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Linq;

namespace Calamity_War
{

    public class Calamity : Game
    {
        #region PRIVATE VARIABLES
        private static Calamity instance;
        private ContentManager contentManager;
        
        private Handler handler;
        
        
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        #endregion

        #region PUBLIC VARIABLES
        public float Delta;
        public Vector2 DeltaVector2;
        
        #region TEMPORARY VARIABLES
        int width;
        int height;
        
        #endregion
        public ContentManager ContentManager
        {
            get
            {
                return contentManager;
            }
        }
        
        public static Calamity Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Calamity();
                }
                return instance;
            }
        }
        public GraphicsDeviceManager Graphics
        {
            get
            {
                return graphics;
            }
        }
        public Handler Handler
        {
            get
            {
                return handler;
            }
        }
        #endregion

        

        public Calamity()
        {
            CheckFiles();
            instance = this;
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 200.0f);
            graphics.SynchronizeWithVerticalRetrace = false;
            //Managers
            contentManager = new ContentManager();
            //inputManager = new InputManager();

            //Load Window Settings
            WindowSettingsLoad();

            handler = new Handler();
            


        }

        
        protected override void Initialize()
        {
            DeltaVector2 = new Vector2(0, 0);
            WindowManager.SetWindowWidth(width);
            WindowManager.SetWindiwHeight(height);
            contentManager.Prepare(GraphicsDevice);
            InputManager.Instance.Initialize();
            
            handler.Initialize();
            
            base.Initialize();
            
        }

        
        protected override void LoadContent()
        {
           
            spriteBatch = new SpriteBatch(GraphicsDevice);
            handler.LoadContent();

        }

       
        protected override void UnloadContent()
        {
            handler.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            Delta = (float)gameTime.ElapsedGameTime.TotalSeconds;
            DeltaVector2.X = Delta;
            DeltaVector2.Y = Delta;
           
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            InputManager.Instance.UpdateStart();
           
            //Insert Game Code
            handler.Update(gameTime);
            InputManager.Instance.UpdateEnd();

            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //Draw
            CameraSystem.Instance.Draw();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointWrap, transformMatrix: CameraSystem.Instance.TransformationMatrix);
            handler.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            //DrawGUI
            spriteBatch.Begin();
            handler.DrawGUI(spriteBatch, gameTime);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public void WindowSettingsLoad()
        {
            var dic = File.ReadAllLines(Global.SettingsFolder + Global.WindowSettingsFile)
                .Select(l => l.Split(new[] { '=' }))
                .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

            width = Convert.ToInt32(dic["width"]);
            height = Convert.ToInt32(dic["height"]);

            if (Convert.ToBoolean(dic["mouseVisible"]))
            {
                IsMouseVisible = true;
            }
        }
        
        public void CheckFiles()
        {
            
            DirectoryInfo di = Directory.CreateDirectory(Global.SettingsFolder);
            if(!File.Exists(Global.SettingsFolder + Global.WindowSettingsFile))
            {
                string[] lines =
                {
                    "width=1280",
                    "height=720",
                    "mouseVisible=false"
                };

                using(StreamWriter outputFile = new StreamWriter(Path.Combine(Global.SettingsFolder, Global.CreateWindowSettingsFile)))
                {
                    foreach(string line in lines)
                    {
                        outputFile.WriteLine(line);
                    }
                }
            }
        }
    }
}
