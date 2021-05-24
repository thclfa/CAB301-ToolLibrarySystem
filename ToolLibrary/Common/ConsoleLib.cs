using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    /// <summary>
    /// Console Library that assists in GUI functionality
    /// </summary>
    public static class ConsoleLib
    {
        public static int WIDTH => Console.WindowWidth;

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
        /// Performs a GUI Selection query from 1 to array.Length using numeric keys.
        /// </summary>
        /// <param name="item">Selected item of type T to be output</param>
        /// <param name="array">Array of type T to query</param>
        /// <returns>Returns true on successful query. Returns false if exited.</returns>
        public static bool SelectFromArray<T>(out T item, T[] array)
        {
            item = default;

            // Loop print each tool
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine($"{i + 1,5}. {array[i]}");

            Console.WriteLine($"\nPlease make a selection (1-{array.Length} or [Escape] to exit)");

            // Selection loop
            while (true)
            {
                if (ReadInput("Enter Selection: ", out string input, 1, 2, InputFlags.Numbers))
                {
                    int i = int.Parse(input) - 1; // Take 1 as arrays are begin at 0.

                    // Can't accept inputs above the allowable indices
                    if (i < array.Length)
                    {
                        item = array[i];
                        return true;
                    }
                    else
                    {
                        KeyWait("Invalid selection, try again.");
                    }
                }
                else return false;
            }
        }

        /// <summary>
        /// Performs a GUI Selection query from 1 to array.Length using numeric keys.
        /// </summary>
        /// <param name="item">Selected item of type T to be output</param>
        /// <param name="dict">Dictionary of type T to query</param>
        /// <returns>Returns true on successful query. Returns false if exited.</returns>
        public static bool SelectFromDictionary<T>(out T item, Dictionary<string,T> dict)
        {
            item = default;
            string[] keys = dict.Keys.ToArray();
            T[] vals = dict.Values.ToArray();

            for (int i = 0; i < keys.Length; i++)
                Console.WriteLine($"{i + 1,5}. {keys[i]}");

            Console.WriteLine($"\nPlease make a selection (1-{keys.Length} or [Escape] to exit)");

            while (true)
            {
                if (ReadInput("Enter selection: ", out string input, 1, 2, InputFlags.Numbers))
                {
                    int i = int.Parse(input) - 1;
                    if (i < keys.Length)
                    {
                        item = vals[i];
                        return true;
                    }
                    else
                    {
                        KeyWait("Invalid selection, try again.");
                    }
                }
                else return false;
            }
        }

        /// <summary>
        /// Helper func to select tool category
        /// </summary>
        public static bool SelectToolCategory(out string category)
        {
            Console.WriteLine($"\n{"Select Tool Category".Pad(' ')}\n" +
                              $"{'-'.Mul(WIDTH)}\n");
            return SelectFromArray(out category, Categories);
        }

        /// <summary>
        /// Helper func to select tool type
        /// </summary>
        public static bool SelectToolType(out string toolType, string category)
        {
            Console.WriteLine($"\n{"Select Tool Type".Pad(' ')}\n" +
                              $"{'-'.Mul(WIDTH)}\n");
            return SelectFromArray(out toolType, ToolTypes(category));
        }

        /// <summary>
        /// Clears console and prints title bar.
        /// </summary>
        /// <param name="title">Menu Title bar text</param>
        public static void PrintMenuHeader(string title)
        {
            Console.Clear();
            Console.WriteLine($"Welcome to the Tool Library\n" +
                $"{title.Pad(' ')}\n" +
                $"{'='.Mul(WIDTH)}\n");
        }

        /// <summary>
        /// Prints the sub menu header with underline
        /// </summary>
        /// <param name="title"></param>
        public static void PrintMenuSubHeader(string title)
        {
            Console.WriteLine(
                $"\n{title.Pad(' ')}\n" +
                $"{'-'.Mul(WIDTH)}");
        }


        /// <summary>
        /// Prints some message and waits for any key input before continuing.
        /// </summary>
        /// <param name="message"></param>
        public static void KeyWait(string message = "")
        {
            Console.WriteLine($"\n{message} [Any Key] to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Returns a new string center-aligned padded with <see cref="Console.WindowWidth"/> number of characters.
        /// </summary>
        /// <param name="str">String to pad</param>
        /// <param name="paddingChar">Char to pad</param>
        /// <returns>A new string center-aligned with some padding character on the left and right sides</returns>
        public static string Pad(this string str, char paddingChar = ' ')
        {
            return str.Pad(Console.WindowWidth, paddingChar);
        }

        /// <summary>
        /// Returns a new string center-aligned with some padding character on the left and right sides.
        /// </summary>
        /// <param name="str">String to pad</param>
        /// <param name="width">Width of padding</param>
        /// <param name="paddingChar">Char to pad</param>
        /// <returns>A new string center-aligned with some padding character on the left and right sides</returns>
        public static string Pad(this string str, int width, char paddingChar = ' ')
        {
            int padding = width - str.Length;
            int padLeft = padding / 2 + str.Length;
            return str.PadLeft(padLeft, paddingChar).PadRight(width, paddingChar);
        }

        /// <summary>
        /// Multiplies a char i times to create a longer string.
        /// </summary>
        /// <param name="chr">char to be multiplied</param>
        /// <param name="i">Coefficient</param>
        /// <returns>New string of length i</returns>
        public static string Mul(this char chr, int i)
        {
            return new string(chr, i);
        }

        /// <summary>
        /// Similar to Console.ReadLine() except adds Escape clause, string length bounds and masking capabilities. Returns true on successful input.
        /// </summary>
        /// <param name="value">Reference string to store output string.</param>
        /// <param name="minLength">Minimum submission length</param>
        /// <param name="maxLength">Maximum submission length</param>
        /// <param name="showInput">Show or mask user input (mask for password entry)</param>
        /// <param name="flags"><see cref="InputFlags"/> that determine allowable characters within the input field.</param>
        /// <returns>Returns <c>True</c> if entry input passes constraints on <see cref="ConsoleKey.Enter"/>. <c>False</c> if entry cancelled.</returns>
        public static bool ReadInput(string question, out string value, int minLength = 0, int maxLength = int.MaxValue, InputFlags flags = InputFlags.AllowAll, bool showInput = true)
        {
            value = "";
            Console.Write(question);
            while (true)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                ConsoleKey key = keyInfo.Key;
                char keyChar = keyInfo.KeyChar;

                switch (key)
                {
                    case ConsoleKey.Escape:
                        Console.WriteLine();
                        return false;
                    case ConsoleKey.Enter when minLength <= value.Length && value.Length <= maxLength:
                        Console.WriteLine();
                        return true;
                    case ConsoleKey.Backspace when value.Length > 0:
                        Console.Write("\b \b"); // Backspace console letters
                        value = value[0..^1];
                        break;
                    default:
                        if ((flags.HasFlag(InputFlags.Alpha) && char.IsLetter(keyChar)) ||
                            (flags.HasFlag(InputFlags.Numbers) && char.IsNumber(keyChar)) ||
                            (flags.HasFlag(InputFlags.Symbols) && char.IsSymbol(keyChar)) ||
                            (flags.HasFlag(InputFlags.WhiteSpace) && keyChar == ' ') || // No tabs!
                            (flags.HasFlag(InputFlags.Punctuation) && char.IsPunctuation(keyChar)))
                        {
                            if (value.Length < maxLength)
                            {
                                Console.Write(showInput ? keyChar : '*');
                                value += keyChar;
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
