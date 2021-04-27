using System;

namespace Assessment1_ToolLibrary
{
    class Program
    {
        ToolLibrarySystem System = new ToolLibrarySystem();

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            /*  Members are verified using their first name, last name and password:
             *      staff username/pass = staff/today123
             */
            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("Main Menu".PadSides(31, '='));

            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");

            Console.WriteLine(new String('=', 31));

            Console.WriteLine("\nPlease make a selection (1-2, or 0 to exit):");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;

            switch(key)
            {
                case ConsoleKey.D1: break;
                case ConsoleKey.D2: break;
                case ConsoleKey.D0: return;
                default: MainMenu(); return;
            }
                
        }

        static void StaffMenu()
        {
            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("Staff Menu".PadSides(31, '='));

            Console.WriteLine("1. Add a new tool");
            Console.WriteLine("2. Add new pieces of an existing tool");
            Console.WriteLine("3. Remove some pieces of a tool");
            Console.WriteLine("4. Register a new member");
            Console.WriteLine("5. Remove a member");
            Console.WriteLine("6. Find the contact number of a member");
            Console.WriteLine("0. Return to main menu");

            Console.WriteLine(new String('=', 31));

            Console.WriteLine("\nPlease make a selection (1-6, or 0 to return to main menu):");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.D1: break;
                case ConsoleKey.D2: break;
                case ConsoleKey.D3: break;
                case ConsoleKey.D4: break;
                case ConsoleKey.D5: break;
                case ConsoleKey.D6: break;
                case ConsoleKey.D0: return;
                default: StaffMenu(); return;
            }
        }

        static void MemberMenu()
        {
            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("Member Menu".PadSides(31, '='));

            Console.WriteLine("1. Display all the tools of a tool type");
            Console.WriteLine("2. Borrow a tool");
            Console.WriteLine("3. Return a tool");
            Console.WriteLine("4. List all the tools that I am renting");
            Console.WriteLine("5. Display top three (3) most frequently rented tools");
            Console.WriteLine("0. Return to main menu");

            Console.WriteLine(new String('=', 31));

            Console.WriteLine("\nPlease make a selection (1-5, or 0 to return to main menu):");

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            ConsoleKey key = keyInfo.Key;

            switch (key)
            {
                case ConsoleKey.D1: break;
                case ConsoleKey.D2: break;
                case ConsoleKey.D3: break;
                case ConsoleKey.D4: break;
                case ConsoleKey.D5: break;
                case ConsoleKey.D6: break;
                case ConsoleKey.D0: return;
                default: MemberMenu(); return;
            }
        }
    }
}
