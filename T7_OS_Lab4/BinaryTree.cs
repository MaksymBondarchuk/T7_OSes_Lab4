using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace T7_OS_Lab4
{
    public class BinaryTree
    {
        private Node _head;
        private string _identifier;
        private readonly List<string> _path = new List<string>();
        public bool WasFound;

        public class Node
        {
            public readonly string Identifier;
            public Node ChildLeft;
            public Node ChildRight;
            public Node Parent;

            public Node(string identifier, Node parent)
            {
                Identifier = identifier;
                Parent = parent;

                ChildLeft = null;
                ChildRight = null;
            }
        }

        private void AddRecursive(Node currentNode)
        {
            if (string.CompareOrdinal(_identifier, currentNode.Identifier) < 0)
            {
                if (currentNode.ChildLeft == null)
                {
                    currentNode.ChildLeft = new Node(_identifier, currentNode);
                    return;
                }
                AddRecursive(currentNode.ChildLeft);
            }

            // ReSharper disable once InvertIf
            if (string.CompareOrdinal(currentNode.Identifier, _identifier) < 0)
            {
                if (currentNode.ChildRight == null)
                {
                    currentNode.ChildRight = new Node(_identifier, currentNode);
                    return;
                }
                AddRecursive(currentNode.ChildRight);
            }
        }

        public void Add(string identifier)
        {
            if (_head == null)
            {
                _head = new Node(identifier, null);
                return;
            }

            _identifier = identifier;
            AddRecursive(_head);
        }


        private static TreeViewItem ToTreeViewItemRecursive(Node currentNode)
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
            return ToTreeViewItemRecursive(_head);
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
    }
}
