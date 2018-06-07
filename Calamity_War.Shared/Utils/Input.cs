using System;

namespace Calamity_War
{
    [Flags]
    public enum Input
    {
        MOVE_UP,
        MOVE_RIGHT,
        MOVE_LEFT,
        MOVE_DOWN,
        BACK,
        RUN,
        UP,
        RIGHT,
        DOWN,
        LEFT,
        WIncrease,
        HIncrease,
        WDecrease,
        HDecrease,
        ZoomIn,
        ZoomOut
    }
}
