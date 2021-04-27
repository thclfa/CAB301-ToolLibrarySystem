using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment1_ToolLibrary
{
    class ToolCollection : iToolCollection
    {
        Tool[] Tools = new Tool[1];

        public int Number => Tools.Length;

        public void add(iTool aTool)
        {
            Array.Resize(ref Tools, Number + 1);
        }

        public void delete(iTool aTool)
        {
            throw new NotImplementedException();
        }

        public bool search(iTool aTool)
        {
            throw new NotImplementedException();
        }

        public iTool[] toArray()
        {
            throw new NotImplementedException();
        }
    }
}
