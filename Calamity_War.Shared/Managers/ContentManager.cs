using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Intermediate;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System.IO;
using System.Xml;

namespace Calamity_War
{
    public class ContentManager
    {
        private GraphicsDevice graphicsDevice;
        public GraphicsDevice GraphicsDevice
        {
            get
            {
                return graphicsDevice;
            }
        }

        public ContentManager()
        {

        }

        public void Prepare(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
        }

        public Texture2D GetTexture2D(string filePath)
        {
            if (File.Exists(Calamity.Instance.Content.RootDirectory + "/" + filePath + ".xnb"))
            {
                return Calamity.Instance.Content.Load<Texture2D>(filePath);
            }
            else
            {
                //TODO: RETURN DEFAULT TEXTURE
                return Calamity.Instance.Content.Load<Texture2D>("Textures/DefaultTexture");
            }
        }

        public SoundEffect GetSoundEffect(string filePath)
        {
            if (File.Exists(Calamity.Instance.Content.RootDirectory + "/" + filePath + ".xnb"))
            {
                return Calamity.Instance.Content.Load<SoundEffect>(filePath);
            }
            else
            {
                throw new FileNotFoundException("Cound Not Load SoundEffect: " + filePath);
            }
        }

        public BitmapFont GetBitmapFont(string filePath)
        {
            if (File.Exists(Calamity.Instance.Content.RootDirectory + "/" + filePath + ".xnb"))
            {
                return Calamity.Instance.Content.Load<BitmapFont>(filePath);
            }
            else
            {
                throw new FileNotFoundException("Could Not Load BitMapFont: " + filePath);
            }
        }

        public void SaveToXML(object obj, string FileName)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(FileName + ".xml", settings))
            {
                IntermediateSerializer.Serialize(writer, obj, null);
            }
        }

    
    }

}
