using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public struct IntRef
    {
        public int Value { get; set; }

        public IntRef(ref int Value)
        {
            this.Value = Value;
        }
    }
}
