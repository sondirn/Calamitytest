using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Calamity_War
{
    static class WindowManager
    {
        public static void SetWindowWidth(int width)
        {
            Calamity.Instance.Graphics.PreferredBackBufferWidth = width;
            Calamity.Instance.Graphics.ApplyChanges();
        }

        public static void SetWindiwHeight(int height)
        {
            Calamity.Instance.Graphics.PreferredBackBufferHeight = height;
            Calamity.Instance.Graphics.ApplyChanges();
        }

        public static void CenterScreen()
        {
            Calamity.Instance.Window.Position =
                new Point((GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2) - (Calamity.Instance.Graphics.PreferredBackBufferWidth / 2),
                (GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2) - (Calamity.Instance.Graphics.PreferredBackBufferHeight / 2));
            Calamity.Instance.Graphics.ApplyChanges();
        }
    }
}
