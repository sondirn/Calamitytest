using System.Collections.Generic;
using System.Linq;

namespace Calamity_War
{
    public class Actor : IComponentOwner
    {
        public readonly IList<IComponent> components;
        public int Id { get; set; }
        public string Name { get; set; }
        public bool InPhase { get; set; }

        public Actor(int Id = 0, string Name = "No Name")
        {
            components = new List<IComponent>();
            this.Name = Name;
            this.Id = Id;
            InPhase = true;
        }

        public void AddComponent(Component component)
        {
            if(component != null)
            {
                components.Add(component);
            }
        }

        public bool ComponentExists<T>() where T : IComponent
        {
            return components.Contains(GetComponent<T>());
        }

        public void RemoveComponent(string componentName)
        {
            
        }

        public void ClearComponentList()
        {
            components.Clear();
        }

        public void SetOutOfPhase()
        {
            ActorHandlingSystem.Instance.RemoveFromSystems(Id);
            ActorHandlingSystem.Instance.AddActorOutPhase(this);
            ClearComponentList();
            InPhase = false;
            
            
        }

        public void SetInPhase()
        {
            InPhase = true;
        }

        public T GetComponent<T>() where T : IComponent
        {
            var component = components.FirstOrDefault(c => c.GetType() == typeof(T));
            return (T)component;
        }

        public List<T> GetComponents<T>() where T: IComponent
        {
            return components.Where(c => c is T).Cast<T>().ToList();
        }

        
    }
}
