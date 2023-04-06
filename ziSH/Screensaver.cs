using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Sys = Cosmos.System;

namespace ziOS.ziSH
{
    public class Screensaver
    {
        public static void Start()
        {
            bool stopped = false;
            Console.Clear();
            while (!stopped)
            {
                Console.ForegroundColor = (ConsoleColor)tuldrv.rng.Next(1, 15);
                Console.Write((char)tuldrv.rng.Next(33,126));
                Thread.Sleep(5);
                if (Sys.KeyboardManager.ShiftPressed)
                {
                    stopped = true;
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            Cmds.Clear();
        }
    }
}
