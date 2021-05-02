using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    class ToolCollection : iToolCollection
    {
        Tool[] Tools = new Tool[0];

        public int Number => Array.FindAll(Tools, t => t != null).Length;

        public void add(Tool aTool)
        {
            Tools[Number] = aTool;
            Array.Resize(ref Tools, Number + 1); // Resize to add empty entry
        }

        public void delete(Tool aTool)
        {
            int i = Array.IndexOf(Tools, aTool);
            if (i == -1)
                return;

            // Remove item
            Tools[i] = null;

            // Shift every item up
            for (; i < Number; i++)
                Tools[i] = Tools[i + 1];

            Array.Resize(ref Tools, Number - 1); // Resize to remove empty entry
        }

        public bool search(Tool aTool)
        {
            return Array.IndexOf(Tools, aTool) > -1;
        }

        public Tool[] toArray()
        {
            return Tools;
        }
    }
}
