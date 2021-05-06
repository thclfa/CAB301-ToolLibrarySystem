using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    class Program
    {
        static ToolLibrarySystem toolLibrarySystem = new ToolLibrarySystem();
        static Program Instance;

        static void Main(string[] args)
        {
            Instance = new Program();
            Instance.Init();
            MainMenu();
        }

        private void Init()
        {
            toolLibrarySystem = new ToolLibrarySystem();
            toolLibrarySystem.add(new Member("Thomas", "Fabian", "0118 999 88199 9119 725 3", "1234"));
            toolLibrarySystem.add(new Member("samohT", "naibaF", "3 527 9119 99188 999 8110", "4321"));
        }

        static void MainMenu()
        {
            ConsoleLib.StartMenu("Main Menu");

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
            ConsoleLib.StartMenu("Staff Login");

            Console.Write("\tEnter Username: ");
            if (ConsoleLib.ReadInput(out string username, 0, 15, ConsoleLib.InputFlags.Alpha)) {
                Console.Write("\tEnter Password: ");
                if (ConsoleLib.ReadInput(out string password, 0, 15, ConsoleLib.InputFlags.Password)) {
                    if (username == "staff" && password == "today123")
                        StaffMenu();
                    else
                    {
                        Console.WriteLine("\nInvalid username or password. (Escape to exit or any key to try again)");
                        switch (Console.ReadKey().Key)
                        {
                            case ConsoleKey.Escape: MainMenu(); return;
                            default: StaffLogin(); return;
                        }
                    }
                }
            }
        }

        static void MemberLogin()
        {
            ConsoleLib.StartMenu("Member Login");

            Console.Write("Enter Username: ");
            if (ConsoleLib.ReadInput(out string username, 0, 15, ConsoleLib.InputFlags.Alpha))
            {
                Console.Write("Enter PIN: ");
                if (ConsoleLib.ReadInput(out string pin, 4, 4, ConsoleLib.InputFlags.Numbers))
                {
                    toolLibrarySystem = new ToolLibrarySystem(username, pin, out Member loggedInAs);

                    if (loggedInAs != null)
                        MemberMenu(loggedInAs);
                }
            }
        }

        static void StaffMenu()
        {
            ConsoleLib.StartMenu("Staff Menu");

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
                ConsoleLib.StartMenu("Staff Menu - Add New Tool");

                Console.Write("Enter new Tool Name:");
                if (ConsoleLib.ReadInput(out string name, flags:ConsoleLib.InputFlags.TextEntry | ConsoleLib.InputFlags.Numbers)) {
                    toolLibrarySystem.add(new Tool(name, 1));
                }
            }

            void AddToExistingTool()
            {
                ConsoleLib.StartMenu("Staff Menu - Add to Existing Tool");

                toolLibrarySystem.add(new Tool("None", 0), 0);
            }

            void RemoveFromExistingTool()
            {
                ConsoleLib.StartMenu("Staff Menu - Remove from Existing Tool");


            }

            void RegisterNewMember()
            {
                ConsoleLib.StartMenu("Staff Menu - Register new Member");

            }

            void RemoveMember()
            {
                ConsoleLib.StartMenu("Staff Menu - Remove existing Member");

            }

            void FindContactNumber()
            {
                ConsoleLib.StartMenu("Staff Menu - Find Member's Contact Number");

            }
        }

        static void MemberMenu(Member loggedInMember)
        {
            ConsoleLib.StartMenu("Member Menu");

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
                ConsoleLib.StartMenu("Member Menu - Display Tools of Type");

                if (ConsoleLib.SelectToolCategory(out string category))
                    if (ConsoleLib.SelectToolType(out string toolType, category))
                        toolLibrarySystem.displayTools(category + '/' + toolType);

                ConsoleLib.KeyWait();
            }

            void BorrowTool()
            {
                ConsoleLib.StartMenu("Member Menu - Borrow a Tool");

            }

            void ReturnTool()
            {
                ConsoleLib.StartMenu("Member Menu - Return a Tool");

            }

            void ListMyRentedTools()
            {
                ConsoleLib.StartMenu("Member Menu - List my rented Tools");

                toolLibrarySystem.displayBorrowingTools(loggedInMember);

            }

            void DisplayTopThreeRentedTools()
            {
                ConsoleLib.StartMenu("Member Menu - Top Three (3) borrowed Tools");

                toolLibrarySystem.displayTopThree();
            }
        }
    }
}
