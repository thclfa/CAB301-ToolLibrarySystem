﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    class ToolLibrarySystem : iToolLibrarySystem
    {
        private Dictionary<string, Dictionary<string, ToolCollection>> ToolCollections;
        private MemberCollection Members;


        /// CONSTRUCTOR SORCERY
        public ToolLibrarySystem(ref MemberCollection members, ref Dictionary<string, Dictionary<string, ToolCollection>> toolCollections) 
        {
            this.Members = members;
            this.ToolCollections = toolCollections;
        }

        /// <summary>
        /// Use Console input to select a tool collection
        /// </summary>
        /// <param name="collection">Collection selected</param>
        /// <returns>Returns True if selection successful, false if input cancelled</returns>
        private bool ConsoleSelectCollection(out ToolCollection collection)
        {
            if (ConsoleLib.SelectToolCategory(out string category))
                if (ConsoleLib.SelectToolType(out string toolType, category))
                {
                    collection = ToolCollections[category][toolType];
                    return true;
                }

            collection = null;
            return false;
        }

        /// <summary>
        /// Flattens the tool collection for linear iteration
        /// </summary>
        /// <returns>Flattened tool array</returns>
        private Tool[] flattenTools()
        {
            return ToolCollections.Values.SelectMany(s => s.Values.SelectMany(c => c.toArray())).ToArray();
        }

        /// INTERFACE METHODS
        public void add(Tool aTool)
        {
            if (ConsoleSelectCollection(out ToolCollection collection))
            {
                // If the tool already exists, just add 1 quantity
                if (collection.search(aTool))
                {
                    Tool tool = collection.toArray().Where(x => x.Name == aTool.Name).First();
                    add(tool, 1);
                }else
                    collection.add(aTool);

                displayTools(collection);
                ConsoleLib.KeyWait($"An {aTool.Name} has been added to the collection.");
            }
        }

        public void add(Tool aTool, int quantity)
        {
            aTool.Quantity += quantity;
            aTool.AvailableQuantity += quantity;
        }

        public void delete(Tool aTool)
        {
            if (aTool.GetBorrowers.Number > 0)
                return;

            ToolCollection collection = ToolCollections.Values
                .SelectMany(category => category.Values)
                .Where(collection => collection.toArray().Any(tool => tool == aTool))
                .FirstOrDefault();

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
            if (!aMember.Tools.Contains(aTool.Name))
                return;

            aMember.deleteTool(aTool);
            aTool.AvailableQuantity++;

            // The member may still have another instance of this tool
            if (!aMember.Tools.Contains(aTool.Name))
                aTool.deleteBorrower(aMember);
        }

        public void displayBorrowingTools(Member aMember)
        {
            Console.WriteLine($"{aMember.FirstName} {aMember.LastName}'s Borrowed Tools".Pad(' '));
            Console.WriteLine(new string('-', ConsoleLib.WIDTH));

            if (aMember.Tools.Length > 0)
                for (int i = 0; i < aMember.Tools.Length; i++)
                    Console.WriteLine($"{i + 1,5}. {aMember.Tools[i]}");
            else
                Console.WriteLine($"{aMember.FirstName} {aMember.LastName} is not currently borrowing any tools.");
        }

        public void displayTools(string aToolType)
        {
            string[] split = aToolType.Split("/");
            string collection = split[0];
            string toolType = split[1];

            displayTools(ToolCollections[collection][toolType]);
        }

        private void displayTools(ToolCollection collection)
        {
            Tool[] tools = collection.toArray();

            Console.WriteLine($"\n" +
                $"{" ",7}" +
                $"{"Tool Name",-45}" +
                $"{"Available Qty",15}" +
                $"{"Total Qty",15}" +
                $"{"No Borrowings",15}" +
                $"\n{'-'.Mul(Console.WindowWidth)}");

            for (int i = 0; i < tools.Length; i++)
            {
                if (tools[i] != null)
                    Console.WriteLine($"{i + 1,5}. {tools[i]}");
            }
        }

        public void displayTopThree()
        {
            Tool[] sorted = flattenTools().CountingSort(t => t.NoBorrowings);
            Array.Reverse(sorted);

            for (int i = 0; i < Math.Min(3, sorted.Length); i++)
            {
                Tool tool = sorted[i];

                if (tool != null)
                    Console.WriteLine($"{i + 1,5}. {tool.Name} has been borrowed {tool.NoBorrowings} times.");
            }
        }

        public string[] listTools(Member aMember)
        {
            return aMember.Tools;
        }
    }
}
