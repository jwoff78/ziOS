using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ziOS.Libs
{
    public class libFS
    {
        public static void LS(string[] args) // ENHANCE IT WITH MORE FEATURES LATER!
        {
            if (!tuldrv.DisableFS)
            {
                string[] dirlist = Directory.GetDirectories(tuldrv.syspath);
                string[] filelist = Directory.GetFiles(tuldrv.syspath); ;
                if (args.Length == 2)
                {
                    if (Directory.Exists(tuldrv.syspath + args[1])) // TODO: SUPPORT FOR 0:\TEST\TEST2 DIRS
                    {
                        Directory.GetDirectories(tuldrv.syspath + args[1]);
                        Directory.GetFiles(tuldrv.syspath + args[1]);
                    }
                    else
                    {
                        tuldrv.DisplayError("Requested directory does not exist.", true);
                    }
                }
                Console.ForegroundColor = ConsoleColor.Cyan;
                foreach (string dir in dirlist)
                {
                    Console.WriteLine(dir);
                }
                Console.ForegroundColor = ConsoleColor.White;
                foreach (string file in filelist)
                {
                    if (file.EndsWith(".udl"))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine(file);
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.WriteLine(file);
                    }
                }
            } else
            {
                tuldrv.DisplayError("File System is Disabled.", true);
            }
        }
        public static void CD(string[] args)
        {
            if (!tuldrv.DisableFS)
            {
                if (args.Length == 2)
                {
                    if (Directory.Exists(tuldrv.syspath + args[1])) // TODO: SUPPORT FOR 0:\TEST\TEST2 DIRS
                    {
                        tuldrv.syspath += $"{args[1]}\\";
                    }
                    else
                    {
                        tuldrv.DisplayError("Requested directory does not exist.", true);
                    }
                }
                else
                {
                    tuldrv.DisplayError("Invalid use of cd command.", true);
                }
            } else
            {
                tuldrv.DisplayError("File System is Disabled.", true);
            }
        }
        public static void CDdotdot()
        {
            if (!tuldrv.DisableFS)
            {
                tuldrv.syspath = tuldrv.syspath.TrimEnd('\\');
                int lastIndex = tuldrv.syspath.LastIndexOf('\\');
                tuldrv.syspath = tuldrv.syspath.Substring(0, lastIndex) + '\\';
            } else
            {
                tuldrv.DisplayError("File System is Disabled.", true);
            }
        }

        public static void Fwrite(string[] args) // TODO: SUPPORT FOR 0:\TEST\TEST2 DIRS
        {
            if (!tuldrv.DisableFS)
            {
                if (args.Length <= 3)
                {
                    tuldrv.DisplayError("Invalid use of fwrite command.", true);
                }
                else
                {
                    var arglist = args.ToList();
                    arglist.RemoveAt(0);
                    arglist.RemoveAt(0);
                    File.WriteAllText(tuldrv.syspath + args[1], string.Join(' ', arglist.ToArray()));
                    tuldrv.DisplaySuccess();
                }
            } else
            {
                tuldrv.DisplayError("File System is Disabled.", true);
            }
        }
        public static void cat(string[] args) // TODO: SUPPORT FOR 0:\TEST\TEST2 DIRS
        {
            if (!tuldrv.DisableFS)
            {
                if (args.Length == 3)
                {
                    if (args[2] == "-x")
                    {
                        if (File.Exists(tuldrv.syspath + args[1]))
                        {
                            string hex = tuldrv.ConvertToHex(File.ReadAllText(tuldrv.syspath + args[1]));
                            Console.WriteLine(hex);
                            Console.WriteLine($"\n{File.ReadAllBytes(tuldrv.syspath + args[1]).Length} bytes.");
                        } else
                        {
                            tuldrv.DisplayError("Requested file does not exist.", true);
                        }
                    } else
                    {
                        tuldrv.DisplayError("Invalid use of cat command.", true);
                    }
                }
                else if (args.Length == 2)
                {
                    if (File.Exists(tuldrv.syspath + args[1]))
                    {
                        Console.WriteLine(File.ReadAllText(tuldrv.syspath + args[1]));
                    } else
                    {
                        tuldrv.DisplayError("Requested file does not exist.", true);
                    }
                } else
                {
                    tuldrv.DisplayError("Invalid use of cat command.", true);
                }
            } else
            {
                tuldrv.DisplayError("File System is Disabled.", true);
            }
        }

        public static void remove(string[] args)
        {
            if (!tuldrv.DisableFS)
            {
                if (args.Length == 3)
                {
                    if (args[2] == "-r")
                    {
                        if(Directory.Exists(tuldrv.syspath + args[1]))
                        {
                            var filelist = Directory.GetFiles(tuldrv.syspath + args[1]);
                            foreach ( var file in filelist ) 
                            {
                                File.Delete(tuldrv.syspath + args[1] + "\\" + file);
                            }
                            Directory.Delete(tuldrv.syspath + args[1]);
                            tuldrv.DisplaySuccess();
                        }
                    }
                } else if (args.Length == 2) 
                {
                    File.Delete(tuldrv.syspath + args[1]);
                    tuldrv.DisplaySuccess();
                } else
                {
                    tuldrv.DisplayError("Invalid use of rm command.", true);
                }
            } else
            {
                tuldrv.DisplayError("File System is Disabled.", true);
            }
        }
        public static void mkdir(string[] args)
        {
            if (!tuldrv.DisableFS)
            {
                if (args.Length == 2)
                {
                    Directory.CreateDirectory(tuldrv.syspath + args[1]);
                    tuldrv.DisplaySuccess();
                }
            } else
            {
                tuldrv.DisplayError("File System is Disabled.", true);
            }
        }
    }
}
