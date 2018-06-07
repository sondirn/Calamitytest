using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public class EnemyAI : Component
    {
        public EnemyAIData Data;

        public EnemyAI(EnemyAIData Data, string Name = "EnemyAI") : base(Name)
        {
            this.Data = Data;
        }
    }
}
