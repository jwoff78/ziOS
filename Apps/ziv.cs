using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ziOS.ziSH;

namespace ziOS.Apps
{
    public class ziv
    {
        static string buffer = "";
        static string file = "";
        static string name = "";
        static bool end = false;
        public static void Start(string filename)
        {
            if (File.Exists(tuldrv.syspath + filename))
            {
                buffer = File.ReadAllText(tuldrv.syspath + filename);
            }
            else
            {
                tuldrv.Log("No files? I will make one then!", "ziv");
                File.Create(tuldrv.syspath + filename).Close();
            }
            file = tuldrv.syspath + filename;
            name = filename;
            Entry();
        }


        public static void Entry()
        {
            Console.Clear();
            tuldrv.Bar(ConsoleColor.DarkYellow, $"ziv - {name} - Type $ziv-wq on 1 line to save and quit.", ConsoleColor.Black);
            while (!end)
            {
                MainLoop();
            }
            End();
        }
        public static void MainLoop()
        {
            string buff2 = Console.ReadLine();
            if (buff2 == "$ziv-wq")
            {
                end = true;
            }
            else if (buff2 == "$ziv-q")
            {
                end = true;
            }
            else
            {
                buffer = buffer + buff2 + "\n";
            }
        }

        public static void End()
        {
            Cmds.Clear();
            Console.WriteLine("Saving file to disk...");
            File.WriteAllText(file, buffer);
            tuldrv.DisplaySuccess();
        }
    }
}