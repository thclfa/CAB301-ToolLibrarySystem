using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    //The specification of MemberCollection ADT, which is used to store and manipulate a collection of members
    
    interface iMemberCollection
    {
        /// <summary>
        /// get the number of members in the community library
        /// </summary>
        int Number
        {
            get;
        }

        /// <summary>
        /// add a new member to this member collection, make sure there are no duplicates in the member collection
        /// </summary>
        void add(Member aMember);

        /// <summary>
        /// delete a given member from this member collection, a member can be deleted only when the member currently is not holding any tool
        /// </summary>
        void delete(Member aMember);

        /// <summary>
        /// search a given member in this member collection. Return true if this memeber is in the member collection; return false otherwise.
        /// </summary>
        Boolean search(Member aMember);

        /// <summary>
        /// output the memebers in this collection to an array of iMember
        /// </summary>
        /// <returns></returns>
        Member[] toArray();
    }
}
