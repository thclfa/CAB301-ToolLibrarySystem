using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    public class MemberCollection : iMemberCollection
    {
        BSTree Members = new BSTree();

        public void add(Member aMember)
        {
            if (!search(aMember))
                Members.Insert(aMember);
        }

        public void delete(Member aMember)
        {
            if(search(aMember))
                Members.Delete(aMember);
        }

        public int Number => Members.Count();

        public bool search(Member aMember) => Members.Search(aMember);

        public Member[] toArray() => Members.ToArray();
    }
}
