using System;
using System.Collections.Generic;
using System.Text;

namespace ziOS
{
    public class StartupOptions
    {
        public static void Entry()
        {
            byte option = tuldrv.Menu(new string[] { "Disable system log", "Disable file system", "Disable log AND file system", "Disable the Operating System" });
            switch (option)
            {
                case 0:
                    tuldrv.DisableSysLog = true;
                    break;
                case 1:
                    tuldrv.DisableFS = true;
                    break;
                case 2:
                    tuldrv.DisableSysLog = true;
                    tuldrv.DisableFS = true;
                    break;
                case 3:
                    while(true)
                    {
                        Console.WriteLine("okay");
                    }
            }
        }
    }
}
