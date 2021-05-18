using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    public class ToolCollection : iToolCollection
    {
        private Tool[] collection = new Tool[0];

        public int Number => collection.Length;

        public void add(Tool aTool)
        {
            Array.Resize(ref collection, Number + 1); // Resize to add empty entry
            collection[Number - 1] = aTool;
        }

        public void delete(Tool aTool)
        {
            // Get index of tool
            int i = Array.IndexOf(collection, aTool);

            // If the tool was found
            if (i != -1)
            {
                // Remove tool
                collection[i] = null;

                // If the array has more entries to the right, shift them to the left
                for (; i < Number - 1; i++)
                    collection[i] = collection[i + 1];

                // Resize to remove right-most null entry
                Array.Resize(ref collection, Number - 1); 
            }
        }

        public bool search(Tool aTool)
        {
            return Array.IndexOf(collection, aTool) > -1;
        }

        public Tool[] toArray()
        {
            return collection;
        }
    }
}
