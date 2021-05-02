using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    //The specification of Tool ADT
    interface iTool
    {

        /// <summary>
        /// Get and Set the name of this tool
        /// </summary>
        string Name
        {
            get;
            set;
        }

        /// <summary>
        /// Get and Set the quantity of this tool
        /// </summary>
        int Quantity
        {
            get;
            set;
        }

        /// <summary>
        /// Get and set the quantity of this tool currently available to lend
        /// </summary>
        int AvailableQuantity
        {
            get;
            set;
        }

        /// <summary>
        /// Get and set the number of times that this tool has been borrowed
        /// </summary>
        int NoBorrowings 
        {
            get;
            set;
        }

        /// <summary>
        /// Get all the members who are currently holding this tool
        /// </summary>
        iMemberCollection GetBorrowers
        {
            get;
        }

        /// <summary>
        /// Add a member to the borrower list
        /// </summary>
        /// <param name="aMember">A User within the ToolLibrarySystem</param>
        void addBorrower(Member aMember);

        /// <summary>
        /// Delte a member from the borrower list
        /// </summary>
        /// <param name="aMember">A User within the ToolLibrarySystem</param>
        void deleteBorrower(Member aMember);

        /// <summary>
        /// Return a string containning the name and the available quantity of this tool 
        /// </summary>
        /// <returns>Returns a string containing the name and available quantity of this tool</returns>
        string ToString(); 

    }

}
