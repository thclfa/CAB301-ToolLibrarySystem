using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment1_ToolLibrary
{
    class Member : iMember, IComparable
    {
        public Member(string firstName, string lastName, string contactNumber, string pin)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.ContactNumber = contactNumber;
            this.PIN = pin;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => string.Format("{0} {1}", FirstName, LastName);
        public string ContactNumber { get; set; }
        public string PIN { get; set; }

        public string[] Tools => throw new NotImplementedException();

        public void addTool(iTool aTool)
        {
            throw new NotImplementedException();
        }

        public void deleteTool(iTool aTool)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            return string.Compare(this.ToString(), obj.ToString());
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FirstName, LastName, ContactNumber);
        }
    }
}
