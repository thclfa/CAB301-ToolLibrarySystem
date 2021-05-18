using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Linq;

namespace CAB301_ToolLibrarySystem
{
    public class Member : iMember, IComparable<Member>
    {
        string firstName;
        string lastName;
        string contactNumber;
        string pin;
        ToolCollection tools = new ToolCollection();

        public Member(string firstName, string lastName, string contactNumber, string pin) {
            this.firstName = firstName;
            this.lastName = lastName;
            this.contactNumber = contactNumber;
            this.pin = pin;
        }

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string ContactNumber { get => contactNumber; set => contactNumber = value; }
        public string PIN { get => pin; set => pin = value; }
        public string[] Tools => tools.toArray().WhereNotNull().Select(t => t.Name).ToArray();

        public void addTool(Tool aTool)
        {
            if (tools.toArray().Length < 3)
                tools.add(aTool);
        }

        public void deleteTool(Tool aTool)
        {
            tools.delete(aTool);
        }

        public override string ToString()
        {
            return $"{FirstName,-10} {LastName,-10} {ContactNumber}";
        }

        public int CompareTo(Member other)
        {
            return (LastName + FirstName).CompareTo(other.LastName + other.FirstName);
        }
    }
}
