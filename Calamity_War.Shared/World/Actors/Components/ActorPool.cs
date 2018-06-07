using System;
using System.Collections.Generic;
using System.Linq;

namespace Calamity_War
{
    public class ActorPool
    {
        public readonly IDictionary<int, Actor> actors;

        public ActorPool()
        {
            actors = new Dictionary<int, Actor>();
        }

        internal void AddActor(Actor actor)
        {
            if (!actors.ContainsKey(actor.Id))
            {
                actors.Add(actor.Id, actor);
            }
        }

        internal void RemoveActor(Actor actor)
        {
            if (actors.ContainsKey(actor.Id))
            {
                actors.Remove(actor.Id);
            }
            else
            {
                throw new ArgumentException("Actor not found in List to be Removed");
            }
        }

        public void ClearPool()
        {
            actors.Clear();
        }

        public int GetCount()
        {
            int count = actors.Count();

            return count;
        }
    }
}
