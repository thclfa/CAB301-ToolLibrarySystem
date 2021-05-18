using System;
using System.Collections.Generic;
using System.Text;

namespace CAB301_ToolLibrarySystem
{
    public class BTreeNode
    {
        public BTreeNode(Member item)
        {
            Item = item;
            LChild = null;
            RChild = null;
        }

        public Member Item { get; set; }
        public BTreeNode LChild { get; set; }
        public BTreeNode RChild { get; set; }
    }

    public class BSTree
    {
        private BTreeNode root;

        public BSTree() => root = null;

        public bool IsEmpty() => root == null;
        public bool Search(Member item) => Search(item, root);
        private bool Search(Member item, BTreeNode r)
        {
            if (r != null)
            {
                int dir = item.CompareTo(r.Item);

                if (dir == 0)
                    return true;
                else if (dir < 0)
                    return Search(item, r.LChild);
                else
                    return Search(item, r.RChild);
            }
            else
                return false;
        }

        public int Count() => Count(root);
        private int Count(BTreeNode r)
        {
            if (r != null)
                return 1 + Count(r.LChild) + Count(r.RChild);
            else
                return 0;
        }

        public BTreeNode GetNode(Member item) => GetNode(item, root);
        private BTreeNode GetNode(Member item, BTreeNode ptr)
        {
            if (ptr != null)
            {
                if (item.CompareTo(ptr.Item) == 0)
                    return ptr;
                else
                    if (item.CompareTo(ptr.Item) < 0)
                    return GetNode(item, ptr.LChild);
                else
                    return GetNode(item, ptr.RChild);
            }
            else
                return null;
        }

        /// <summary>
        /// Returns BSTree as an ordered array
        /// </summary>
        /// <returns>In Order Member array sorted alphabetically by 'LastName' then 'Firstname'</returns>
        public Member[] ToArray()
        {
            int i = 0;  // Reference index, increased by 1 after every node is added
            Member[] arr = new Member[Count()];

            ToArray(root, ref i, ref arr);

            return arr;
        }
        private void ToArray(BTreeNode ptr, ref int i, ref Member[] arr)
        {
            if (ptr != null)
            {
                // Left side of tree is less than, so index it before adding parent to tree
                ToArray(ptr.LChild, ref i, ref arr);
                arr[i++] = ptr.Item;
                ToArray(ptr.RChild, ref i, ref arr);
            }
        }

        public void Insert(Member item)
        {
            if (root == null)
                root = new BTreeNode(item);
            else
                Insert(item, root);
        }

        // pre: ptr != null
        // post: item is inserted to the binary search tree rooted at ptr
        private void Insert(Member item, BTreeNode ptr)
        {
            if (item.CompareTo(ptr.Item) < 0)
            {
                if (ptr.LChild == null)
                    ptr.LChild = new BTreeNode(item);
                else
                    Insert(item, ptr.LChild);
            }
            else
            {
                if (ptr.RChild == null)
                    ptr.RChild = new BTreeNode(item);
                else
                    Insert(item, ptr.RChild);
            }
        }

        // there are three cases to consider:
        // 1. the node to be deleted is a leaf
        // 2. the node to be deleted has only one child 
        // 3. the node to be deleted has both left and right children
        public void Delete(Member item)
        {
            // search for item and its parent
            BTreeNode ptr = root; // search reference
            BTreeNode parent = null; // parent of ptr
            while ((ptr != null) && (item.CompareTo(ptr.Item) != 0))
            {
                parent = ptr;
                if (item.CompareTo(ptr.Item) < 0) // move to the left child of ptr
                    ptr = ptr.LChild;
                else
                    ptr = ptr.RChild;
            }

            if (ptr != null) // if the search was successful
            {
                // case 3: item has two children
                if ((ptr.LChild != null) && (ptr.RChild != null))
                {
                    // find the right-most node in left subtree of ptr
                    if (ptr.LChild.RChild == null) // a special case: the right subtree of ptr.LChild is empty
                    {
                        ptr.Item = ptr.LChild.Item;
                        ptr.LChild = ptr.LChild.LChild;
                    }
                    else
                    {
                        BTreeNode p = ptr.LChild;
                        BTreeNode pp = ptr; // parent of p
                        while (p.RChild != null)
                        {
                            pp = p;
                            p = p.RChild;
                        }
                        // copy the item at p to ptr
                        ptr.Item = p.Item;
                        pp.RChild = p.LChild;
                    }
                }
                else // cases 1 & 2: item has no or only one child
                {
                    BTreeNode c;
                    if (ptr.LChild != null)
                        c = ptr.LChild;
                    else
                        c = ptr.RChild;

                    // remove node ptr
                    if (ptr == root) //need to change root
                        root = c;
                    else
                    {
                        if (ptr == parent.LChild)
                            parent.LChild = c;
                        else
                            parent.RChild = c;
                    }
                }

            }
        }

        public void Clear()
        {
            root = null;
        }
    }
}
