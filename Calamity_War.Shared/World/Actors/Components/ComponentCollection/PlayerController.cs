using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public class PlayerController : Component
    {
        public PlayerControllerData Data;

        public PlayerController(PlayerControllerData Data,string Name = "PlayerController") : base(Name)
        {
            this.Data = Data;
        }
    }
}
