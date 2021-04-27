using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    interface iToolLibrarySystem
    {
        /// <summary>
        /// Add a new <see cref="iTool"/> to the system
        /// </summary>
        /// <param name="aTool"><see cref="iTool"/> to be added</param>
        void add(iTool aTool);

        /// <summary>
        /// Add new pieces of an existing <see cref="iTool"/> to the system
        /// </summary>
        /// <param name="aTool">Existing <see cref="iTool"/> to be extended</param>
        /// <param name="quantity">Quantity to add</param>
        void add(iTool aTool, int quantity);

        /// <summary>
        /// Delete a given <see cref="iTool"/> from the system
        /// </summary>
        /// <param name="aTool"><see cref="iTool"/> to be deleted</param>
        void delete(iTool aTool);

        /// <summary>
        /// Remove some pieces of a <see cref="iTool"/> from the system
        /// </summary>
        /// <param name="aTool">Existing <see cref="iTool"/> to be reduced</param>
        /// <param name="quantity">Quantity to reduce by</param>
        void delete(iTool aTool, int quantity);

        /// <summary>
        /// Add a new <see cref="iMember"/> to the system
        /// </summary>
        /// <param name="aMember"><see cref="iMember"/> to be added</param>
        void add(iMember aMember);

        /// <summary>
        /// Delete a <see cref="iMember"/> from the system
        /// </summary>
        /// <param name="aMember"><see cref="iMember"/> to be deleted</param>
        void delete(iMember aMember);

        /// <summary>
        /// Display the tools a <see cref="iMember"/> is currently borrowing
        /// </summary>
        /// <param name="aMember"><see cref="iMember"/> to display</param>
        void displayBorrowingTools(iMember aMember);

        /// <summary>
        /// Display all the tools of a <see cref="iTool"/> type selected by a <see cref="iMember"/>
        /// </summary>
        /// <param name="aToolType">Tool to be displayed</param>
        void displayTools(string aToolType);

        /// <summary>
        /// Borrow out a <see cref="iTool"/> to a <see cref="iMember"/>
        /// </summary>
        /// <param name="aMember">Borrowing member</param>
        /// <param name="aTool">Borrowed tool</param>
        void borrowTool(iMember aMember, iTool aTool);

        /// <summary>
        /// Return a <see cref="iTool"/> to the tool library
        /// </summary>
        /// <param name="aMember">Borrowing member</param>
        /// <param name="aTool">Borrowed tool</param>
        void returnTool(iMember aMember, iTool aTool);

        /// <summary>
        /// Get a list of <see cref="iTool"/> that are currently held by a given <see cref="iMember"/>
        /// </summary>
        /// <param name="aMember">Member to search</param>
        /// <returns>Returns a list of <see cref="iTool"/> currently held by the member</returns>
        string[] listTools(iMember aMember);

        /// <summary>
        /// Display top three most frequently borrowed tools by the members in the descending order by the number of times each tool has been borrowed.
        /// </summary>
        void displayTopTHree();

    }
}
