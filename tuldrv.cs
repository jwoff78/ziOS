// Utility Functions

using System;
using System.Collections.Generic;
using System.Text;

namespace ziOS
{
    public class tuldrv
    {
        // System Log.
        private static string syslog = "";
        // Trivial, disable things.
        public static bool DisableSysLog = false;
        public static bool DisableFS = false;
        // The system path.
        public static string syspath = @"0:\";

        // A global random object that any library or program can use.
        public static Random rng = new Random();

        /// <summary>
        /// Draws the Taskbar.
        /// </summary>
        /// <param name="barcolor">The color of the taskbar.</param>
        public static void Bar(ConsoleColor barcolor)
        {
            ConsoleColor oldc = Console.BackgroundColor;
            Console.BackgroundColor = barcolor;
            Console.SetCursorPosition(0, Console.CursorTop);

            Console.Write(new string(' ', 90));

            Console.BackgroundColor = oldc;
            tuldrv.Log("A bar has been displayed on VGA console.", "tuldrv");
        }
        /// <summary>
        /// Draws the Taskbar, with text!
        /// </summary>
        /// <param name="barcolor">The color of the taskbar.</param>
        /// <param name="Ltext">The text in the taskbar.</param>
        /// <param name="LtextColor">The color of the taskbar text.</param>
        public static void Bar(ConsoleColor barcolor, string Ltext, ConsoleColor LtextColor)
        {
            ConsoleColor oldc = Console.BackgroundColor;
            Console.BackgroundColor = barcolor;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', 90));
            Console.SetCursorPosition(0, Console.CursorTop-1);
            ConsoleColor oldlc = Console.ForegroundColor;
            Console.ForegroundColor = LtextColor;
            Console.Write(Ltext);
            Console.ForegroundColor = oldlc;
            Console.BackgroundColor = oldc;
            Console.SetCursorPosition(0, Console.CursorTop + 1);
            tuldrv.Log("A bar has been displayed on VGA console.", "tuldrv");
        }
        
        /// <summary>
        /// Add to system log.
        /// </summary>
        /// <param name="ToBeLogged">What you want to log.</param>
        /// <param name="task">The process that logged it, like the kernel logs with "kernel" task param.</param>
        public static void Log(string ToBeLogged, string task)
        {
            if (!DisableSysLog)
            {
                syslog += $"{task}: {ToBeLogged}\n";
            }
        }
        /// <summary>
        /// Returns the system log.
        /// </summary>
        /// <returns></returns>
        public static string GetLog()
        {
            return syslog;
        }
        /// <summary>
        /// Displays an error with red text and everything. Also logs the error.
        /// </summary>
        /// <param name="errmsg">The error message.</param>
        /// <param name="user">If it's a "userspace" application or not.</param>
        public static void DisplayError(string errmsg, bool user)
        {
            ConsoleColor oldc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red; 
            Console.WriteLine("ERROR: " + errmsg);
            Console.ForegroundColor = oldc;
            Log("error: " + errmsg, "User app");
        }
        /// <summary>
        /// Like DisplayError(), but it's intended for system libraries and does a "silent error".
        /// </summary>
        /// <param name="errmsg">The error message.</param>
        /// <param name="lib">The library that causes this, for example, libmouse.</param>
        public static void DisplayErrorLib(string errmsg, string lib)
        {
            //ConsoleColor oldc = Console.ForegroundColor;
            //Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine("ERROR: " + errmsg);
            //Console.ForegroundColor = oldc;
            Log("error: " + errmsg, lib);
        }

        /// <summary>
        /// Simple menu implementation. You give it a string array, and it will return the option that the user pressed. 9 max.
        /// </summary>
        /// <param name="options">A string array that contains all your options.</param>
        /// <returns></returns>
        public static byte Menu(string[] options)
        {
            Console.WriteLine($"Please choose a valid option from 0 to {options.Length-1}.");
            for (int i = 0;  i < options.Length; i++)
            {
                Console.WriteLine($"{i}. {options[i]}");
            }
            bool ValidOptionChosen = false;
            byte ChosenOption = 255;
            while (!ValidOptionChosen)
            {
                char op = Console.ReadKey(true).KeyChar;
                byte result;
                bool success = byte.TryParse(op.ToString(), out result);
                if (success && result < 10)
                {
                    ValidOptionChosen = true;
                    ChosenOption = result;
                }
            }
            return ChosenOption;
        }
        /// <summary>
        /// A yes/no menu, returns false if user said no, returns true otherwise.
        /// </summary>
        /// <param name="reason">Should be worded better, It appears as "{reason} (Y/N)."</param>
        /// <returns></returns>
        public static bool YNmenu(string reason)
        {
            Console.Write($"{reason} (Y/N)");
            bool ValidOptionChosen = false;
            bool result = false;
            while (!ValidOptionChosen)
            {
                char op = Console.ReadKey(true).KeyChar;
                if (op == 'y')
                {
                    ValidOptionChosen = true;
                    result = true;
                }
                else if (op == 'n')
                {
                    ValidOptionChosen = true;
                    result = false;
                }
            }
            return result;
        }
        /// <summary>
        /// Converts an input string into a hex string. It returns the hex string.
        /// </summary>
        /// <param name="input">What you want to convert to hex.</param>
        /// <returns></returns>
        public static string ConvertToHex(string input)
        {
            byte[] bytes = Encoding.Default.GetBytes(input);

            string hexString = BitConverter.ToString(bytes);
            hexString = hexString.Replace("-", " ");
            return hexString;
        }
        /// <summary>
        /// Just displays "The operation completed successfully.". Useful for commands such as fwrite or mkdir
        /// </summary>
        public static void DisplaySuccess()
        {
            ConsoleColor oldc = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("The operation completed successfully.");
            Console.ForegroundColor = oldc;
        }
        /// <summary>
        /// Centers a string. Returns the centered string.
        /// </summary>
        /// <param name="s">What you want to be centered.</param>
        /// <param name="width">The amount of space you want {s} to be centered in.</param>
        /// <param name="sep">Should be worded better, sep is the chars that go around a string. like '  test  ' or '--test--'.</param>
        /// <returns></returns>
        public static string CenterString(string s, int width, char sep)
        {
            if (s.Length >= width)
            {
                return s;
            }

            int leftPadding = (width - s.Length) / 2;
            int rightPadding = width - s.Length - leftPadding;
            /*if (width % 2 == 1)
            {
                s += sep;
            }*/// Removed because it causes problems
            return new string(sep, leftPadding) + s + new string(sep, rightPadding);
        }
    }
}
