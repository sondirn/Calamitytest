using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public sealed class TileRenderSystem : System
    {
        
        private List<Vector2[,]> TilePos;
        private List<Rectangle[,]> Rects;
        public List<Tile[,]> Tiles;
        public int Layers;
        public int tileWidth;
        public int tileHeight;
        private static TileRenderSystem instance = null;
        private static readonly object padlock = new object();
        private static IDictionary<string, Texture2D> textures;
        private int xTiles;
        private int yTiles;
        public int MapWidth;
        public int MapHeight;

        public static TileRenderSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new TileRenderSystem();
                    }
                    return instance;
                }
            }
        }

        public TileRenderSystem()
        {
            textures = new Dictionary<string, Texture2D>();
            Tiles = new List<Tile[,]>();
            TilePos = new List<Vector2[,]>();
            Rects = new List<Rectangle[,]>();
            Layers = 1;
            tileWidth = 16;
            tileHeight = 16;
            xTiles = 2000;
            yTiles = 2000;
            MapWidth = xTiles * tileWidth;
            MapHeight = yTiles * tileHeight;

            for(int i  = 0; i < Layers; i++)
            {
                Tiles.Add(new Tile[2000,2000]);
                TilePos.Add(new Vector2[2000,2000]);
                Rects.Add(new Rectangle[2000,2000]);
            }
        }

        internal void LoadContent()
        {
            for(int i = 0; i < Layers; i++)
            {
                for(int x  = 0; x < 1000; x++)
                {
                    for(int y = 0; y < 1000; y++)
                    {
                        string path = Tiles[i][x,y].Data.SpriteSheet;
                        if (path != null && !textures.ContainsKey(path))
                        {
                            Texture2D texture = Calamity.Instance.ContentManager.GetTexture2D(path);
                            textures.Add(path, texture);
                        }
                    }
                }
            }
        }

        internal void CreateTile(Tile tile,int layer, int indexX, int indexY)
        {
            Tiles[layer][indexX, indexY] = tile;
            TilePos[layer][indexX, indexY] = tile.Data.Position;
            Rects[layer][indexX, indexY] = (new Rectangle(tile.Data.xFrame * tile.Data.Width, tile.Data.yFrame * tile.Data.Height, tile.Data.Width, tile.Data.Height));
        }

        internal void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            for(int i = 0; i < Layers; i++)
            {
                for(int x = (int)CameraSystem.Instance.Camera.Position.X / 16; x < (int)Math.Ceiling((CameraSystem.Instance.Camera.Position.X + 640) / 16); x++)
                {
                    for(int y = (int)CameraSystem.Instance.Camera.Position.Y / 16; y < (int)Math.Ceiling((CameraSystem.Instance.Camera.Position.Y + 360) / 16); y++)
                    {
                        if(Tiles[i][x,y].Data.SpriteSheet != null)
                        {
                            spriteBatch.Draw(textures[Tiles[i][x, y].Data.SpriteSheet], new Vector2(x * 16, y * 16), Rects[i][x, y], Color.White);
                        }
                        
                    }
                    
                }
            }
        }

    }
}
