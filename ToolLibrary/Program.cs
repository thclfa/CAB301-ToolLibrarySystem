using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Linq.Expressions;

namespace CAB301_ToolLibrarySystem
{
    public class Program
    {
        private static ToolLibrarySystem LibrarySystem;
        private static MemberCollection Members;
        private static Dictionary<string, Dictionary<string, ToolCollection>> ToolCollections;

        static void Main(string[] args)
        {
            Members = new MemberCollection();
            ToolCollections = new Dictionary<string, Dictionary<string, ToolCollection>>();

            // Populate ToolCollections
            foreach( KeyValuePair<string, string[]> category in ConsoleLib.Tools)
            {
                var collection = new Dictionary<string, ToolCollection>();

                foreach (string toolType in category.Value)
                    collection.Add(toolType, new ToolCollection());

                ToolCollections.Add(category.Key, collection);
            }


            LibrarySystem = new ToolLibrarySystem(ref Members, ref ToolCollections);

            RunUnitTests(); // Perform unit testing to validate the implementation
            //InsertTestData(); // Insert some test data into the ToolLibrarySystem for manual testing
            //MainMenu(); // Main Console entry point
        }

        /// <summary>
        /// Runs Unit tests on all functionality of the ToolLibrarySystem, not included in the 
        /// main program, for debug purposes only.
        /// </summary>
        static void RunUnitTests()
        {
            // Just for calculating total pass/fails
            List<bool> tests = new List<bool>();

            // This function performs individual unit tests taking some object and a predicate
            void UnitTest<T>(T obj, Func<T, bool> predicate, string testName, string expected = "")
            {
                bool result = predicate.Invoke(obj);
                tests.Add(result);

                Console.Write($" {tests.Count(),-2} [");

                if (result)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("PASS");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("FAIL");
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"] {testName,-35} {expected}");
            }

            ConsoleLib.PrintMenuHeader("Running Unit Tests");

            Console.WriteLine($" {"ID",-2} RESULT {"TEST NAME",-35} EXPECTED OUTCOME\n{'-'.Mul(Console.WindowWidth)}");

            // Dummy tests
            //UnitTest(10, x => x + 10 == 20, "10 + 10", "20");
            //UnitTest(10, x => x - 10 == 20, "10 - 10", "20 (This test was designed to fail)");

            Console.WriteLine("Creation Test");

            // NEW MEMBER TEST
            Member member = new Member("Joe", "Blogs", "0412345678", "1234");
            LibrarySystem.add(member);
            string username = member.FirstName + member.LastName;
            string fullName = member.FirstName + " " + member.LastName;
            UnitTest(member, x => GetMember(username) != null, $"Add new Member '{fullName}'", $"'{fullName}' has been created.");

            // NEW TOOL TEST
            Tool tool = new Tool("Hand Saw", 0);
            string toolName = tool.Name;
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(tool);
            UnitTest(tool, x => GetTool(toolName) != null, $"Add new Tool '{toolName}'", $"'{toolName}' has been created.");

            Console.WriteLine("Unsafe Borrow Test");

            // Unsafe Borrow Tool Test
            LibrarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 0 && tool.GetBorrowers.toArray().Length == 0, $"Try Borrow '{toolName}'", $"'{toolName}' not borrowed as there is 0x Quantity");

            Console.WriteLine("Quantity Test");

            // Quantity Add Test
            LibrarySystem.add(tool, 20);
            UnitTest(tool, x => x.Quantity == 20, $"Add 20x of '{toolName}'", $"'{toolName}' Quantity is 20");
            // Quantity Remove Test
            LibrarySystem.delete(tool, 10);
            UnitTest(tool, x => x.Quantity == 10, $"Del 10x of '{toolName}'", $"'{toolName}' Quantity is 10");

            Console.WriteLine("Safe Borrow Test");

            // Safe Borrow Tool Test
            LibrarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 1 && tool.GetBorrowers.toArray().Length == 1, $"Borrow 1st '{toolName}'", $"'{fullName}' has 1 tools and '{toolName}' has 1 borrower");
            LibrarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 2 && tool.GetBorrowers.toArray().Length == 1, $"Borrow 2nd '{toolName}'", $"'{fullName}' has 2 tools and '{toolName}' has 1 borrower");
            LibrarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 3 && tool.GetBorrowers.toArray().Length == 1, $"Borrow 3rd '{toolName}'", $"'{fullName}' has 3 tools and '{toolName}' has 1 borrower");
            LibrarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 3, $"Try Borrow 4th '{toolName}'", $"'{fullName}' has 3 tools and '{toolName}' not borrowed");

            Console.WriteLine("Unsafe Deletion Test");

            // Quantity Remove Test
            LibrarySystem.delete(tool, 10);
            UnitTest(tool, x => x.Quantity == 10, $"Try Delete 10x of '{toolName}'", "Unchanged Quantity as can't delete borrowed tools");
            // Delete Member while Borrowing
            LibrarySystem.delete(member);
            UnitTest(member, x => GetMember(username) == member, $"Try Delete Member '{fullName}'", $"'{fullName}' still exists as is still borrowing tools");
            // Delete Tool while Borrowing
            LibrarySystem.delete(tool);
            UnitTest(tool, x => GetTool(toolName) == tool, $"Try Delete Tool '{toolName}'", $"'{toolName}' still exists as it is still being borrowed");

            Console.WriteLine("Return Test");

            // Member Return Tools
            LibrarySystem.returnTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 2 && tool.GetBorrowers.toArray().Length == 1, $"Return 1st '{toolName}'", $"'{fullName}' has 2 tools and '{toolName}' has 1 borrower");
            LibrarySystem.returnTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 1 && tool.GetBorrowers.toArray().Length == 1, $"Return 2nd '{toolName}'", $"'{fullName}' has 1 tools and '{toolName}' has 1 borrower");
            LibrarySystem.returnTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 0 && tool.GetBorrowers.toArray().Length == 0, $"Return 3rd '{toolName}'", $"'{fullName}' has 0 tools and '{toolName}' has no borrower");
            LibrarySystem.returnTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 0, $"Try Return 4th Tool '{toolName}'", $"No '{toolName}' to be returned.");


            Console.WriteLine("Safe Deletion Test");

            // Delete Member
            username = member.FirstName + member.LastName;
            LibrarySystem.delete(member);
            UnitTest(member, x => GetMember(username) == null, $"Delete the Member '{fullName}'", $"'{fullName}' no longer exists");
            // Delete Tool
            LibrarySystem.delete(tool);
            UnitTest(tool, x => GetTool(toolName) == null, $"Delete the Tool '{toolName}'", $"'{toolName}' no longer exists");

            Console.WriteLine("Merge Sort Test");

            // Test Merge Sort
            int[] testSort = new int[] { 50, 30, 20, 45, 10, 1, 5 };
            int[] sorted = testSort.MergeSort();
            int[] expected = new int[] { 1, 5, 10, 20, 30, 45, 50 };
            UnitTest(sorted, x => x.SequenceEqual(expected), $"Sort array [{string.Join(',', testSort)}]", $"Sorted array [{string.Join(',', expected)}]");

            // PRINT RESULT
            int passCount = tests.Where(x => x == true).Count();
            int testCount = tests.Count();
            Console.WriteLine($"\nPassed {passCount} of {testCount} tests...");
            Console.ReadKey();
        }

        /// <summary>
        /// Add some dummy data to the member/tool collections for testing purposes
        /// </summary>
        static void InsertTestData()
        {
            LibrarySystem.add(new Member("a", "a", "1234567", "1111"));
            LibrarySystem.add(new Member("Thomas", "Fabian", "N10582835", "1234"));
            LibrarySystem.add(new Member("Maurice", "Moss", "0118 999 88199 9119 725 3", "1234"));
            LibrarySystem.add(new Member("Lowly", "Apprentice", "0432 384 542", "1111"));
            LibrarySystem.add(new Member("Gone", "Fishing", "0448 123 952", "1111"));

            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Silent Bell", 32));
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Left-Handed Hammer", 4));
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Spirit-Level Bubble", 2));
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Sky Hooks", 50));
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Skirting Board Ladder", 9));
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Bucket of Steam", 10));
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Glass Hammer", 10));
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("A Long Weight", 1000));
            ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Tin of Striped Paint", 15));
        }

        /// <summary>
        /// Use Console input to traverse Category/ToolType menus to return a Tool
        /// </summary>
        /// <param name="tool">Tool selected</param>
        /// <returns>Returns True if selection successful, false if input cancelled</returns>
        static bool ConsoleSelectTool(out Tool tool)
        {
            if (ConsoleLib.SelectToolCategory(out string category))
                if (ConsoleLib.SelectToolType(out string toolType, category))
                {
                    ToolCollection collection = ToolCollections[category][toolType];
                    Tool[] tools = collection.toArray();

                    Console.WriteLine($"\n" +
                        $"{" ",7}" +
                        $"{"Tool Name",-45}" +
                        $"{"Available Qty",15}" +
                        $"{"Total Qty",15}" +
                        $"\n{new string('-', ConsoleLib.WIDTH)}");

                    if (ConsoleLib.SelectFromArray(out tool, tools))
                        return true;
                }

            tool = null;
            return false;
        }

        /// <summary>
        /// Returns the ToolCollection a Tool belongs to
        /// </summary>
        /// <param name="name">Name of the tool</param>
        /// <returns></returns>
        public static ToolCollection GetCollection(string name)
        {
            return ToolCollections.Values
                .SelectMany(category => category.Values)
                .Where(collection => collection.toArray().Any(tool => tool.Name.Equals(name)))
                .FirstOrDefault();
        }

        /// <summary>
        /// Returns a Tool object by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        static Tool GetTool(string name)
        {
            try {
                return GetCollection(name)?.toArray().Where(t => t.Name.Equals(name)).First();
            } catch(Exception e) {
                return null;
            }
        }
        
        static Member GetMember(string firstName, string lastName)
        {
            return GetMember(firstName + lastName);
        }
        
        static Member GetMember(string username)
        {
            try {
                return Members.toArray().Where(m => (m.FirstName + m.LastName).Equals(username, StringComparison.OrdinalIgnoreCase)).First();
            } catch(Exception e) {
                return null;
            }
        }

        /// <summary>
        /// 'Main Menu' entry point, used as primary program entry point
        /// </summary>
        static void MainMenu()
        {
            ConsoleLib.PrintMenuHeader("Main Menu");

            // OPTIONS AVAILABLE FOR THIS MENU
            Dictionary<string, Action> options = new Dictionary<string, Action>
            {
                {"Staff Login",     new Action(StaffLogin) },
                {"Member Login",    new Action(MemberLogin) },
            };

            // PRINTS SELECTION MENU BASED ON OPTIONS ABOVE
            if (ConsoleLib.SelectFromDictionary(out Action action, options))
            {
                action.Invoke();    // INVOKE SELECTED ACTION
                MainMenu();         // RECURSIVE MENU, WHILE LOOPS CAN BE EVIL
            }
        }

        static void StaffLogin()
        {
            ConsoleLib.PrintMenuHeader("Staff Login");

            if (ConsoleLib.ReadInput("Enter Username: ", out string username, 0, 15, ConsoleLib.InputFlags.Alpha))
                if (ConsoleLib.ReadInput("Enter Password: ", out string password, 0, 15, ConsoleLib.InputFlags.Password)) {
                    if (username == "staff" && password == "today123")
                        StaffMenu();
                    else
                        ConsoleLib.KeyWait("Error: Invalid username or password.");
                }
        }

        static void MemberLogin()
        {
            ConsoleLib.PrintMenuHeader("Member Login");

            if (ConsoleLib.ReadInput("Enter Username: ", out string username, 0, 15, ConsoleLib.InputFlags.Alpha))
                if (ConsoleLib.ReadInput("Enter PIN: ", out string pin, 4, 4, ConsoleLib.InputFlags.Numbers))
                {
                    Member member = GetMember(username);

                    if (member != null)
                        if (member.PIN == pin)
                        {
                            MemberMenu(member);
                            return;
                        }
                    
                    ConsoleLib.KeyWait("Error: Invalid username or password.");
                }
        } 

        /// <summary>
        /// 'Staff Menu' entry point
        /// </summary>
        static void StaffMenu()
        {
            ConsoleLib.PrintMenuHeader("Staff Menu");

            Dictionary<string, Action> options = new Dictionary<string, Action>
            {
                {"Add a new tool",                      new Action(AddNewTool) },
                {"Add new pieces of an existing tool",  new Action(AddToExistingTool) },
                {"Remove some pieces of a tool",        new Action(RemoveFromExistingTool) },
                {"Register a new member",               new Action(RegisterNewMember) },
                {"Remove a member",                     new Action(RemoveMember) },
                {"Find the contact number of a member", new Action(FindContactNumber) },
            };

            if (ConsoleLib.SelectFromDictionary(out Action action, options))
            {
                action.Invoke();
                StaffMenu();
            }

            void AddNewTool()
            {
                // SELECT CATEGORY
                ConsoleLib.PrintMenuHeader("Staff Menu - Add New Tool");

                if (ConsoleLib.ReadInput("Enter new Tool Name: ", out string name, flags:ConsoleLib.InputFlags.TextEntry | ConsoleLib.InputFlags.Numbers)) 
                    LibrarySystem.add(new Tool(name, 1));
            }

            void AddToExistingTool()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Add to Existing Tool");

                if (ConsoleSelectTool(out Tool tool))
                    if (ConsoleLib.ReadInput("Enter Quantity to Add: ", out string quantity, 1, flags: ConsoleLib.InputFlags.Numbers))
                        LibrarySystem.add(tool, int.Parse(quantity));
            }

            void RemoveFromExistingTool()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Remove from Existing Tool");

                if (ConsoleSelectTool(out Tool tool))
                    if (ConsoleLib.ReadInput("Enter Quantity to Remove: ", out string quantity, 1, flags: ConsoleLib.InputFlags.Numbers))
                        LibrarySystem.delete(tool, int.Parse(quantity));
            }

            void RegisterNewMember()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Register new Member");

                if (ConsoleLib.ReadInput("Enter First Name: ", out string firstName, flags: ConsoleLib.InputFlags.Alpha))
                    if (ConsoleLib.ReadInput("Enter Last Name: ", out string lastName, flags: ConsoleLib.InputFlags.Alpha))
                        if (ConsoleLib.ReadInput("Enter Contact No: ", out string contactNumber, flags: ConsoleLib.InputFlags.Numbers))
                            if (ConsoleLib.ReadInput("Enter PIN (4 Digits): ", out string pin, 4, 4, ConsoleLib.InputFlags.Numbers))
                                LibrarySystem.add(new Member(firstName, lastName, contactNumber, pin));
            }

            void RemoveMember()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Remove existing Member");

                if (ConsoleLib.SelectFromArray(out Member member, Members.toArray()))
                    LibrarySystem.delete(member);
            }

            void FindContactNumber()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Find Member's Contact Number");

                if (ConsoleLib.ReadInput("Enter Members First Name: ", out string firstName, 1, flags: ConsoleLib.InputFlags.Alpha))
                    if (ConsoleLib.ReadInput("Enter Members Last Name: ", out string lastName, 1, flags: ConsoleLib.InputFlags.Alpha))
                    {
                        Member member = GetMember(firstName, lastName);

                        if (member != null)
                            ConsoleLib.KeyWait($"{member.FirstName} {member.LastName}'s Contact Number is {member.ContactNumber}");
                    }
            }
        }

        /// <summary>
        /// 'Member Menu' GUI entry point.
        /// </summary>
        /// <param name="loggedInMember">Member logged in as</param>
        static void MemberMenu(Member loggedInMember)
        {
            ConsoleLib.PrintMenuHeader("Member Menu");

            Dictionary<string, Action> options = new Dictionary<string, Action>
            {
                {"Display all the tools of a tool type",                new Action(DisplayToolsOfType) },
                {"Borrow a tool",                                       new Action(BorrowTool) },
                {"Return a tool",                                       new Action(ReturnTool) },
                {"List all the tools that I am renting",                new Action(ListMyRentedTools) },
                {"Display top three (3) most frequently rented tools",  new Action(DisplayTopThreeRentedTools) },
            };

            if (ConsoleLib.SelectFromDictionary(out Action action, options))
            {
                action.Invoke();
                MemberMenu(loggedInMember);
            }

            void DisplayToolsOfType()
            {
                ConsoleLib.PrintMenuHeader("Member Menu - Display Tools of Type");

                if (ConsoleLib.SelectToolCategory(out string category))
                    if (ConsoleLib.SelectToolType(out string toolType, category))
                    {
                        ConsoleLib.PrintMenuSubHeader($"Displaying {category} - {toolType}");
                        LibrarySystem.displayTools(category + '/' + toolType);
                    }

                ConsoleLib.KeyWait();
            }

            void BorrowTool()
            {
                ConsoleLib.PrintMenuHeader("Member Menu - Borrow a Tool");

                if (loggedInMember.Tools.Length >= 3)
                {
                    ConsoleLib.KeyWait("Error: You cannot borrow more than 3 tools.");
                    return;
                }
                    

                if (ConsoleSelectTool(out Tool tool))
                    LibrarySystem.borrowTool(loggedInMember, tool);
            }

            void ReturnTool()
            {
                ConsoleLib.PrintMenuHeader("Member Menu - Return a Tool");

                if (!ConsoleLib.SelectFromArray(out string toolName, loggedInMember.Tools))
                    return;

                Tool tool = GetTool(toolName);
                LibrarySystem.returnTool(loggedInMember, tool);
            }

            void ListMyRentedTools()
            {
                ConsoleLib.PrintMenuHeader("Member Menu - List my rented Tools");

                LibrarySystem.displayBorrowingTools(loggedInMember);

                ConsoleLib.KeyWait();
            }

            void DisplayTopThreeRentedTools()
            {
                ConsoleLib.PrintMenuHeader("Member Menu - Top Three (3) borrowed Tools");

                LibrarySystem.displayTopThree();

                ConsoleLib.KeyWait();
            }
        }
    }
}
