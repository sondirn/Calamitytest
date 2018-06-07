using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public class StateComponent : Component
    {

        public StateData Data;

        public StateComponent(StateData Data, string Name = "State") : base(Name)
        {
            this.Data = Data;
        }
    }
}
