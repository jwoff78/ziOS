using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.Threading;
using ziOS.ziSH;

namespace ziOS
{
    public class Kernel : Sys.Kernel
    {
        public static readonly int build = 230404;
        public static readonly string buildstring = $"ziOS Milestone 3 [Build {build}], Copyright (c) 2023 z-izz";
        Sys.FileSystem.CosmosVFS fs;
        protected override void BeforeRun()
        {
            try
            {
                Sys.Graphics.VGAScreen.SetTextMode(Cosmos.HAL.VGADriver.TextSize.Size90x30);
                Console.Clear();
                Console.WriteLine("Hold shift key NOW to get advanced startup options...");
                Thread.Sleep(1000);
                if (Sys.KeyboardManager.ShiftPressed)
                {
                    StartupOptions.Entry();
                }
                tuldrv.Log("The VGA console has been resized to 90*30.", "kernel");
                Console.Clear();
                tuldrv.Bar(ConsoleColor.DarkYellow, $"ziOS [Build {build}, running on a {Cosmos.Core.CPU.GetCPUVendorName()} CPU with {Cosmos.Core.CPU.GetAmountOfRAM()} MB of RAM]", ConsoleColor.White);

                if (!tuldrv.DisableFS)
                {
                    Console.Write("Initializing File System...  ");
                    fs = new Sys.FileSystem.CosmosVFS();
                    Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);
                    tuldrv.Log("File system initialized!", "kernel");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("[OK]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("  The type of the file system is " + fs.GetFileSystemType(@"0:\"));
                    Console.WriteLine("  File system is named \"" + fs.GetFileSystemLabel(@"0:\") + "\".");
                    Console.WriteLine(fs.GetTotalSize(@"0:\") + " bytes total disk space.");
                    Console.WriteLine(fs.GetAvailableFreeSpace(@"0:\") + " bytes available on disk.");
                }

                Thread.Sleep(750);
                Console.Clear();
                Console.SetCursorPosition(89 - buildstring.Length, 29);
                Console.Write(buildstring);
                Console.SetCursorPosition(0, 0);
                tuldrv.Bar(ConsoleColor.DarkYellow, $"Welcome to ziOS. Type '?' for a list of commands.", ConsoleColor.Black);

                tuldrv.Log("The kernel's BeforeRun() function has ended.", "kernel");
            } catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("       ..                     ziOS has CRASHed!");
                Console.WriteLine("        .*+:          :       System crash at boot stage.");
                Console.WriteLine("          +#*#*    -*%+       ");
                Console.WriteLine("       .:  *%%%=..+%##:       Error: " + e.Message);
                Console.WriteLine("       *%***:::+**##+:        ");
                Console.Write("        .-+=:#+****#-         Try to reboot your computer to see if it helps to stop this.");
                Console.WriteLine("     +=+=++**+=-:-=*#-        But if it happened at boot then your chances are unlikely.");
                Console.WriteLine("     -++#******++=***#        ");
                Console.WriteLine("         .-++=---=***#:       Here are some tips to get rid of crash during boot:");
                Console.WriteLine("         .++=--*#***##.       ");
                Console.WriteLine("        .**###%###**#=        1. Disable or remove any hardware that is not nessesary.");
                Console.WriteLine("       -***##%%%**##=         This can include USB/PCI devices. No mouse is required.");
                Console.WriteLine("        --:==######-          2. Disable any BIOS options that modern operating systems");
                Console.WriteLine("             ++**#:           will use. As this OS is about on par with a DOS of the 80s.");
                Console.WriteLine("             ==**+            3. Go back to 1992 linux and fix OS yourself >:)");
                Console.WriteLine("            :**+*#            ");
                Console.WriteLine("           :##+=+#.           ");
                Console.WriteLine("          :+##+=*#:           ");
                Console.WriteLine("         :=*%#+=+*=           ");
                Console.WriteLine("         -++#%*==*#-          Hope you don't have to meet crash in this OS again!");
                Console.WriteLine("         =++#%**++*#          ");
                Console.WriteLine("          .:## :+**:          ");
                Console.WriteLine("    ::.   -*##:.:=#=          ");
                Console.WriteLine("  -##############%%%:         ");
                Console.WriteLine(" :+++====*##########*         Error Type: " + e.GetType().Name);
                Console.WriteLine("         :       :=+=.        ");
                Console.WriteLine("Crash Bandicoot lies in crashed OS.");
                Console.WriteLine("\nPress any key to view system log.");
                Console.ReadKey();
                Console.Clear();
                if (!tuldrv.DisableSysLog)
                {
                    Console.WriteLine(tuldrv.GetLog());
                } else
                {
                    Console.WriteLine("Can't do that, sir! System log is disabled!");
                }
                Console.WriteLine("\nPress any key to reboot.");
                Console.ReadKey();
                Cosmos.HAL.Power.CPUReboot();
            }
        }

        protected override void Run()
        {
            try
            {
                Cmds.CmdHandler.Handle(Prompt.ShowPrompt());
            } catch (Exception e)
            {
                Console.Clear();
                Console.WriteLine("       ..                     ziOS has CRASHed!");
                Console.WriteLine("        .*+:          :       System crash at run stage.");
                Console.WriteLine("          +#*#*    -*%+       ");
                Console.WriteLine("       .:  *%%%=..+%##:       Error: " + e.Message);
                Console.WriteLine("       *%***:::+**##+:        ");
                Console.Write("        .-+=:#+****#-         Try to reboot your computer to see if it helps to stop this.");
                Console.WriteLine("     +=+=++**+=-:-=*#-        And then try to avoid whatever happened to cause this.");
                Console.WriteLine("     -++#******++=***#        ");
                Console.WriteLine("         .-++=---=***#:       Here are some tips to get rid of crash:");
                Console.WriteLine("         .++=--*#***##.       ");
                Console.WriteLine("        .**###%###**#=        1. Disable or remove any hardware that is not nessesary.");
                Console.WriteLine("       -***##%%%**##=         This can include USB/PCI devices. No mouse is required.");
                Console.WriteLine("        --:==######-          2. Disable any BIOS options that modern operating systems");
                Console.WriteLine("             ++**#:           will use. As this OS is about on par with a DOS of the 80s.");
                Console.WriteLine("             ==**+            3. Avoid what caused this. It's that simple.");
                Console.WriteLine("            :**+*#            4. Go back to 1992 linux and fix OS yourself >:)");
                Console.WriteLine("           :##+=+#.           ");
                Console.WriteLine("          :+##+=*#:           ");
                Console.WriteLine("         :=*%#+=+*=           ");
                Console.WriteLine("         -++#%*==*#-          Hope you don't have to meet crash in this OS again!");
                Console.WriteLine("         =++#%**++*#          ");
                Console.WriteLine("          .:## :+**:          ");
                Console.WriteLine("    ::.   -*##:.:=#=          ");
                Console.WriteLine("  -##############%%%:         ");
                Console.WriteLine(" :+++====*##########*         Error Type: " + e.GetType().Name);
                Console.WriteLine("         :       :=+=.        ");
                Console.WriteLine("Crash Bandicoot lies in crashed OS.");
                Console.WriteLine("\nPress any key to reboot.");
                Console.ReadKey();
                Cosmos.HAL.Power.CPUReboot();
            }
        }
    }
}
