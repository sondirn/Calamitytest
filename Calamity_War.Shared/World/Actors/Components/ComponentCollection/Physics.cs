using Microsoft.Xna.Framework;

namespace Calamity_War
{
    public class Physics : Component
    {
        public PhysicsData Data;

        public Physics(PhysicsData Data, string Name = "Physics") : base(Name)
        {
            this.Data = Data;
            
        }
    }
}
