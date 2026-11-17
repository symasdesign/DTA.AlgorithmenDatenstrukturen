namespace Datenstrukturen.Structures;

public sealed class BinarySearchTree
{
    private sealed class Node
    {
        public int Value { get; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(int value) => Value = value;
    }

    private Node? _root;
    public int Count { get; private set; }

    // Vorgegeben: Einfügen, ohne den Baum zu balancieren.
    // Der Index speichert jede Artikelnummer nur einmal.
    public void Add(int articleNumber)
    {
        if (_root is null)
        {
            _root = new Node(articleNumber);
            Count++;
            return;
        }

        var current = _root;
        while (true)
        {
            if (articleNumber == current.Value)
                return;

            if (articleNumber < current.Value)
            {
                if (current.Left is null)
                {
                    current.Left = new Node(articleNumber);
                    Count++;
                    return;
                }
                current = current.Left;
            }
            else
            {
                if (current.Right is null)
                {
                    current.Right = new Node(articleNumber);
                    Count++;
                    return;
                }
                current = current.Right;
            }
        }
    }

    public bool Contains(int articleNumber)
    {
        // TODO B1: Bei _root starten. Bei Gleichheit true liefern.
        // Sonst genau einen passenden Teilbaum wählen.
        // Ein leerer Teilbaum bedeutet: Nummer nicht gefunden.
        throw new NotImplementedException("B1: BinarySearchTree.Contains fehlt.");
    }
}
