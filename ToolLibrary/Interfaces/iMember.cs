using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    //The specification of Member ADT
    interface iMember : IComparable
    {
        /// <summary>
        /// Get and Set the first name of this member
        /// </summary>
        string FirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Get and Set the last name of this member
        /// </summary>
        string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// Get and Set the contact number of this member
        /// </summary>
        string ContactNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Get and Set the password of this member
        /// </summary>
        string PIN
        {
            get;
            set;
        }

        /// <summary>
        /// Get a list of tools that this member is currently holding
        /// </summary>
        string[] Tools
        {
            get;
        }

        /// <summary>
        /// Add a given tool to the list of tools that this member is currently holding
        /// </summary>
        /// <param name="aTool">Tool to be added</param>
        void addTool(Tool aTool);

        /// <summary>
        /// Delete a given tool from the list of tools that this member is currently holding
        /// </summary>
        /// <param name="aTool">Tool to be deleted</param>
        void deleteTool(Tool aTool);

        /// <summary>
        /// Return a string containing the first name, lastname, and contact phone number of this memeber
        /// </summary>
        /// <returns>Returns a string containing the first name, last name and contact phone number of the member</returns>
        string ToString();
    }
}
