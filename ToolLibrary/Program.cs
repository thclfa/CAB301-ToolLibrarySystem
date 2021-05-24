using System;
using System.Collections.Generic;
using System.Linq;

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
            foreach (KeyValuePair<string, string[]> category in ConsoleLib.Tools)
            {
                var collection = new Dictionary<string, ToolCollection>();

                foreach (string toolType in category.Value)
                    collection.Add(toolType, new ToolCollection());

                ToolCollections.Add(category.Key, collection);
            }

            LibrarySystem = new ToolLibrarySystem(ref Members, ref ToolCollections);
           
            MainMenu(); // Main Console entry point
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


            void StaffLogin()
            {
                ConsoleLib.PrintMenuHeader("Staff Login");

                if (ConsoleLib.ReadInput("Enter Username: ", out string username, 0, 15, ConsoleLib.InputFlags.Alpha))
                    if (ConsoleLib.ReadInput("Enter Password: ", out string password, 0, 15, ConsoleLib.InputFlags.Password, false))
                    {
                        if (username == "staff" && password == "today123")
                            StaffMenu();
                        else
                            ConsoleLib.KeyWait("Login Unsuccessful. Invalid username or password.");
                    }
            }

            void MemberLogin()
            {
                ConsoleLib.PrintMenuHeader("Member Login");

                if (ConsoleLib.ReadInput("Enter Username: ", out string username, 0, 15, ConsoleLib.InputFlags.Alpha))
                    if (ConsoleLib.ReadInput("Enter PIN: ", out string pin, 4, 4, ConsoleLib.InputFlags.Numbers, false))
                    {
                        Member member = GetMember(username);

                        if (member != null)
                            if (member.PIN == pin)
                            {
                                MemberMenu(member);
                                return;
                            }

                        ConsoleLib.KeyWait("Login Unsuccessful. Invalid username or password.");
                    }
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

            /// SUB MENU FUNCTIONS

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
                    {
                        LibrarySystem.add(tool, int.Parse(quantity));
                        ConsoleLib.KeyWait($"{quantity} has been added to {tool.Name} for a total of {tool.Quantity}.");
                    }
                        
            }

            void RemoveFromExistingTool()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Remove from Existing Tool");

                if (ConsoleSelectTool(out Tool tool))
                    if (ConsoleLib.ReadInput("Enter Quantity to Remove: ", out string quantity, 1, flags: ConsoleLib.InputFlags.Numbers))
                    {
                        LibrarySystem.delete(tool, int.Parse(quantity));
                        ConsoleLib.KeyWait($"{quantity} has been removed from {tool.Name} and now has {tool.Quantity} left.");
                    }
            }

            void RegisterNewMember()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Register new Member");

                if (ConsoleLib.ReadInput("Enter First Name: ", out string firstName, flags: ConsoleLib.InputFlags.Alpha))
                    if (ConsoleLib.ReadInput("Enter Last Name: ", out string lastName, flags: ConsoleLib.InputFlags.Alpha))
                        if (ConsoleLib.ReadInput("Enter Contact No: ", out string contactNumber, flags: ConsoleLib.InputFlags.Numbers))
                            if (ConsoleLib.ReadInput("Enter PIN (4 Digits): ", out string pin, 4, 4, ConsoleLib.InputFlags.Numbers))
                            {
                                Member member = new Member(firstName, lastName, contactNumber, pin);
                                if (!Members.search(member))
                                {
                                    LibrarySystem.add(member);
                                    ConsoleLib.KeyWait($"{firstName} {lastName} has been registered as a new member.");
                                }
                                else
                                    ConsoleLib.KeyWait($"{firstName} {lastName} already exists!");
                            }
                                
            }

            void RemoveMember()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Remove existing Member");

                if (ConsoleLib.SelectFromArray(out Member member, Members.toArray()))
                {
                    LibrarySystem.delete(member);

                    if (!Members.search(member))
                        ConsoleLib.KeyWait($"{member.FirstName} {member.LastName} has been deleted.");
                    else
                        ConsoleLib.KeyWait($"{member.FirstName} {member.LastName} deletion unsuccessful.");
                }
                    
            }

            void FindContactNumber()
            {
                ConsoleLib.PrintMenuHeader("Staff Menu - Find Member's Contact Number");

                if (ConsoleLib.ReadInput("Enter Members First Name: ", out string firstName, 1, flags: ConsoleLib.InputFlags.Alpha))
                    if (ConsoleLib.ReadInput("Enter Members Last Name: ", out string lastName, 1, flags: ConsoleLib.InputFlags.Alpha))
                    {
                        Member member = GetMember(firstName+lastName);

                        if (member != null)
                            ConsoleLib.KeyWait($"{member.FirstName} {member.LastName}'s Contact Number is {member.ContactNumber}");
                        else
                            ConsoleLib.KeyWait($"{member.FirstName} {member.LastName} has not been found.");
                    }
            }
        }

        /// <summary>
        /// 'Member Menu' entry point.
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
                        ConsoleLib.KeyWait();
                    }
            }

            void BorrowTool()
            {
                ConsoleLib.PrintMenuHeader("Member Menu - Borrow a Tool");

                if (loggedInMember.Tools.Length >= 3)
                    ConsoleLib.KeyWait("Error: You cannot borrow more than 3 tools. Please return one before trying again.");
                else if (ConsoleSelectTool(out Tool tool))
                {
                    if (tool.AvailableQuantity > 0)
                    {
                        LibrarySystem.borrowTool(loggedInMember, tool);
                        ConsoleLib.KeyWait($"{tool.Name} has been borrowed. You now have {loggedInMember.Tools.Length} tools borrowed.");
                    }
                    else
                        ConsoleLib.KeyWait($"{tool.Name} was not borrowed. No Available Quantity.");
                }
            }

            void ReturnTool()
            {
                ConsoleLib.PrintMenuHeader("Member Menu - Return a Tool");

                if (!ConsoleLib.SelectFromArray(out string toolName, loggedInMember.Tools))
                    return;

                Tool tool = GetTool(toolName);
                LibrarySystem.returnTool(loggedInMember, tool);
                ConsoleLib.KeyWait($"{tool.Name} has been returned. You now have {loggedInMember.Tools.Length} tools borrowed.");
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
                        $"{"No Borrowings",15}" +
                        $"\n{'-'.Mul(Console.WindowWidth)}");

                    if (ConsoleLib.SelectFromArray(out tool, tools))
                        return true;
                }

            tool = null;
            return false;
        }

        /// <summary>
        /// Returns some tool by name from the ToolCollection
        /// </summary>
        /// <param name="name"></param>
        /// <returns>REturns a tool if found or null</returns>
        static Tool GetTool(string name)
        {
            try
            {
                ToolCollection collection = ToolCollections.Values
                    .SelectMany(category => category.Values)
                    .Where(collection => collection.toArray().Any(tool => tool.Name.Equals(name)))
                    .First();

                return collection?.toArray().Where(t => t.Name.Equals(name)).First();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns some member by username from the MemberCollection
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns a member if found or null</returns>      
        static Member GetMember(string username)
        {
            try
            {
                return Members.toArray().Where(m => (m.FirstName + m.LastName).Equals(username, StringComparison.OrdinalIgnoreCase)).First();
            }
            catch (Exception e)
            {
                return null;
            }
        }


        ///// <summary>
        ///// Add some dummy data to the member/tool collections for testing purposes
        ///// </summary>
        //static void InsertTestData()
        //{
        //    LibrarySystem.add(new Member("a", "a", "1234567", "1111"));
        //    LibrarySystem.add(new Member("Thomas", "Fabian", "N10582835", "1234"));
        //    LibrarySystem.add(new Member("Maurice", "Moss", "0118 999 88199 9119 725 3", "1234"));
        //    LibrarySystem.add(new Member("Lowly", "Apprentice", "0432 384 542", "1111"));
        //    LibrarySystem.add(new Member("Gone", "Fishing", "0448 123 952", "1111"));

        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Silent Bell", 32));
        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Left-Handed Hammer", 4));
        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Spirit-Level Bubble", 2));
        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Sky Hooks", 50));
        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Skirting Board Ladder", 9));
        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Bucket of Steam", 10));
        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Glass Hammer", 10));
        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("A Long Weight", 1000));
        //    ToolCollections["Gardening Tools"]["Line Trimmers"].add(new Tool("Tin of Striped Paint", 15));
        //}
    }
}
