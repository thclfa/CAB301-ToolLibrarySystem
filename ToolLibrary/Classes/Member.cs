using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    class Member : iMember, IComparable
    {
        string firstName;
        string lastName;
        string contactNumber;
        string pin;

        public Member(string firstName, string lastName, string contactNumber, string pin)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.contactNumber = contactNumber;
            this.pin = pin;
        }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public string PIN { get => pin; set => firstName = value; }

        public string[] Tools => throw new NotImplementedException();

        public void addTool(Tool aTool)
        {
            throw new NotImplementedException();
        }

        public void deleteTool(Tool aTool)
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
