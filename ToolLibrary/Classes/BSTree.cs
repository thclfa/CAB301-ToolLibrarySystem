using System;
using System.Collections.Generic;
using System.Text;

namespace Assessment1_ToolLibrary
{
	public class BTreeNode
	{
		private IComparable item; // value
		private BTreeNode lchild; // reference to its left child 
		private BTreeNode rchild; // reference to its right child

		public BTreeNode(IComparable item)
		{
			this.item = item;
			lchild = null;
			rchild = null;
		}

		public IComparable Item
		{
			get { return item; }
			set { item = value; }
		}

		public BTreeNode LChild
		{
			get { return lchild; }
			set { lchild = value; }
		}

		public BTreeNode RChild
		{
			get { return rchild; }
			set { rchild = value; }
		}
	}

	public class BSTree
	{
		private BTreeNode root;

		public BSTree()
		{
			root = null;
		}

		public bool IsEmpty() => root == null;
		public bool Search(IComparable item) => Search(item, root);
		private bool Search(IComparable item, BTreeNode r)
		{
			if (r != null)
			{
				if (item.CompareTo(r.Item) == 0)
					return true;
				else
					if (item.CompareTo(r.Item) < 0)
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
			if (r == null)
				return 0;
			else
				return 1 + Count(r.LChild) + Count(r.RChild);
        }

		public BTreeNode GetNode(IComparable item) => GetNode(item, root);
		private BTreeNode GetNode(IComparable item, BTreeNode r)
        {
			if (r != null)
			{
				if (item.CompareTo(r.Item) == 0)
					return r;
				else
					if (item.CompareTo(r.Item) < 0)
					return GetNode(item, r.LChild);
				else
					return GetNode(item, r.RChild);
			}
			else 
				return null;
        }

		public IComparable[] ToArray()
        {
			IComparable[] arr = new IComparable[Count()];
			ToArray(ref arr, root, 0);

			return arr;
        }
		private void ToArray(ref IComparable[] arr, BTreeNode r, int i)
        {
			if (r == null)
				return;

			arr[i] = r.Item;
			i++;

			ToArray(ref arr, r.LChild, i);
			ToArray(ref arr, r.RChild, i);
		}

		public void Insert(IComparable item)
		{
			if (root == null)
				root = new BTreeNode(item);
			else
				Insert(item, root);
		}

		// pre: ptr != null
		// post: item is inserted to the binary search tree rooted at ptr
		private void Insert(IComparable item, BTreeNode ptr)
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
		public void Delete(IComparable item)
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

		public void PreOrderTraverse()
		{
			Console.Write("PreOrder: ");
			PreOrderTraverse(root);
			Console.WriteLine();
		}

		private void PreOrderTraverse(BTreeNode root)
		{
			if (root != null)
			{
				Console.Write(root.Item);
				PreOrderTraverse(root.LChild);
				PreOrderTraverse(root.RChild);
			}
		}

		public void InOrderTraverse()
		{
			Console.Write("InOrder: ");
			InOrderTraverse(root);
			Console.WriteLine();
		}

		private void InOrderTraverse(BTreeNode root)
		{
			if (root != null)
			{
				InOrderTraverse(root.LChild);
				Console.Write(root.Item);
				InOrderTraverse(root.RChild);
			}
		}

		public void PostOrderTraverse()
		{
			Console.Write("PostOrder: ");
			PostOrderTraverse(root);
			Console.WriteLine();
		}

		private void PostOrderTraverse(BTreeNode root)
		{
			if (root != null)
			{
				PostOrderTraverse(root.LChild);
				PostOrderTraverse(root.RChild);
				Console.Write(root.Item);
			}
		}

		public void Clear()
		{
			root = null;
		}
	}
}
