using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    public class Tool : iTool, IComparable<Tool>
    {
        private MemberCollection members;
        private string name;
        private int quantity;
        private int availableQuantity;
        private int noBorrowings;

        public Tool(string name, int quantity)
        {
            this.name = name;
            this.quantity = quantity;
            this.availableQuantity = quantity;
            this.noBorrowings = 0;
            this.members = new MemberCollection();
        }

        public string Name { get => name; set => name = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int AvailableQuantity { get => availableQuantity; set => availableQuantity = value; }
        public int NoBorrowings { get => noBorrowings; set => noBorrowings = value; }

        public MemberCollection GetBorrowers => members;

        public void addBorrower(Member aMember)
        {
            if (AvailableQuantity <= 0)
                return;

            members.add(aMember);
            AvailableQuantity--;
            NoBorrowings++;
        }

        public void deleteBorrower(Member aMember)
        {
            if (!members.search(aMember))
                return;

            members.delete(aMember);
        }

        public override string ToString()
        {
            return $"{Name,-45}{AvailableQuantity,15}{Quantity,15}{NoBorrowings,15}";
        }

        public int CompareTo(Tool other)
        {
            return Name.CompareTo(other.Name);
        }
    }
}
