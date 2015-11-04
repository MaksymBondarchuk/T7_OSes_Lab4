namespace T7_OS_Lab4
{
    public class BinaryTree
    {
        private Node _head;
        private string _identifier;

        private class Node
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
    }
}
