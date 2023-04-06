using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Sys = Cosmos.System;
using ziOS.Libs;
using ziOS.Apps;

namespace ziOS.ziSH
{
    public class Cmds
    {
        // NOTE: WHEN THIS FILE REACHES 350 LINES, SPLIT IT. libFS.

        public static void Help(string[] args)
        {
            if (args.Length == 1)
            {
                //                |                                                                                          |
                Console.WriteLine("List of commands:\n");
                Console.WriteLine("help or ? - Displays this menu.");
                Console.WriteLine("shutdown or - [r] - If arg isn't given, it will shutdown, if arg is '-r',it will restart.");
                Console.WriteLine("echo [msg] - This command simply echos back what you say, it's a sanity check, I guess.");
                Console.WriteLine("syslog - It's like Event Viewer in Windows. It just prints the current system log.");
                Console.WriteLine("ls or $ [path] - this command will list present dir, if path is given, it will list it.");
                Console.WriteLine("cd or @ [path] - Sets path to given path. Do 'cd..' without the space to go to upper dir.");
                Console.WriteLine("fwrite or * [file] [msg] - Writes msg argument to the file argument on the file system.");
                Console.WriteLine("cat or # [file] [x] - Prints the contents of a file. Use -x flag to print in hexadecimal!");
                Console.WriteLine("rm, del, or & [r] - Removes a file or directory, when deleting dirs, use -r switch.");
                Console.WriteLine("mkdir [dir] - Creates a directory of the given path.");
                Console.WriteLine("ziv or % - Built-in text editor."); //
                //Console.WriteLine("udlc or ! - Underlang compiler."); // for a long time lol
                //Console.WriteLine("exec or ~ - Execute SBC program."); //
                Console.WriteLine("scr - Screensaver. Hold shift, alt, or control for about half a second to exit.");
                Console.WriteLine("cls or clear - Clears the screen. What did you think it would do?");
                    Console.Write("eraser - A little app that turns your computer mouse into an eraser, middle click to exit.");
                Console.WriteLine("condraw - An app makes you to draw on the console using the mouse! Middle click to exit.");
            }
        }
        public static void Shutdown(string[] args)
        {
            if (args.Length == 2)
            {
                if (args[1] == "-r")
                {
                    Console.WriteLine("Hasta Luego!");
                    Sys.Power.Reboot();
                }
                else
                {
                    tuldrv.DisplayError("Invalid use of shutdown command.", true);
                }
            }
            else if (args.Length == 1)
            {
                Console.WriteLine("Adios!");
                Sys.Power.Shutdown();
            }
            else
            {
                tuldrv.DisplayError("Invalid use of shutdown command.", true);
            }
        }

        public static void Echo(string[] args)
        {
            if (args.Length == 1)
            {
                Console.WriteLine();
            }
            else
            {
                var arglist = args.ToList();
                arglist.RemoveAt(0);
                Console.WriteLine(string.Join(' ', arglist.ToArray()));
            }
        }

        public static void ViewSysLog()
        {
            if (!tuldrv.DisableSysLog)
            {
                Console.WriteLine("System log so far:");
                Console.WriteLine(tuldrv.GetLog());
            }
            else
            {
                tuldrv.DisplayError("The system log is disabled.",true);
            }
        }
        public static void Clear()
        {
            Console.Clear();
            Console.SetCursorPosition(89 - Kernel.buildstring.Length, 29);
            Console.Write(Kernel.buildstring);
            Console.SetCursorPosition(0, 0);
            tuldrv.Bar(ConsoleColor.DarkYellow, $"Welcome to ziOS. Type '?' for a list of commands.", ConsoleColor.Black);
        }
        public static void ziv(string[] args)
        {
            if (!tuldrv.DisableFS)
            {
                if (args.Length == 2)
                {
                    Apps.ziv.Start(args[1]);
                } else
                {
                    tuldrv.DisplayError("Invalid use of ziv command! Expected filename for argument 1!", true);
                }
            } else
            {
                tuldrv.DisplayError("The file system is Disabled.", true);
            }
        }
        public class CmdHandler
        {
            public static void Handle(string cmd)
            {
                string[] args = cmd.Split(' ');

                if (args[0] == "help" || args[0] == "?") { Cmds.Help(args); }
                else if (args[0] == "shutdown" || args[0] == "-") { Cmds.Shutdown(args); }
                else if (args[0] == "echo") { Cmds.Echo(args); }
                else if (args[0] == "syslog") { Cmds.ViewSysLog(); }
                else if (args[0] == "ls" || args[0] == "$") { libFS.LS(args); }
                else if (args[0] == "cd" || args[0] == "@") { libFS.CD(args); }
                else if (args[0] == "cd.." || args[0] == "@..") { libFS.CDdotdot(); }
                else if (args[0] == "fwrite" || args[0] == "*") { libFS.Fwrite(args); }
                else if (args[0] == "cat" || args[0] == "#") { libFS.cat(args); }
                else if (args[0] == "rm" || args[0] == "del" || args[0] == "&") { libFS.remove(args); }
                else if (args[0] == "mkdir") { libFS.mkdir(args); }
                else if (args[0] == "eraser") { libmouse.CursorErase(); } // REMOVE?
                else if (args[0] == "cls" || args[0] == "clear") { Cmds.Clear(); }
                else if (args[0] == "scr") { Screensaver.Start(); }
                else if (args[0] == "condraw") { condraw.CursorDraw(); }
                else if (args[0] == "ziv" || args[0] == "%") { Cmds.ziv(args); }
                else
                {
                    tuldrv.DisplayError($"Command \"{args[0]}\" was not found in the command dictionary.", true);
                }
            }
        }
    }
}
