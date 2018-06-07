using System;
using System.Collections.Generic;
using System.Text;

namespace Calamity_War
{
    public class InputData
    {
        public bool IsPressed;
        public bool IsHeld;
        public double NextValidInput;

        public static InputData Unpressed
        {
            get
            {
                return new InputData { IsHeld = false, IsPressed = false, NextValidInput = double.MinValue };
            }
        }

        public InputData()
        {
            IsPressed = IsHeld = false;
            NextValidInput = double.MinValue;
        }

        public InputData(double pNextValidInput)
        {
            IsPressed = IsHeld = false;
            NextValidInput = pNextValidInput;
        }
    }
}
