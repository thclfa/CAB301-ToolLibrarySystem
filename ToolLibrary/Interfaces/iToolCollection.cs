using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    //The specification of ToolCollection ADT, which is used to store and manipulate a collection of tools
    interface iToolCollection
    {
        /// <summary>
        /// get the number of the types of tools in the community library
        /// </summary>
        int Number
        {
            get;
        }

        /// <summary>
        /// add a given tool to this tool collection
        /// </summary>
        /// <param name="aTool"></param>
        void add(Tool aTool);

        /// <summary>
        /// delete a given tool from this tool collection
        /// </summary>
        void delete(Tool aTool);

        /// <summary>
        /// search a given tool in this tool collection. Return true if this tool is in the tool collection; return false otherwise
        /// </summary>
        Boolean search(Tool aTool);
        
        /// <summary>
        /// output the tools in this tool collection to an array of iTool
        /// </summary>
        /// <returns></returns>
        Tool[] toArray(); 
    }
}
