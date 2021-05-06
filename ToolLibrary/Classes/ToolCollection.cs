using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    public class ToolCollection : iToolCollection
    {
        private Tool[] collection = new Tool[1];

        public int Number => Array.FindAll(collection, t => t != null).Length;

        public void add(Tool aTool)
        {
            collection[Number] = aTool;
            Array.Resize(ref collection, Number + 1); // Resize to add empty entry
        }

        public void delete(Tool aTool)
        {
            int i = Array.IndexOf(collection, aTool);
            if (i == -1)
                return;

            // Remove item
            collection[i] = null;

            // Shift every item up
            for (; i < Number; i++)
                collection[i] = collection[i + 1];

            Array.Resize(ref collection, Number - 1); // Resize to remove empty entry
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
