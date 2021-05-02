using System;

namespace CAB301_ToolLibrarySystem
{
    class Program
    {
        static ToolLibrarySystem ToolSystem = new ToolLibrarySystem();
        static Member LoggedInAs;

        static void Main(string[] args)
        {
            ToolSystem.Members.add(new Member("Thomas", "Fabian", "01189998819991197253", "1234"));
            ToolSystem.Members.add(new Member("samohT", "naibaF", "35279119991889998110", "4321"));
            MainMenu();
        }

        static void MainMenu()
        {
            /*  Members are verified using their first name, last name and password:
             *      staff username/pass = staff/today123
             */
            Console.Clear();
            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("Main Menu".PadSides(31, '='));

            Console.WriteLine("1. Staff Login");
            Console.WriteLine("2. Member Login");
            Console.WriteLine("0. Exit");

            Console.WriteLine(new String('=', 31));

            Console.WriteLine("\nPlease make a selection (1-2, or 0 to exit):");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            ConsoleKey key = keyInfo.Key;

            switch(key)
            {
                case ConsoleKey.D1: StaffLogin(); return;
                case ConsoleKey.D2: MemberLogin(); return;
                case ConsoleKey.D0: return;
                default: MainMenu(); return;
            }
        }

        static void StaffLogin()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("Staff Login".PadSides(31, '='));
            Console.Write("Enter Username:".PadRight(18));
            if (!StringLib.ReadInput(out string username, 0, 15, flags:StringLib.InputFlags.Alpha))
                return;
            Console.Write("Enter Password:".PadRight(18));
            if (!StringLib.ReadInput(out string password, 0, 15, flags: StringLib.InputFlags.Password))
                return;

            if (username == "staff" && password == "today123")
                StaffMenu();
            else
            {
                Console.WriteLine("\nInvalid username or password. (0 to exit or any key to try again)");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D0:MainMenu(); return;
                    default: StaffLogin(); return;
                }
            }
        }

        static void MemberLogin()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("Member Login".PadSides(31, '='));
            Console.Write("Enter First Name:".PadRight(18));
            if (!StringLib.ReadInput(out string firstName, 1, 15, flags:StringLib.InputFlags.Alpha))
                return;
            Console.Write("Enter Last Name:".PadRight(18));
            if (!StringLib.ReadInput(out string lastName, 1, 15, flags: StringLib.InputFlags.Alpha))
                return;
            Console.Write("Enter PIN:".PadRight(18));
            if (!StringLib.ReadInput(out string pin, 4, 4, flags: StringLib.InputFlags.Numbers))
                return;

            Member member = (Member)ToolSystem.Members.get(firstName, lastName, pin);

            if (member != null)
                MemberMenu(member);
            else
            {
                Console.WriteLine("\nInvalid login details. (0 to exit or any key to try again)");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D0: MainMenu(); return;
                    default: MemberLogin(); return;
                }
            }
        }

        static void StaffMenu()
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("Staff Menu".PadSides(31, '='));

            Console.WriteLine("1. Add a new tool");
            Console.WriteLine("2. Add new pieces of an existing tool");
            Console.WriteLine("3. Remove some pieces of a tool");
            Console.WriteLine("4. Register a new member");
            Console.WriteLine("5. Remove a member");
            Console.WriteLine("6. Find the contact number of a member");
            Console.WriteLine("0. Return to main menu");

            Console.WriteLine(new string('=', 31));

            Console.WriteLine("\nPlease make a selection (1-6, or 0 to return to main menu):");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1: AddNewTool(); break;
                case ConsoleKey.D2: AddToExistingTool(); break;
                case ConsoleKey.D3: RemoveFromExistingTool(); break;
                case ConsoleKey.D4: RegisterNewMember(); break;
                case ConsoleKey.D5: RemoveMember(); break;
                case ConsoleKey.D6: FindContactNumber(); break;
                case ConsoleKey.D0: MainMenu(); return;
                default: StaffMenu(); return;
            }

            void AddNewTool()
            {
                // SELECT CATEGORY
                Console.Clear();
                Console.WriteLine("Welcome to the Tool Library");
                Console.WriteLine("Staff Menu - New Tool".PadSides(31, '='));
                Console.WriteLine("Select new Tool Category");
                if (!StringLib.DoSelectFromEnumType(typeof(Tools.Categories), out int category))
                    return;
                
                // SELECT SUB-CATEGORY
                Console.Clear();
                Console.WriteLine("Welcome to the Tool Library");
                Console.WriteLine("Staff Menu - New Tool".PadSides(31, '='));
                Console.WriteLine("Select new Tool Sub-Category");
                if (!StringLib.DoSelectFromEnumType(Tools.SubCategory(category), out int subType))
                    return;

                // ENTER TOOL NAME
                Console.Clear();
                Console.WriteLine("Welcome to the Tool Library");
                Console.WriteLine("Staff Menu - New Tool".PadSides(31, '='));
                Console.Write("Enter new Tool Name:");
                if (!StringLib.ReadInput(out string name, maxLength:20, flags:StringLib.InputFlags.TextEntry | StringLib.InputFlags.Numbers))
                    return;

                // CREATE NEW TOOL OBJECT AND ADD TO SYSTEM
                string path = string.Format("{0}{3}{1}{3}{2}", category, subType, name, StringLib.DELIMITER);

                Console.WriteLine(path);
                Console.ReadKey();

                ToolSystem.add(new Tool(path, 0));
            }

            void AddToExistingTool()
            {

            }

            void RemoveFromExistingTool()
            {

            }

            void RegisterNewMember()
            {

            }

            void RemoveMember()
            {

            }

            void FindContactNumber()
            {

            }
        }

        static void MemberMenu(Member member)
        {
            Console.Clear();

            Console.WriteLine("Welcome to the Tool Library");
            Console.WriteLine("Member Menu".PadSides(31, '='));

            Console.WriteLine("1. Display all the tools of a tool type");
            Console.WriteLine("2. Borrow a tool");
            Console.WriteLine("3. Return a tool");
            Console.WriteLine("4. List all the tools that I am renting");
            Console.WriteLine("5. Display top three (3) most frequently rented tools");
            Console.WriteLine("0. Return to main menu");

            Console.WriteLine(new string('=', 31));

            Console.WriteLine("\nPlease make a selection (1-5, or 0 to return to main menu):");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1: DisplayToolsOfType(); break;
                case ConsoleKey.D2: BorrowTool(); break;
                case ConsoleKey.D3: ReturnTool(); break;
                case ConsoleKey.D4: ListMyRentedTools(); break;
                case ConsoleKey.D5: ListMyRentedTools(); break;
                case ConsoleKey.D6: DisplayTopThreeRentedTools(); break;
                case ConsoleKey.D0: MainMenu(); return;
            }

            MemberMenu(member); // Refresh Menu

            void DisplayToolsOfType()
            {

            }

            void BorrowTool()
            {

            }

            void ReturnTool()
            {

            }

            void ListMyRentedTools()
            {

            }

            void DisplayTopThreeRentedTools()
            {

            }
        }
    }
}
