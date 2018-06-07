using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Calamity_War
{
    public sealed class InputManager
    {
        private static InputManager instance = null;
        private static readonly object padlock = new object();
        private readonly Dictionary<Input, Keys> keyBindings;
        private KeyboardState previousState;
        private KeyboardState currentState;
        private static MouseState mouseCurrent;
        private static MouseState mousePrevious;


        public static InputManager Instance
        {
            get
            {
                lock (padlock)
                {
                    if(instance == null)
                    {
                        instance = new InputManager();
                    }
                    return instance;
                }
            }
        }

        public  InputManager()
        {
            keyBindings = new Dictionary<Input, Keys>
            {
                {Input.MOVE_DOWN, Keys.S },
                {Input.MOVE_RIGHT, Keys.D },
                {Input.MOVE_UP, Keys.W },
                {Input.MOVE_LEFT, Keys.A },
                {Input.BACK, Keys.B },
                {Input.RUN, Keys.LeftShift },
                {Input.UP, Keys.Up },
                {Input.RIGHT, Keys.Right },
                {Input.LEFT, Keys.Left },
                {Input.DOWN, Keys.Down },
                {Input.HIncrease, Keys.K },
                {Input.HDecrease, Keys.I },
                {Input.WIncrease, Keys.L },
                {Input.WDecrease, Keys.J },
                
                
            };
        }

        internal void Initialize()
        {
            currentState = Keyboard.GetState();
            mouseCurrent = Mouse.GetState();
            mousePrevious = Mouse.GetState();
            previousState = Keyboard.GetState();
        }

        internal void UpdateStart()
        {
            mouseCurrent = Mouse.GetState();
            currentState = Keyboard.GetState();
        }

        internal void UpdateEnd()
        {
            previousState = currentState;
            mousePrevious = mouseCurrent;
        }

        internal bool Pressed(Input inputs)
        {
            if (currentState.IsKeyDown(keyBindings[inputs]))
                return true;
            return false;
        }


        internal bool JustPressed(Input inputs)
        {
            if (currentState.IsKeyDown(keyBindings[inputs]) && !previousState.IsKeyDown(keyBindings[inputs]))
                return true;
            return false;
        }

        internal bool ClickedLeft()
        {
            if (mouseCurrent.LeftButton == ButtonState.Pressed)
                return true;
            return false;
        }

        internal bool JustClickedLeft()
        {
            if (mouseCurrent.LeftButton == ButtonState.Pressed && mousePrevious.LeftButton != ButtonState.Pressed)
            {
                return true;
            }
            else return false;
        }

        internal bool ClickedRight()
        {
            if (mouseCurrent.RightButton == ButtonState.Pressed)
                return true;
            return false;
        }

        internal bool JustClickedRight()
        {
            if (mouseCurrent.RightButton == ButtonState.Pressed && mousePrevious.RightButton != ButtonState.Pressed)
            {
                return true;
            }
            else return false;
        }

        internal Vector2 GetMouseToWorld()
        {
            return CameraSystem.Instance.Camera.ScreenToWorld(new Vector2(mouseCurrent.X, mouseCurrent.Y));
        }

        internal Vector2 GetMouseToScreen()
        {
            return new Vector2(mouseCurrent.X, mouseCurrent.Y);
        }
    }
}
