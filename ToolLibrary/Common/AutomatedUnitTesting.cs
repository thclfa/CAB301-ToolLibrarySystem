using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    /// <summary>
    /// Automated Unit Testing class designed to test the internal functionalities of the 
    /// ToolLibrarySystem circumventing the need for a GUI.
    /// </summary>
    public class AutomatedUnitTesting
    {
        private List<bool> tests;
        private ToolLibrarySystem librarySystem;
        private MemberCollection members;
        private Dictionary<string, Dictionary<string, ToolCollection>> toolCollections;

        public AutomatedUnitTesting()
        {
            tests = new List<bool>();
            members = new MemberCollection();
            toolCollections = new Dictionary<string, Dictionary<string, ToolCollection>>();
            librarySystem = new ToolLibrarySystem(ref members, ref toolCollections);

            // Populate ToolCollections
            foreach (KeyValuePair<string, string[]> category in ConsoleLib.Tools)
            {
                var collection = new Dictionary<string, ToolCollection>();

                foreach (string toolType in category.Value)
                    collection.Add(toolType, new ToolCollection());

                toolCollections.Add(category.Key, collection);
            }
        }

        /// <summary>
        /// Sorting Numbers introduced by Hugo Steinhaus for testing Sorting Algorithms
        /// https://en.wikipedia.org/wiki/Sorting_number
        /// </summary>
        /// <param name="n">Size of array</param>
        /// <returns>Returns array of worst case numbers to test for sorting</returns>
        public int[] GenerateSortingNumbers(int n)
        {
            int[] result = new int[n];

            for (int i = 1; i <= n; i++)
            {
                int S = (int)Math.Floor(1 + Math.Log2(i));
                result[i - 1] = (int)(i * S - Math.Pow(2, S) + 1);
            }

            return result;
        }

        /// <summary>
        /// Performs unit testing on all functionalities of the ToolLibrarySystem bar the Console GUI
        /// </summary>
        public void RunUnitTests()
        {
            ConsoleLib.PrintMenuHeader("Running Unit Tests");

            Console.WriteLine($" {"ID",-2} RESULT {"TEST NAME",-35} EXPECTED OUTCOME\n{'-'.Mul(Console.WindowWidth)}");

            // Dummy tests
            //UnitTest(10, x => x + 10 == 20, "10 + 10", "20");
            //UnitTest(10, x => x - 10 == 20, "10 - 10", "20 (This test was designed to fail)");

            Console.WriteLine("Creation Test");

            // NEW MEMBER TEST
            Member member = new Member("Maurice", "Moss", "0118999881999119725 3", "1234");
            librarySystem.add(member);
            string username = member.FirstName + member.LastName;
            string fullName = member.FirstName + " " + member.LastName;
            UnitTest(member, x => GetMember(username) != null, $"Add new Member '{fullName}'", $"'{fullName}' has been created.");

            // NEW TOOL TEST
            Tool tool = new Tool("Sky Hooks", 0);
            string toolName = tool.Name;
            toolCollections["Gardening Tools"]["Line Trimmers"].add(tool);
            UnitTest(tool, x => GetTool(toolName) != null, $"Add new Tool '{toolName}'", $"'{toolName}' has been created.");

            Console.WriteLine("Unsafe Borrow Test");

            // Unsafe Borrow Tool Test
            librarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 0 && tool.GetBorrowers.toArray().Length == 0, $"Try Borrow '{toolName}'", $"'{toolName}' not borrowed as there is 0x Quantity");

            Console.WriteLine("Quantity Test");

            // Quantity Add Test
            librarySystem.add(tool, 20);
            UnitTest(tool, x => x.Quantity == 20, $"Add 20x of '{toolName}'", $"'{toolName}' Quantity is 20");
            // Quantity Remove Test
            librarySystem.delete(tool, 10);
            UnitTest(tool, x => x.Quantity == 10, $"Del 10x of '{toolName}'", $"'{toolName}' Quantity is 10");

            Console.WriteLine("Safe Borrow Test");

            // Safe Borrow Tool Test
            librarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 1 && tool.GetBorrowers.toArray().Length == 1, $"Borrow 1st '{toolName}'", $"'{fullName}' has 1 tools and '{toolName}' has 1 borrower");
            librarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 2 && tool.GetBorrowers.toArray().Length == 1, $"Borrow 2nd '{toolName}'", $"'{fullName}' has 2 tools and '{toolName}' has 1 borrower");
            librarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 3 && tool.GetBorrowers.toArray().Length == 1, $"Borrow 3rd '{toolName}'", $"'{fullName}' has 3 tools and '{toolName}' has 1 borrower");
            librarySystem.borrowTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 3, $"Try Borrow 4th '{toolName}'", $"'{fullName}' has 3 tools and '{toolName}' not borrowed");

            Console.WriteLine("Unsafe Deletion Test");

            // Quantity Remove Test
            librarySystem.delete(tool, 10);
            UnitTest(tool, x => x.Quantity == 10, $"Try Delete 10x of '{toolName}'", "Unchanged Quantity as can't delete borrowed tools");
            // Delete Member while Borrowing
            librarySystem.delete(member);
            UnitTest(member, x => GetMember(username) == member, $"Try Delete Member '{fullName}'", $"'{fullName}' still exists as is still borrowing tools");
            // Delete Tool while Borrowing
            librarySystem.delete(tool);
            UnitTest(tool, x => GetTool(toolName) == tool, $"Try Delete Tool '{toolName}'", $"'{toolName}' still exists as it is still being borrowed");

            Console.WriteLine("Return Test");

            // Member Return Tools
            librarySystem.returnTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 2 && tool.GetBorrowers.toArray().Length == 1, $"Return 1st '{toolName}'", $"'{fullName}' has 2 tools and '{toolName}' has 1 borrower");
            librarySystem.returnTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 1 && tool.GetBorrowers.toArray().Length == 1, $"Return 2nd '{toolName}'", $"'{fullName}' has 1 tools and '{toolName}' has 1 borrower");
            librarySystem.returnTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 0 && tool.GetBorrowers.toArray().Length == 0, $"Return 3rd '{toolName}'", $"'{fullName}' has 0 tools and '{toolName}' has no borrower");
            librarySystem.returnTool(member, tool);
            UnitTest(member, x => x.Tools.Length == 0, $"Try Return 4th Tool '{toolName}'", $"No '{toolName}' to be returned.");

            Console.WriteLine("Safe Deletion Test");

            // Delete Member
            username = member.FirstName + member.LastName;
            librarySystem.delete(member);
            UnitTest(member, x => GetMember(username) == null, $"Delete the Member '{fullName}'", $"'{fullName}' no longer exists");
            // Delete Tool
            librarySystem.delete(tool);
            UnitTest(tool, x => GetTool(toolName) == null, $"Delete the Tool '{toolName}'", $"'{toolName}' no longer exists");

            Console.WriteLine("Merge Sort Test");

            // Test Partially Ordered Merge Sort
            Random random = new Random();
            int[] testSort = new int[] { 0, 1, 3, 5, 8, 11, 14, 17, 21, 25 }.OrderBy(x => random.Next()).ToArray();
            int[] sorted = testSort.MergeSort();
            int[] expected = new int[] { 0, 1, 3, 5, 8, 11, 14, 17, 21, 25 };
            UnitTest(sorted, x => x.SequenceEqual(expected), $"Sort [{string.Join(',', testSort)}]", $"Sorted [{string.Join(',', expected)}]");

            // PRINT RESULT
            int passCount = tests.Where(x => x == true).Count();
            int testCount = tests.Count();
            Console.WriteLine($"\nPassed {passCount} of {testCount} tests...");
            Console.ReadKey();
        }

        /// <summary>
        /// Performs a unit test on some object given some predicate.
        /// </summary>
        /// <typeparam name="T">Type of object to test</typeparam>
        /// <param name="obj">Object to test</param>
        /// <param name="predicate">Predicate for test</param>
        /// <param name="testName">Name of test</param>
        /// <param name="expected">Expected result of test as string</param>
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

        /// <summary>
        /// Returns some tool by name from the ToolCollection
        /// </summary>
        /// <param name="name"></param>
        /// <returns>REturns a tool if found or null</returns>
        Tool GetTool(string name)
        {
            try
            {
                ToolCollection collection = toolCollections.Values
                    .SelectMany(category => category.Values)
                    .Where(collection => collection.toArray().Any(tool => tool.Name.Equals(name)))
                    .FirstOrDefault();

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
        Member GetMember(string username)
        {
            try
            {
                return members.toArray().Where(m => (m.FirstName + m.LastName).Equals(username, StringComparison.OrdinalIgnoreCase)).First();
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
