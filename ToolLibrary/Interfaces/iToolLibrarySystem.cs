using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    interface iToolLibrarySystem
    {
        /// <summary>
        /// add a new tool to the system
        /// </summary>
        void add(Tool aTool);

        /// <summary>
        /// add new pieces of an existing tool to the system
        /// </summary>
        void add(Tool aTool, int quantity);

        /// <summary>
        /// delte a given tool from the system
        /// </summary>
        void delete(Tool aTool);

        /// <summary>
        /// remove some pieces of a tool from the system
        /// </summary>
        void delete(Tool aTool, int quantity);
        
        /// <summary>
        /// add a new memeber to the system
        /// </summary>
        void add(Member aMember);

        /// <summary>
        /// delete a member from the system
        /// </summary>
        void delete(Member aMember);

        /// <summary>
        /// given a member, display all the tools that the member are currently renting
        /// </summary>
        void displayBorrowingTools(Member aMember);

        /// <summary>
        /// display all the tools of a tool type selected by a member
        /// </summary>
        void displayTools(string aToolType);

        /// <summary>
        /// a member borrows a tool from the tool library
        /// </summary>
        void borrowTool(Member aMember, Tool aTool);

        /// <summary>
        /// a member return a tool to the tool library
        /// </summary>
        void returnTool(Member aMember, Tool aTool);

        /// <summary>
        /// get a list of tools that are currently held by a given member
        /// </summary>
        string[] listTools(Member aMember);

        /// <summary>
        /// Display top three most frequently borrowed tools by the members in the descending order by the number of times each tool has been borrowed.
        /// </summary>
        void displayTopThree();

    }
}
