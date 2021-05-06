using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    public static class ConsoleLib
    {
        public const char DELIMITER = '\t';
        public const int WIDTH = 50;

        /// <summary>
        /// Dictionary containing Tool Categories and their Tool Type designations.
        /// </summary>
        public static Dictionary<string, string[]> Tools = new Dictionary<string, string[]>
        {
            {
                "Gardening Tools", new string[]
                {
                    "Line Trimmers",
                    "Lawn Mowers",
                    "Hand Tools",
                    "Wheelbarrows",
                    "Garden Power Tools"
                }
            },
            {
                "Flooring Tools", new string[]
                {
                    "Scrapers",
                    "Floor Lasers",
                    "Floor Levelling Tools",
                    "Floor Levelling Materials",
                    "Floor Hand Tools",
                    "Tiling Tools"
                }
            },
            {
                "Fencing Tools", new string[]
                {
                    "Hand Tools",
                    "Electric Fencing",
                    "Steel Fencing Tools",
                    "Power Tools",
                    "Fencing Accessories"
                }
            },
            {
                "Measuring Tools", new string[]
                {
                    "Distance Tools",
                    "Laser Measurer",
                    "Measuring Jugs",
                    "Temperature & Humidity Tools",
                    "Levelling Tools",
                    "Markers"
                }
            },
            {
                "Cleaning Tools", new string[]
                {
                    "Draining Tools",
                    "Car Cleaning",
                    "Vacuum",
                    "Pressure Cleaners",
                    "Pool Cleaning",
                    "Floor Cleaning"
                }
            },
            {
                "Painting Tools", new string[]
                {
                    "Sanding Tools",
                    "Brushes",
                    "Rollers",
                    "Paint Removal Tools",
                    "Paint Scrapers",
                    "Sprayers"
                }
            },
            {
                "Electronic Tools", new string[]
                {
                    "Voltage Tester",
                    "Oscilloscopes",
                    "Thermal Imaging",
                    "Data Test Tool",
                    "Insulation Testers"
                }
            },
            {
                "Electricity Tools", new string[]
                {
                    "Test Equipment",
                    "Safety Equipment",
                    "Basic Hand Tools",
                    "Circuit Protection",
                    "Cable Tools"
                }
            },
            {
                "Automotive Tools", new string[]
                {
                    "Jacks",
                    "Air Compressors",
                    "Battery Chargers",
                    "Socket Tools",
                    "Braking",
                    "Drive train"
                }
            }
        };

        public static string[] Categories => Tools.Keys.ToArray();
        public static string[] ToolTypes(string category) => Tools[category];

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
        /// Performs a GUI Selection query from 1 to array.Length using numeric keys.
        /// </summary>
        /// <param name="item">Selected item of type T to be output</param>
        /// <param name="array">Array of type T to query</param>
        /// <returns>Returns true on successful query. Returns false if exited.</returns>
        public static bool SelectFromArray<T>(out T item, T[] array)
        {
            item = default;

            for (int i = 1; i <= array.Length; i++)
                Console.WriteLine(string.Format("{0}. {1}", i, array[i-1].ToString()));

            Console.WriteLine(string.Format("\nPlease make a selection (1-{0} or [Escape] to exit)", array.Length));

            while (true)
            {
                Console.Write("Enter Selection: ");
                if (ReadInput(out string input, 1, 2, InputFlags.Numbers))
                {
                    int i = int.Parse(input) - 1; // Take 1 as arrays are begin at 0.
                    if (i <= array.Length)
                    {
                        item = array[i];
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("\nError: Invalid Input. Please try again.");
                        return false;
                    }
                }
            }
        }

        /// <summary>
        /// Performs a GUI Selection query from 1 to array.Length using numeric keys.
        /// </summary>
        /// <param name="item">Selected item of type T to be output</param>
        /// <param name="array">Array of type T to query</param>
        /// <returns>Returns true on successful query. Returns false if exited.</returns>
        public static bool SelectFromDictionary<T>(out T item, Dictionary<string,T> dict)
        {
            item = default;
            string[] keys = dict.Keys.ToArray();
            T[] vals = dict.Values.ToArray();

            for (int i = 1; i <= keys.Length; i++)
                Console.WriteLine(string.Format("{0}. {1}", i.ToString().PadLeft(5), keys[i - 1].ToString()));

            Console.WriteLine(string.Format("\nPlease make a selection (1-{0} or [Escape] to exit)", keys.Length));

            while (true)
            {
                Console.Write("Enter selection: ");
                if (ReadInput(out string input, 1, 2, InputFlags.Numbers))
                {
                    int i = int.Parse(input) - 1; // Take 1 as arrays are begin at 0.
                    if (i <= keys.Length)
                    {
                        item = vals[i];
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("\nError: Invalid Input. Please try again.");
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Helper func to select tool category
        /// </summary>
        public static bool SelectToolCategory(out string category)
        {
            return SelectFromArray(out category, Categories);
        }

        /// <summary>
        /// Helper func to select tool type
        /// </summary>
        public static bool SelectToolType(out string toolType, string category)
        {
            return SelectFromArray(out toolType, ToolTypes(category));
        }

        /// <summary>
        /// Clears console and prints title bar.
        /// </summary>
        /// <param name="menuTitle">Menu Title bar text</param>
        public static void StartMenu(string menuTitle)
        {
            Console.Clear();
            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine(menuTitle.PadSides(WIDTH, ' '));
            Console.WriteLine(new String('=', WIDTH));
        }

        public static void KeyWait(string message = "Press [Any Key] to continue...")
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }

        /// <summary>
        /// Similar to Console.ReadLine() except adds Escape clause, string length bounds and masking capabilities. Returns true on successful input.
        /// </summary>
        /// <param name="input">Reference string to store output string.</param>
        /// <param name="minLength">Minimum submission length</param>
        /// <param name="maxLength">Maximum submission length</param>
        /// <param name="showInput">Show or mask user input (mask for password entry)</param>
        /// <param name="flags"><see cref="InputFlags"/> that determine allowable characters within the input field.</param>
        /// <returns>Returns <c>True</c> if entry input passes constraints on <see cref="ConsoleKey.Enter"/>. <c>False</c> if entry cancelled.</returns>
        public static bool ReadInput(out string input, int minLength = 0, int maxLength = int.MaxValue, InputFlags flags = InputFlags.AllowAll, bool showInput = true)
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
                            (flags.HasFlag(InputFlags.WhiteSpace) && keyInfo.KeyChar == ' ') || // No tabs!
                            (flags.HasFlag(InputFlags.Punctuation) && char.IsPunctuation(keyInfo.KeyChar)))
                        {
                            if (input.Length <= maxLength)
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
            /// <summary>Compares input against <see cref="char.IsLetter(char)"/></summary>
            Alpha = 1 << 1,
            /// <summary>Compares input against <see cref="char.IsNumber(char)"/></summary>
            Numbers = 1 << 2,
            /// <summary>Compares input against <see cref="char.IsSymbol(char)"/></summary>
            Symbols = 1 << 4,
            /// <summary>Compares input against <see cref="char.IsWhiteSpace(char)"/></summary>
            WhiteSpace = 1 << 6,
            /// <summary>Compares input against <see cref="char.IsPunctuation(char)"/></summary>
            Punctuation = 1 << 8,
            /// <summary>Combination of <see cref="Alpha"/> and <see cref="Numbers"/> flags.</summary>
            AlphaNumeric = Alpha | Numbers,
            /// <summary>Combination of <see cref="AlphaNumeric"/>, <see cref="WhiteSpace"/> and <see cref="Punctuation"/> flags.</summary>
            TextEntry = AlphaNumeric | WhiteSpace | Punctuation,
            /// <summary>Combination of <see cref="AlphaNumeric"/>, <see cref="Symbols"/> and <see cref="Punctuation"/> flags.</summary>
            Password = AlphaNumeric | Symbols | Punctuation,
            /// <summary>Combination of all flags.</summary>
            AllowAll = AlphaNumeric | WhiteSpace | Symbols | Punctuation,
        }
    }
}
