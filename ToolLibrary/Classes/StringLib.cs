using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace CAB301_ToolLibrarySystem
{
    public static class StringLib
    {
        public const char DELIMITER = '\t';
        /// <summary>
        /// Returns a new string center-aligned with some padding character on the left and right sides.
        /// </summary>
        /// <param name="str">String to pad</param>
        /// <param name="width">Width of padding</param>
        /// <param name="paddingChar">Char to pad</param>
        /// <returns>A new string center-aligned with some padding character on the left and right sides</returns>
        public static string PadSides(this string str, int width, char paddingChar = ' ')
        {
            
            int padding = width - str.Length;
            int padLeft = padding / 2 + str.Length;

            return str.PadLeft(padLeft, paddingChar).PadRight(width, paddingChar);
        }

        /// <summary>
        /// Use Keyboard input to select an item from within some enum of <see cref="Type"/> enumType
        /// </summary>
        /// <param name="enumType">Type of enum to be parsed.</param>
        /// <param name="index">Reference variable to output result.</param>
        /// <returns>Returns true on success, false otherwise</returns>
        public static bool DoSelectFromEnumType(Type enumType, out int index)
        {
            // PRINT COLLECTION OPTIONS
            string[] categories = Enum.GetNames(enumType);
            for (int i = 0; i < categories.Length; i++)
            {
                string name = categories[i];
                string spacedName = Regex.Replace(name, "(?<=[a-zA-Z])(?=[A-Z])", " ");
                Console.WriteLine(string.Format("{0}. {1}", i + 1, spacedName));
            }
            Console.WriteLine("0. Exit");

            // SELECT COLLECTION OPTION
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                if (key >= ConsoleKey.D1 && key <= (ConsoleKey.D1 + categories.Length))
                {
                    index = key - ConsoleKey.D1;
                    return true;
                }
                else if (key == ConsoleKey.D0)
                {
                    index = -1;
                    return false;
                }
            }
        }

        /// <summary>
        /// Similar to Console.ReadLine() except adds Escape clause, string length bounds and masking capabilities. Returns true on successful input.
        /// </summary>
        /// <param name="input">Reference string to store output string.</param>
        /// <param name="minLength">Minimum submission length</param>
        /// <param name="maxLength">Maximum submission length</param>
        /// <param name="showInput">Show or mask user input (mask for password entry)</param>
        /// <returns>Returns true if input passes constraints on <see cref="ConsoleKey.Enter"/>. False if pressed <see cref="ConsoleKey.Escape"/></returns>
        public static bool ReadInput(out string input, int minLength = 0, int maxLength = int.MaxValue, bool showInput = true, InputFlags flags = InputFlags.AllowAll)
        {
            input = "";
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                switch (keyInfo.Key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine("");
                        return false;
                    case ConsoleKey.Enter when minLength <= input.Length && input.Length <= maxLength:
                        Console.WriteLine("");
                        return true;
                    case ConsoleKey.Backspace when input.Length > 0:
                        if (showInput) Console.Write("\b \b"); 
                        input = input[0..^1];
                        break;
                    default:
                        if ((flags.HasFlag(InputFlags.Alpha) && char.IsLetter(keyInfo.KeyChar)) ||
                            (flags.HasFlag(InputFlags.Numbers) && char.IsNumber(keyInfo.KeyChar)) ||
                            (flags.HasFlag(InputFlags.Symbols) && char.IsSymbol(keyInfo.KeyChar)) ||
                            (flags.HasFlag(InputFlags.WhiteSpace) && keyInfo.KeyChar == ' ') ||
                            (flags.HasFlag(InputFlags.Punctuation) && char.IsPunctuation(keyInfo.KeyChar)))
                        {
                            if (input.Length < maxLength)
                            {
                                if (showInput)
                                    Console.Write(keyInfo.KeyChar);
                                else
                                    Console.Write("*");
                                input += keyInfo.KeyChar;
                            }
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Flags identifying which characters may be accepted when using <see cref="ReadInput(out string, int, int, bool, InputFlags)"/>.
        /// </summary>
        public enum InputFlags
        {
            /// <summary>Check input against <see cref="char.IsLetter(char)"/></summary>
            Alpha = 1 << 1,
            /// <summary>Check input against <see cref="char.IsNumber(char)"/></summary>
            Numbers = 1 << 2,
            /// <summary>Check input against <see cref="char.IsSymbol(char)"/></summary>
            Symbols = 1 << 4,
            /// <summary>Check input against <see cref="char.IsWhiteSpace(char)"/></summary>
            WhiteSpace = 1 << 6,
            /// <summary>Check input against <see cref="char.IsPunctuation(char)"/></summary>
            Punctuation = 1 << 8,
            /// <summary>Combination if Alpha and Numeric.</summary>
            AlphaNumeric = Alpha | Numbers,
            /// <summary>Combination if Alpha, White Space and Punctuation flags.</summary>
            TextEntry = Alpha | WhiteSpace | Punctuation,
            /// <summary>Combination if Alpha, White Space and Punctuation flags.</summary>
            Password = Alpha | Numbers | Symbols | Punctuation,
            /// <summary>Combination of all flags.</summary>
            AllowAll = Alpha | Numbers | WhiteSpace | Symbols | Punctuation,
        }
    }
}
