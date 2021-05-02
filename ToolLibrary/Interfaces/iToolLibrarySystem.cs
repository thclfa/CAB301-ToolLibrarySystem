using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    interface iToolLibrarySystem
    {
        /// <summary>
        /// Add a new <see cref="Tool"/> to the system
        /// </summary>
        /// <param name="aTool"><see cref="Tool"/> to be added</param>
        void add(Tool aTool);

        /// <summary>
        /// Add new pieces of an existing <see cref="Tool"/> to the system
        /// </summary>
        /// <param name="aTool">Existing <see cref="Tool"/> to be extended</param>
        /// <param name="quantity">Quantity to add</param>
        void add(Tool aTool, int quantity);

        /// <summary>
        /// Delete a given <see cref="Tool"/> from the system
        /// </summary>
        /// <param name="aTool"><see cref="Tool"/> to be deleted</param>
        void delete(Tool aTool);

        /// <summary>
        /// Remove some pieces of a <see cref="Tool"/> from the system
        /// </summary>
        /// <param name="aTool">Existing <see cref="Tool"/> to be reduced</param>
        /// <param name="quantity">Quantity to reduce by</param>
        void delete(Tool aTool, int quantity);

        /// <summary>
        /// Add a new <see cref="Member"/> to the system
        /// </summary>
        /// <param name="aMember"><see cref="Member"/> to be added</param>
        void add(Member aMember);

        /// <summary>
        /// Delete a <see cref="Member"/> from the system
        /// </summary>
        /// <param name="aMember"><see cref="Member"/> to be deleted</param>
        void delete(Member aMember);

        /// <summary>
        /// Display the tools a <see cref="Member"/> is currently borrowing
        /// </summary>
        /// <param name="aMember"><see cref="Member"/> to display</param>
        void displayBorrowingTools(Member aMember);

        /// <summary>
        /// Display all the tools of a <see cref="Tool"/> type selected by a <see cref="Member"/>
        /// </summary>
        /// <param name="aToolType">Tool to be displayed</param>
        void displayTools(string aToolType);

        /// <summary>
        /// Borrow out a <see cref="Tool"/> to a <see cref="Member"/>
        /// </summary>
        /// <param name="aMember">Borrowing member</param>
        /// <param name="aTool">Borrowed tool</param>
        void borrowTool(Member aMember, Tool aTool);

        /// <summary>
        /// Return a <see cref="iTool"/> to the tool library
        /// </summary>
        /// <param name="aMember">Borrowing member</param>
        /// <param name="aTool">Borrowed tool</param>
        void returnTool(Member aMember, Tool aTool);

        /// <summary>
        /// Get a list of <see cref="Tool"/> that are currently held by a given <see cref="Member"/>
        /// </summary>
        /// <param name="aMember">Member to search</param>
        /// <returns>Returns a list of <see cref="Tool"/> currently held by the member</returns>
        string[] listTools(Member aMember);

        /// <summary>
        /// Display top three most frequently borrowed tools by the members in the descending order by the number of times each tool has been borrowed.
        /// </summary>
        void displayTopThree();

    }
}
