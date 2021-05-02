using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    //The specification of MemberCollection ADT, which is used to store and manipulate a collection of members
    
    interface iMemberCollection
    {
        /// <summary>
        /// Get the number of members in the community library
        /// </summary>
        int Number
        {
            get;
        }

        /// <summary>
        /// Add a new <see cref="Member"/> to the collection, duplicates will be ignored.
        /// </summary>
        /// <param name="aMember">Member to add</param>
        void add(Member aMember);

        /// <summary>
        /// Delete a given <see cref="Member"/> from the collection, a member can be deleted only when the member currently is not holding any tool
        /// </summary>
        /// <param name="aMember">Member to be removed</param>
        void delete(Member aMember);

        /// <summary>
        /// Search a given <see cref="Member"/> in collection. Return true if this memeber is in the member collection; return false otherwise.
        /// </summary>
        /// <param name="aMember">Member to search for</param>
        /// <returns>Returns true if the member is in the collection, false if otherwise</returns>
        Boolean search(Member aMember);

        /// <summary>
        /// Output the members in this collection to an array of iMember
        /// </summary>
        /// <returns>Returns this collection in the form of an array</returns>
        Member[] toArray();
    }
}
