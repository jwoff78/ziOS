using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using ziOS.ziSH;
using Sys = Cosmos.System;

namespace ziOS.Libs
{
    public class libmouse
    {
        public static void CursorErase()
        {
            bool stopped = false;
            Sys.MouseManager.ScreenWidth = 89*3;
            Sys.MouseManager.ScreenHeight = 29*3;
            int oldx = Console.CursorLeft, oldy = Console.CursorTop;
            ConsoleColor oldc = Console.BackgroundColor;
            int x = 0, y = 0;
            bool DontDrawPos = false;
            while (!stopped)
            {
                x = (int)Sys.MouseManager.X / 3; y = (int)Sys.MouseManager.Y / 3;
                if (!DontDrawPos)
                {
                    Console.SetCursorPosition(0, 29);
                    Console.Write($"X={x} Y={y}");
                }
                if (Sys.MouseManager.MouseState == Sys.MouseState.Left)
                {
                    DontDrawPos = true;
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(x, y);
                    Console.Write(' ');
                    for (int i = 0; i < 5; i++) { }
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.SetCursorPosition(x, y);
                    Console.Write(' ');
                }
                if (Sys.MouseManager.MouseState == Sys.MouseState.Right)
                {
                    DontDrawPos = false;
                }
                if (Sys.MouseManager.MouseState == Sys.MouseState.Middle) { stopped = true; }
            }
            Console.BackgroundColor = oldc;
            Cmds.Clear();
        }
    }
}
