using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace T7_OS_Lab4
{
    public class BinaryTree
    {
        private Node _head;

        // For Add
        private string _identifier;

        // For Find
        private readonly List<string> _path = new List<string>();
        public bool WasFound;

        public class Node
        {
            public string Identifier;
            public Node ChildLeft;
            public Node ChildRight;
            public Node Parent;
            public int CountLeft;
            public int CountRight;

            public Node(string identifier, Node parent, int countLeft, int countRight)
            {
                Identifier = identifier;
                Parent = parent;

                ChildLeft = null;
                ChildRight = null;

                CountLeft = countLeft;
                CountRight = countRight;
            }

            private Node(string identifier, Node parent, Node childLeft, Node childRight, int countLeft, int countRight)
            {
                Identifier = identifier;
                Parent = parent;

                ChildLeft = null;
                ChildRight = null;

                CountLeft = countLeft;
                CountRight = countRight;

                ChildLeft = childLeft;
                ChildRight = childRight;
            }

            public Node Copy()
            {
                return new Node(Identifier, Parent, ChildLeft, ChildRight, CountLeft, CountRight);
            }
        }

        private void Balance(Node node)
        {
            if (node.CountRight - node.CountLeft == 2)
            {
                // Small left
                if (node.ChildRight.CountLeft <= node.ChildRight.CountRight)
                {
                    var a = node;
                    var b = node.ChildRight;
                    var c = b.ChildLeft;

                    //var a = node.Copy();
                    //var b = node.ChildRight.Copy();
                    //var c = b.ChildLeft.Copy();

                    b.Parent = a.Parent;
                    a.Parent = b;
                    if (c != null)
                        c.Parent = a;

                    b.ChildLeft = a;
                    a.ChildRight = c;

                    if (c != null)
                        a.CountRight = c.CountLeft + c.CountRight + 1;
                    else a.CountRight = 0;
                    b.CountLeft = a.CountLeft + a.CountRight + 1;
                }
                else
                {
                    // Big left
                    var a = node;
                    var b = a.ChildRight;
                    var c = b.ChildLeft;
                    var m = c.ChildLeft;
                    var n = c.ChildRight;

                    //var a = node.Copy();
                    //var b = a.ChildRight.Copy();
                    //var c = b.ChildLeft.Copy();
                    //var m = c.ChildLeft.Copy();
                    //var n = c.ChildRight.Copy();

                    c.Parent = a.Parent;
                    a.Parent = c;
                    b.Parent = c;
                    if (m != null)
                        m.Parent = a;
                    if (n != null)
                        n.Parent = b;

                    a.ChildRight = m;
                    b.ChildLeft = n;
                    c.ChildLeft = a;
                    c.ChildRight = b;

                    if (m != null)
                        a.CountRight = m.CountLeft + m.CountRight + 1;
                    else a.CountRight = 0;
                    if (n != null)
                        b.CountLeft = n.CountLeft + n.CountRight + 1;
                    else b.CountLeft = 0;
                    c.CountLeft = a.CountLeft + a.CountRight + 1;
                    c.CountRight = b.CountLeft + b.CountRight + 1;
                }
                return;
            }

            if (node.CountRight - node.CountLeft == 2)
            {
                // Small right
                if (node.CountRight <= node.CountLeft)
                {
                    var a = node;
                    var b = node.ChildLeft;
                    var c = b.ChildRight;

                    //var a = node.Copy();
                    //var b = node.ChildLeft.Copy();
                    //var c = b.ChildRight.Copy();

                    b.Parent = a.Parent;
                    a.Parent = b;
                    if (c != null)
                        c.Parent = a;

                    a.ChildLeft = c;
                    b.ChildRight = a;

                    if (c != null)
                        a.CountLeft = c.CountLeft + c.CountRight + 1;
                    else a.CountRight = 0;
                    b.CountRight = a.CountLeft + a.CountRight + 1;
                }
                else
                {
                    var a = node;
                    var b = a.ChildRight;
                    var c = b.ChildRight;
                    var m = c.ChildLeft;
                    var n = c.ChildRight;

                    //var a = node.Copy();
                    //var b = a.ChildRight.Copy();
                    //var c = b.ChildRight.Copy();
                    //var m = c.ChildLeft.Copy();
                    //var n = c.ChildRight.Copy();

                    c.Parent = a.Parent;
                    a.Parent = c;
                    b.Parent = c;
                    if (m != null)
                        m.Parent = b;
                    if (n != null)
                        n.Parent = a;

                    b.ChildRight = m;
                    a.ChildLeft = n;
                    c.ChildLeft = b;
                    c.ChildRight = a;

                    if (m != null)
                        b.CountRight = m.CountLeft + m.CountRight + 1;
                    else b.CountRight = 0;
                    if (n != null)
                        a.CountLeft = n.CountLeft + n.CountRight + 1;
                    else a.CountLeft = 0;
                    c.CountLeft = b.CountLeft + b.CountRight + 1;
                    c.CountRight = a.CountLeft + a.CountRight + 1;
                }
            }
        }

        private void AddRecursive(Node currentNode)
        {
            if (string.CompareOrdinal(_identifier, currentNode.Identifier) < 0)
            {
                if (currentNode.ChildLeft == null)
                {
                    currentNode.ChildLeft = new Node(_identifier, currentNode, 0, 0);
                    currentNode.CountLeft++;
                    return;
                }
                currentNode.CountLeft++;
                AddRecursive(currentNode.ChildLeft);
                if (Math.Abs(currentNode.CountLeft - currentNode.CountRight) == 2)
                    Balance(currentNode);
            }

            // ReSharper disable once InvertIf
            if (string.CompareOrdinal(currentNode.Identifier, _identifier) < 0)
            {
                if (currentNode.ChildRight == null)
                {
                    currentNode.ChildRight = new Node(_identifier, currentNode, 0, 0);
                    currentNode.CountRight++;
                    return;
                }
                currentNode.CountRight++;
                AddRecursive(currentNode.ChildRight);
                if (Math.Abs(currentNode.CountLeft - currentNode.CountRight) == 2)
                    Balance(currentNode);
            }
        }

        public void Add(string identifier)
        {
            if (_head == null)
            {
                _head = new Node(identifier, null, 0, 0);
                return;
            }

            _identifier = identifier;
            AddRecursive(_head);
        }


        private TreeViewItem ToTreeViewItemRecursive(Node currentNode)
        {
            var node = new TreeViewItem
            {
                Header = currentNode.Identifier,
                IsExpanded = true
            };

            if (currentNode.ChildLeft == null && currentNode.ChildRight == null)
                node.ItemsSource = new object[] { };
            else if (currentNode.ChildLeft == null && currentNode.ChildRight != null)
                node.ItemsSource = new object[] { "<None>", ToTreeViewItemRecursive(currentNode.ChildRight) };
            else if (currentNode.ChildLeft != null && currentNode.ChildRight == null)
                node.ItemsSource = new object[] { ToTreeViewItemRecursive(currentNode.ChildLeft), "<None>" };
            else node.ItemsSource = new object[] { ToTreeViewItemRecursive(currentNode.ChildLeft), ToTreeViewItemRecursive(currentNode.ChildRight) };

            return node;
        }

        public TreeViewItem ToTreeViewItem()
        {
            return _head != null ? ToTreeViewItemRecursive(_head) : null;
        }

        private Node FindNodeRecursive(Node currentNode)
        {
            if (currentNode == null)
                return null;
            _path.Add(currentNode.Identifier);
            return _identifier == currentNode.Identifier ? currentNode : FindNodeRecursive(string.CompareOrdinal(_identifier, currentNode.Identifier) < 0 ? currentNode.ChildLeft : currentNode.ChildRight);
        }

        private Node FindNode(string identifier)
        {
            _identifier = identifier;
            return FindNodeRecursive(_head);
        }

        public IEnumerable<string> Find(string identifier)
        {
            _path.Clear();
            WasFound = FindNode(identifier) != null;
            return _path;
        }

        private void RemoveNode(Node node)
        {
            // Has no children
            if (node.ChildLeft == null && node.ChildRight == null)
            {
                if (node.Parent.ChildLeft == node)
                    node.Parent.ChildLeft = null;
                else node.Parent.ChildRight = null;
                return;
            }

            // Has only one child
            if (node.ChildLeft == null)
            {
                if (node.Parent.ChildLeft == node)
                    node.Parent.ChildLeft = node.ChildRight;
                else node.Parent.ChildRight = node.ChildRight;
                node.ChildRight.Parent = node.Parent;
                return;
            }

            if (node.ChildRight == null)
            {
                if (node.Parent.ChildLeft == node)
                    node.Parent.ChildLeft = node.ChildLeft;
                else
                    node.Parent.ChildRight = node.ChildLeft;
                node.ChildLeft.Parent = node.Parent;
                return;
            }

            // Has 2 children
            // * find a minimum value in the right subtree;
            // * replace value of the node to be removed with found minimum.Now, right subtree contains a duplicate!;
            // * apply remove to the right subtree to remove a duplicate;
            var minNode = node.ChildRight;
            while (minNode.ChildLeft != null)
                minNode = minNode.ChildLeft;

            node.Identifier = minNode.Identifier;
            RemoveNode(minNode);
        }

        public void Remove(string identifier)
        {
            var node = FindNode(identifier);
            if (node != null)
                RemoveNode(node);
        }
    }
}
