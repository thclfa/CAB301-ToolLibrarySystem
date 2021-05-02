using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    /// <summary>
    /// The specification of ToolCollection ADT, which is used to store and manipulate a collection of tools
    /// </summary>
    interface iToolCollection
    {
        /// <summary>
        /// Get the number of the types of tools in the community library
        /// </summary>
        int Number
        {
            get;
        }

        /// <summary>
        /// Add a given tool to this tool collection
        /// </summary>
        /// <param name="aTool">Tool to be added</param>
        void add(Tool aTool);

        /// <summary>
        /// Delete a given tool from this tool collection
        /// </summary>
        /// <param name="aTool"></param>
        void delete(Tool aTool);

        /// <summary>
        /// Search a given tool in this tool collection. Return true if this tool is in the tool collection; return false otherwise
        /// </summary>
        /// <param name="aTool">Tool to be searched for</param>
        /// <returns>Returns true if the tool is in the collection, false if otherwise</returns>
        Boolean search(Tool aTool);

        /// <summary>
        /// Output the tools in this tool collection to an array of iTool
        /// </summary>
        /// <returns>Returns this collection in the form of an array</returns>
        Tool[] toArray();
    }
}
