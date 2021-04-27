using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    class ToolCollection : iToolCollection
    {
        iTool[] Tools = new iTool[0];

        public int Number => Tools.Length;

        public void add(iTool aTool)
        {
            Array.Resize(ref Tools, Number + 1);
            Tools[Number] = (Tool)aTool;
        }

        public void delete(iTool aTool)
        {
            int i = Array.IndexOf(Tools, aTool);
            if (i == -1)
                return;

            // Remove item
            Tools[i] = null;

            // Shift every item up
            for (; i < Number; i++)
                Tools[i] = Tools[i + 1];

            // Resize array to reflect new size
            Array.Resize(ref Tools, Number - 1);
        }

        public bool search(iTool aTool)
        {
            return Array.IndexOf(Tools, aTool) > -1;
        }

        public iTool[] toArray()
        {
            return Tools;
        }
    }
}
