using System;

namespace TugOfWar
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            GameWorld.Instance.Run();
        }
    }
}
