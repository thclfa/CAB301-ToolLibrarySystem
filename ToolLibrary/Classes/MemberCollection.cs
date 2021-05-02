using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    class MemberCollection : iMemberCollection
    {
        BSTree Members = new BSTree();

        public int Number => Members.Count();

        public void add(Member aMember)
        {
            if (search(aMember) == false)
                Members.Insert(aMember);
        }

        public void delete(Member aMember)
        {
            Members.Delete(aMember);
        }

        public bool search(Member aMember)
        {
            return Array.Exists(toArray(), m => 
                   string.Equals(m.FirstName, aMember.FirstName, StringComparison.OrdinalIgnoreCase) 
                && string.Equals(m.LastName, aMember.LastName, StringComparison.OrdinalIgnoreCase) 
                && m.PIN == aMember.PIN);
        }

        // NOT PART OF THE INTERFACE!
        public Member get(string firstName, string lastName, string pin)
        {
            return Array.Find(toArray(), m =>
                    string.Equals(m.FirstName, firstName, StringComparison.OrdinalIgnoreCase)
                 && string.Equals(m.LastName, lastName, StringComparison.OrdinalIgnoreCase)
                 && m.PIN == pin);
        }

        public Member[] toArray()
        {
            return Array.ConvertAll(Members.ToArray(), m => (Member)m);
        }
    }
}
