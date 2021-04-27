using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    class ToolLibrarySystem : iToolLibrarySystem
    {
        ToolCollection Tools = new ToolCollection();
        MemberCollection Members = new MemberCollection();

        public void add(iTool aTool)
        {
            Tools.add(aTool);
        }

        public void add(iTool aTool, int quantity)
        {
            throw new NotImplementedException();
        }

        public void add(iMember aMember)
        {
            Members.add(aMember);
        }

        public void borrowTool(iMember aMember, iTool aTool)
        {
            Member member = Members.GetMember(aMember);
        }

        public void delete(iTool aTool)
        {
            throw new NotImplementedException();
        }

        public void delete(iTool aTool, int quantity)
        {
            throw new NotImplementedException();
        }

        public void delete(iMember aMember)
        {
            Members.delete(aMember);
        }

        public void displayBorrowingTools(iMember aMember)
        {
            throw new NotImplementedException();
        }

        public void displayTools(string aToolType)
        {
            throw new NotImplementedException();
        }

        public void displayTopTHree()
        {
            throw new NotImplementedException();
        }

        public string[] listTools(iMember aMember)
        {
            throw new NotImplementedException();
        }

        public void returnTool(iMember aMember, iTool aTool)
        {
            throw new NotImplementedException();
        }
    }
}
