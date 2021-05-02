using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    class Tool : iTool
    {
        public Tool(string name, int quantity)
        {
            this.Name = name;
            this.Quantity = quantity;
            this.AvailableQuantity = quantity;
            this.NoBorrowings = 0;
        }

        public string Name { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        public int NoBorrowings { get; set; }

        public iMemberCollection GetBorrowers => throw new NotImplementedException();

        public void addBorrower(Member aMember)
        {
            NoBorrowings++;
            throw new NotImplementedException();
        }

        public void deleteBorrower(Member aMember)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Name.Split('/')[2];
        }
    }
}
