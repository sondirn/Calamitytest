using Microsoft.Xna.Framework;

namespace Calamity_War
{
    public class Sprite : Component
    {
        public SpriteData Data;
        

        public Sprite(SpriteData Data, string Name = "Sprite") : base(Name)
        {
            this.Data = Data;
        }
    }
}
