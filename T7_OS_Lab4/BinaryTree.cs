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
            public int Height;

            public Node(string identifier, Node parent, int height)
            {
                Identifier = identifier;
                Parent = parent;

                ChildLeft = null;
                ChildRight = null;

                Height = height;
            }

            public void UpdateHeight()
            {
                if (ChildLeft == null && ChildRight == null)
                    Height = 1;
                else if (ChildLeft == null && ChildRight != null)
                    Height = ChildRight.Height + 1;
                else if (ChildLeft != null && ChildRight == null)
                    Height = ChildLeft.Height + 1;
                else if (ChildLeft != null && ChildRight != null)
                    Height = Math.Max(ChildLeft.Height, ChildRight.Height) + 1;
            }

            public bool NeedBalance()
            {
                return ChildRight == null && ChildLeft != null && ChildLeft.Height == 2 ||
                       ChildRight != null && ChildLeft == null && ChildRight.Height == 2 ||
                       ChildRight != null && ChildLeft != null && Math.Abs(ChildLeft.Height - ChildRight.Height) == 2;
            }
        }

        private void Balance(Node node)
        {
            if (node.ChildLeft == null && node.ChildRight != null && node.ChildRight.Height == 2 ||
                node.ChildLeft != null && node.ChildRight != null && node.ChildRight.Height - node.ChildLeft.Height == 2)
            {
                var a = node;
                var b = node.ChildRight;
                var c = b.ChildLeft;

                // Small left
                if (c == null || b.ChildRight != null && c.Height <= b.ChildRight.Height)
                {
                    if (a.Parent != null)
                        if (a.Parent.ChildLeft == a)
                            a.Parent.ChildLeft = b;
                        else a.Parent.ChildRight = b;

                    b.Parent = a.Parent;
                    a.Parent = b;
                    if (c != null)
                        c.Parent = a;

                    b.ChildLeft = a;
                    a.ChildRight = c;

                    a.UpdateHeight();
                    b.UpdateHeight();

                    if (_head == a)
                        _head = b;
                }
                else
                {
                    // Big left
                    var m = c.ChildLeft;
                    var n = c.ChildRight;

                    if (a.Parent != null)
                        if (a.Parent.ChildLeft == a)
                            a.Parent.ChildLeft = c;
                        else a.Parent.ChildRight = c;

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

                    a.UpdateHeight();
                    b.UpdateHeight();
                    c.UpdateHeight();

                    if (_head == a)
                        _head = c;
                }
                return;
            }

            // ReSharper disable once InvertIf
            if (node.ChildRight == null && node.ChildLeft != null && node.ChildLeft.Height == 2 ||
                node.ChildRight != null && node.ChildLeft != null && node.ChildLeft.Height - node.ChildRight.Height == 2)
            {
                var a = node;
                var b = a.ChildLeft;
                var c = b.ChildRight;

                // Small right
                if (c == null || b.ChildLeft != null && c.Height <= b.ChildLeft.Height)
                {
                    if (a.Parent != null)
                        if (a.Parent.ChildLeft == a)
                            a.Parent.ChildLeft = b;
                        else a.Parent.ChildRight = b;

                    b.Parent = a.Parent;
                    a.Parent = b;

                    if (c != null)
                        c.Parent = a;


                    a.ChildLeft = c;
                    b.ChildRight = a;

                    a.UpdateHeight();
                    b.UpdateHeight();

                    if (_head == a)
                        _head = b;
                }
                else
                {
                    var m = c.ChildLeft;
                    var n = c.ChildRight;

                    if (a.Parent != null)
                        if (a.Parent.ChildLeft == a)
                            a.Parent.ChildLeft = c;
                        else a.Parent.ChildRight = c;

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

                    a.UpdateHeight();
                    b.UpdateHeight();
                    c.UpdateHeight();

                    if (_head == a)
                        _head = c;
                }
            }
        }

        private void AddRecursive(Node node)
        {
            if (string.CompareOrdinal(_identifier, node.Identifier) < 0)
            {
                if (node.ChildLeft == null)
                {
                    node.ChildLeft = new Node(_identifier, node, 1);
                    node.Height = 2;
                    return;
                }
                AddRecursive(node.ChildLeft);
                node.UpdateHeight();
                if (node.NeedBalance())
                    Balance(node);
            }

            // ReSharper disable once InvertIf
            if (string.CompareOrdinal(node.Identifier, _identifier) < 0)
            {
                if (node.ChildRight == null)
                {
                    node.ChildRight = new Node(_identifier, node, 1);
                    node.Height = 2;
                    return;
                }
                AddRecursive(node.ChildRight);
                node.UpdateHeight();
                if (node.NeedBalance())
                    Balance(node);
            }
        }

        public void Add(string identifier)
        {
            if (_head == null)
            {
                _head = new Node(identifier, null, 1);
                return;
            }

            _identifier = identifier;
            AddRecursive(_head);
        }


        private TreeViewItem ToTreeViewItemRecursive(Node node)
        {
            var nodeNew = new TreeViewItem
            {
                Header = node.Identifier,
                //Header = $"{node.Identifier,4}    ({node.Height})",
                IsExpanded = true
            };

            if (node.ChildLeft == null && node.ChildRight == null)
                nodeNew.ItemsSource = new object[] { };
            else if (node.ChildLeft == null && node.ChildRight != null)
                nodeNew.ItemsSource = new object[] { "<None>", ToTreeViewItemRecursive(node.ChildRight) };
            else if (node.ChildLeft != null && node.ChildRight == null)
                nodeNew.ItemsSource = new object[] { ToTreeViewItemRecursive(node.ChildLeft), "<None>" };
            else nodeNew.ItemsSource = new object[] { ToTreeViewItemRecursive(node.ChildLeft), ToTreeViewItemRecursive(node.ChildRight) };

            return nodeNew;
        }

        public TreeViewItem ToTreeViewItem()
        {
            return _head != null ? ToTreeViewItemRecursive(_head) : null;
        }

        private Node FindNodeRecursive(Node node)
        {
            if (node == null)
                return null;
            _path.Add(node.Identifier);
            return _identifier == node.Identifier ? node : FindNodeRecursive(string.CompareOrdinal(_identifier, node.Identifier) < 0 ? node.ChildLeft : node.ChildRight);
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

        private void RemoveBalance(Node node)
        {
            while (node.Parent != null)
            {
                if (node.Parent.NeedBalance())
                    Balance(node.Parent);
                else
                    node.Parent.UpdateHeight();
                node = node.Parent;
            }
        }

        private void RemoveNode(Node node)
        {
            // Is leaf
            if (node.ChildLeft == null && node.ChildRight == null)
            {
                // If removing head
                if (node.Parent == null)
                {
                    _head = null;
                    return;
                }

                if (node.Parent.ChildLeft == node)
                    node.Parent.ChildLeft = null;
                else node.Parent.ChildRight = null;

                RemoveBalance(node);
                return;
            }

            // Has only one child
            if (node.ChildLeft == null)
            {
                if (_head == node)
                    _head = node.ChildLeft;

                if (node.Parent.ChildLeft == node)
                    node.Parent.ChildLeft = node.ChildRight;
                else node.Parent.ChildRight = node.ChildRight;
                node.ChildRight.Parent = node.Parent;

                RemoveBalance(node);
                return;
            }

            if (node.ChildRight == null)
            {
                if (_head == node)
                    _head = node.ChildRight;

                if (node.Parent.ChildLeft == node)
                    node.Parent.ChildLeft = node.ChildLeft;
                else
                    node.Parent.ChildRight = node.ChildLeft;
                node.ChildLeft.Parent = node.Parent;

                RemoveBalance(node);
                return;
            }

            // Has 2 children
            // * find a min value in the right subtree or max value in the left subtree;
            // * replace value of the node to be removed with found minimum. Now, right subtree contains a duplicate!;
            // * apply remove to the right subtree to remove a duplicate;
            if (node.ChildLeft.Height >= node.ChildRight.Height)
            {
                var maxNode = node.ChildRight;
                while (maxNode.ChildLeft != null)
                    maxNode = maxNode.ChildRight;

                node.Identifier = maxNode.Identifier;
                RemoveNode(maxNode);
            }
            else
            {
                var minNode = node.ChildRight;
                while (minNode.ChildLeft != null)
                    minNode = minNode.ChildLeft;

                node.Identifier = minNode.Identifier;
                RemoveNode(minNode);
            }
        }

        public void Remove(string identifier)
        {
            var node = FindNode(identifier);
            if (node != null)
                RemoveNode(node);
        }
    }
}
