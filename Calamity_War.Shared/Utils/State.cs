using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    [Flags]
    public enum State
    {
        IDLE,
        WALKING,
        RUNNING,
        WANDERING,
        WALL,
        ISCAMERA
    }

}
