using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    class MemberCollection : iMemberCollection
    {
        BSTree Members = new BSTree();

        public int Number => Members.Count();

        public void add(iMember aMember)
        {
            Member member = (Member)aMember;

            if (search(member) == false)
            {
                Members.Insert(member);
            }
        }

        public Member GetMember(iMember aMember)
        {
            return (Member)Members.GetNode((Member)aMember).Item;
        }

        public void delete(iMember aMember)
        {
            BTreeNode node = Members.GetNode((Member)aMember);
            Members.Delete((Member)aMember);
        }

        public bool search(iMember aMember)
        {
            return Members.Search((Member)aMember);
        }

        public iMember[] toArray()
        {
            return (Member[])Members.ToArray();
        }
    }
}
