using System;
using System.Collections.Generic;
using System.Text;

namespace ziOS.ziSH
{
    public class Prompt
    {
        public static string ShowPrompt()
        {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(tuldrv.syspath);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write('-');
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(')');
            Console.ForegroundColor = ConsoleColor.White;
            return Console.ReadLine();
        }
    }
}
