using System.Collections.Generic;

namespace Calamity_War
{
    public interface IComponentOwner
    {
        int Id { get; }
        T GetComponent<T>() where T : IComponent;
        List<T> GetComponents<T>() where T : IComponent;
        bool ComponentExists<T>() where T : IComponent;
        
    }
}
