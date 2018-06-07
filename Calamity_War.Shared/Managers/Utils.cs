using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    static class Utils
    {
        public static bool PointInRect(float pointX, float pointY, RectangleF rect)
        {
            return InRange(pointX, rect.X, rect.X + rect.Width) && InRange(pointY, rect.Y, rect.Y + rect.Height);
        }
        private static bool InRange(float value, float min, float max)
        {
            return value >= Math.Min(min, max) && value <= Math.Max(min, max);
        }
        
        private static bool RangeIntersect(float min0, float max0, float min1, float max1)
        {
            return Math.Max(min0, max0) >= Math.Min(min1, max1) &&
                Math.Min(min0, max0) <= Math.Max(min1, max1);
        }

        public static bool RectIntersect(RectangleF rect1, RectangleF rect2)
        {
           return RangeIntersect(rect1.X, rect1.X + rect1.Width, rect2.X, rect2.X + rect2.Width) &&
                RangeIntersect(rect1.Y, rect1.Y + rect1.Height, rect2.Y, rect2.Y + rect2.Height);
        }

       
        
    }
}
