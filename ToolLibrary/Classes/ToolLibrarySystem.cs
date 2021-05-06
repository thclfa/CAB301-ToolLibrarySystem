using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    class ToolLibrarySystem : iToolLibrarySystem
    {
        private static readonly Dictionary<string, Dictionary<string, ToolCollection>> toolCollections = new Dictionary<string, Dictionary<string, ToolCollection>>
        {
            {
                "Gardening Tools", new Dictionary<string, ToolCollection>()
                {

                    { "Line Trimmers", new ToolCollection() },
                    { "Lawn Mowers", new ToolCollection() },
                    { "Hand Tools", new ToolCollection() },
                    { "Wheelbarrows", new ToolCollection() },
                    { "Garden Power Tools", new ToolCollection() },
                }
            },
            {
                "Flooring Tools", new Dictionary<string, ToolCollection>()
                {
                    { "Scrapers", new ToolCollection() },
                    { "Floor Lasers", new ToolCollection() },
                    { "Floor Levelling Tools", new ToolCollection() },
                    { "Floor Levelling Materials", new ToolCollection() },
                    { "Floor Hand Tools", new ToolCollection() },
                    { "Tiling Tools", new ToolCollection() },
                }
            },
            {
                "Fencing Tools", new Dictionary<string, ToolCollection>()
                {
                    { "Hand Tools", new ToolCollection() },
                    { "Electric Fencing", new ToolCollection() },
                    { "Steel Fencing Tools", new ToolCollection() },
                    { "Power Tools", new ToolCollection() },
                    { "Fencing Accessories", new ToolCollection() },
                }
            },
            {
                "Measuring Tools", new Dictionary<string, ToolCollection>()
                {
                    { "Distance Tools", new ToolCollection() },
                    { "Laser Measurer", new ToolCollection() },
                    { "Measuring Jugs", new ToolCollection() },
                    { "Temperature & Humidity Tools", new ToolCollection() },
                    { "Levelling Tools", new ToolCollection() },
                    { "Markers", new ToolCollection() },
                }
            },
            {
                "Cleaning Tools", new Dictionary<string, ToolCollection>()
                {
                    { "Draining Tools", new ToolCollection() },
                    { "Car Cleaning", new ToolCollection() },
                    { "Vacuum", new ToolCollection() },
                    { "Pressure Cleaners", new ToolCollection() },
                    { "Pool Cleaning", new ToolCollection() },
                    { "Floor Cleaning", new ToolCollection() },
                }
            },
            {
                "Painting Tools", new Dictionary<string, ToolCollection>()
                {
                    { "Sanding Tools", new ToolCollection() },
                    { "Brushes", new ToolCollection() },
                    { "Rollers", new ToolCollection() },
                    { "Paint Removal Tools", new ToolCollection() },
                    { "Paint Scrapers", new ToolCollection() },
                    { "Sprayers", new ToolCollection() },
                }
            },
            {
                "Electronic Tools", new Dictionary<string, ToolCollection>()
                {
                    { "Voltage Tester", new ToolCollection() },
                    { "Oscilloscopes", new ToolCollection() },
                    { "Thermal Imaging", new ToolCollection() },
                    { "Data Test Tool", new ToolCollection() },
                    { "Insulation Testers", new ToolCollection() },
                }
            },
            {
                "Electricity Tools", new Dictionary<string, ToolCollection>()
                {
                    { "Test Equipment", new ToolCollection() },
                    { "Safety Equipment", new ToolCollection() },
                    { "Basic Hand Tools", new ToolCollection() },
                    { "Circuit Protection", new ToolCollection() },
                    { "Cable Tools", new ToolCollection() },
                }
            },
            {
                "Automotive Tools", new Dictionary<string, ToolCollection>()
                {
                    { "Jacks", new ToolCollection() },
                    { "Air Compressors", new ToolCollection() },
                    { "Battery Chargers", new ToolCollection() },
                    { "Socket Tools", new ToolCollection() },
                    { "Braking", new ToolCollection() },
                    { "Drive train", new ToolCollection() },
                }
            }
        };
        private static readonly MemberCollection Members = new MemberCollection();

        /// CONSTRUCTOR SORCERY
        public ToolLibrarySystem() { } // Empty constructor
        public ToolLibrarySystem(string username, out Member member) 
        {
            member = selectMember(username); // Just returns a member by username search
        }

        public ToolLibrarySystem(string username, string pin, out Member loggedInMember)
        {
            Member member = selectMember(username); // Just returns a member by username search
            loggedInMember = member.PIN.Equals(pin) ? member : null; 
        }

        /// PRIVATE METHODS
        private Member selectMember(string username)
        {
            return Members.toArray().FirstOrDefault(m => (m.FirstName + m.LastName).Equals(username));
        }

        private Tool selectTool()
        {
            if (ConsoleLib.SelectFromArray(out Tool tool, selectToolCollection().toArray()))
                return tool;
            return null;
        }

        private ToolCollection selectToolCollection()
        {
            if (ConsoleLib.SelectToolCategory(out string category))
                if (ConsoleLib.SelectToolType(out string toolType, category))
                    return toolCollections[category][toolType];
            return null;
        }

        private ToolCollection getCollection(Tool aTool)
        {
            return toolCollections.Values.SelectMany(x => x.Values).Where(x => x.search(aTool)).First();
        }

        private Tool getTool(ToolCollection collection, string name)
        {
            return collection.toArray().Where(x => x.Name.Equals(name)).First();
        }

        private Tool[] toArray()
        {
            return toolCollections.Values.SelectMany(x => x.Values).SelectMany(x => x.toArray()).ToArray();
        }


        /// INTERFACE METHODS
        public void add(Tool aTool)
        {
            ToolCollection collection = selectToolCollection();

            if (!collection.search(aTool))
                collection.add(aTool);
        }

        public void add(Tool aTool, int quantity)
        {
            ToolCollection collection = selectToolCollection();

            if (!collection.search(aTool))
                return;

            Tool t = getTool(collection, aTool.Name);

            t.Quantity += quantity;
            t.AvailableQuantity += quantity;
        }

        public void delete(Tool aTool)
        {
            if (aTool.GetBorrowers.Number > 0)
                return;

            ToolCollection collection = getCollection(aTool);

            collection.delete(aTool);
        }

        public void delete(Tool aTool, int quantity)
        {
            // Can't delete tools if they are being borrowed.
            if (aTool.AvailableQuantity < quantity)
                return;

            aTool.Quantity -= quantity;
            aTool.AvailableQuantity -= quantity;
        }

        public void add(Member aMember)
        {
            if (!Members.search(aMember))
                Members.add(aMember);
        }

        public void delete(Member aMember)
        {
            // Only delete the member if they aren't borrowing any tools.
            if (aMember.Tools.Length == 0)
                Members.delete(aMember);
        }

        public void borrowTool(Member aMember, Tool aTool)
        {
            if (aTool.AvailableQuantity == 0 || aMember.Tools.Length >= 3)
                return;

            aMember.addTool(aTool);
            aTool.addBorrower(aMember);
        }

        public void returnTool(Member aMember, Tool aTool)
        {
            if (aMember.Tools.Contains(aTool.Name))
            {
                aMember.deleteTool(aTool);
                aTool.deleteBorrower(aMember);
            }
        }

        public void displayBorrowingTools(Member aMember)
        {
            Console.WriteLine(string.Format("{0} {1}'s Borrowed Tools", aMember.FirstName, aMember.LastName)
                .PadSides(ConsoleLib.WIDTH, ' '));
            Console.WriteLine(new string('=', ConsoleLib.WIDTH));

            for (int i = 0; i < aMember.Tools.Length; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}",
                    i.ToString().PadLeft(5),
                    aMember.Tools[i]));
            }
        }

        public void displayTools(string aToolType)
        {
            string[] split = aToolType.Split("/");
            string collection = split[0];
            string toolType = split[1];
            Tool[] tools = toolCollections[collection][toolType].toArray();

            for (int i = 0; i < tools.Length; i++)
            {
                Console.WriteLine(string.Format("{0}. {1}", 
                    i.ToString().PadLeft(5), 
                    tools[i].ToString()));
            }
        }

        public void displayTopThree()
        {
            Tool[] sorted = toArray();
            sorted.OrderByDescending(x => x.NoBorrowings);

            for (int i = 0; i < 3; i++)
            {
                Tool tool = sorted[i];
                Console.WriteLine("{0}. {1} has been borrowed {2} times.", i.ToString().PadLeft(5), tool.Name, tool.NoBorrowings);
            }
        }

        public string[] listTools(Member aMember)
        {
            return aMember.Tools;
        }
    }
}
