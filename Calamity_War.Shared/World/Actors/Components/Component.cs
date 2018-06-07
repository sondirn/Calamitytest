namespace Calamity_War
{
    public abstract class Component : IComponent
    {
        public Actor Owner { get; set; }
        public readonly string Name;
        public bool Killed { get; set; }

        protected Component(string Name)
        {
            this.Name = Name;
            
        }

       
    }
}
