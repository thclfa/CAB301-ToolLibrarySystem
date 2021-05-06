using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    //The specification of Tool ADT
    interface iTool
    {
        /// <summary>
        /// get and set the name of this tool
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// get and set the quantity of this tool
        /// </summary>
        int Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// get and set the quantity of this tool currently available to lend
        /// </summary>
        int AvailableQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// get and set the number of times that this tool has been borrowed
        /// </summary>
        int NoBorrowings
        {
            get;
            set;
        }

        /// <summary>
        /// get all the members who are currently holding this tool
        /// </summary>
        MemberCollection GetBorrowers
        {
            get;
        }

        /// <summary>
        /// add a member to the borrower list
        /// </summary>
        void addBorrower(Member aMember);

        /// <summary>
        /// delete a member from the borrower list
        /// </summary>
        /// <param name="aMember"></param>
        void deleteBorrower(Member aMember);

        /// <summary>
        /// return a string containning the name and the available quantity quantity this tool 
        /// </summary>
        string ToString();

    }

}
