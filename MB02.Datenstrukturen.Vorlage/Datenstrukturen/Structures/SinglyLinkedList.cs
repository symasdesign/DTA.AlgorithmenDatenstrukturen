namespace Datenstrukturen.Structures;

public sealed class SinglyLinkedList
{
    private sealed class Node
    {
        public int Value { get; }
        public Node? Next { get; set; }

        public Node(int value) => Value = value;
    }

    private Node? _head;
    private Node? _tail;
    public int Count { get; private set; }

    // Vorgegeben: Anhängen mit einer Referenz auf den letzten Knoten.
    // Eine Liste ist eine Sequenz und darf dieselbe Nummer mehrfach enthalten.
    public void Add(int articleNumber)
    {
        var node = new Node(articleNumber);
        if (_tail is null)
            _head = node;
        else
            _tail.Next = node;
        _tail = node;
        Count++;
    }

    // Vorgegeben: Am Kopf einfügen und bei einer leeren Liste auch _tail setzen.
    public void AddFirst(int articleNumber)
    {
        var node = new Node(articleNumber) { Next = _head };
        _head = node;
        if (_tail is null)
            _tail = node;
        Count++;
    }

    public bool Contains(int articleNumber)
    {
        // TODO L1: Ab _head jeden Knoten prüfen.
        // Bei einem Treffer true liefern, am Listenende false.
        // Verwende ausschliesslich die Knoten und ihre Next-Referenz.
        throw new NotImplementedException("L1: SinglyLinkedList.Contains fehlt.");
    }
}
