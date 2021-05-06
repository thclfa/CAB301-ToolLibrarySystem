using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    //The specification of Member ADT
    interface iMember
    {

        /// <summary>
        /// get and set the first name of this member
        /// </summary>
        string FirstName
        {
            get;
            set;
        }
        /// <summary>
        /// get and set the last name of this member
        /// </summary>
        string LastName
        {
            get;
            set;
        }

        /// <summary>
        /// get and set the contact number of this member
        /// </summary>
        string ContactNumber
        {
            get;
            set;
        }

        /// <summary>
        /// get and set the password of this member
        /// </summary>
        string PIN
        {
            get;
            set;
        }

        /// <summary>
        /// get a list of tools that this memebr is currently holding
        /// </summary>
        string[] Tools
        {
            get;
        }

        /// <summary>
        /// add a given tool to the list of tools that this member is currently holding
        /// </summary>
        void addTool(Tool aTool);

        /// <summary>
        /// delete a given tool from the list of tools that this member is currently holding
        /// </summary>
        void deleteTool(Tool aTool);

        /// <summary>
        /// return a string containing the first name, lastname, and contact phone number of this memeber
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
