using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public sealed class GameModeSystem : System
    {
        private static GameModeSystem instance = null;
        private static readonly object padlock = new object();
        

        public static GameModeSystem Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new GameModeSystem();
                    }
                    return instance;
                }
            }
        }

        public GameModeSystem()
        {

        }
    }
    
}
