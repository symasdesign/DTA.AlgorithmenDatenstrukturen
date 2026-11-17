namespace Datenstrukturen.Structures;

public sealed class ChainedHashTable
{
    // Jeder Bucket verwendet die eigene einfach verkettete Liste aus Aufgabe L1.
    private readonly SinglyLinkedList[] _buckets;
    public int Count { get; private set; }
    public int BucketCount => _buckets.Length;

    public ChainedHashTable(int bucketCount = 5)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bucketCount);
        _buckets = new SinglyLinkedList[bucketCount];
        for (int i = 0; i < _buckets.Length; i++)
            _buckets[i] = new SinglyLinkedList();
    }

    // Vorgegeben: Rest auf einen gültigen Bucketindex abbilden.
    private int GetBucketIndex(int articleNumber)
    {
        int remainder = articleNumber % _buckets.Length;
        return remainder < 0 ? remainder + _buckets.Length : remainder;
    }

    public void Add(int articleNumber)
    {
        int index = GetBucketIndex(articleNumber);
        // TODO H1: Voraussetzung ist die fertige Listensuche L1.
        // Die vorhandene SinglyLinkedList in _buckets[index] verwenden.
        // Mit Contains auf ein Duplikat prüfen, neue IDs mit AddFirst einfügen.
        // Count nur für eine neue Artikelnummer erhöhen.
        throw new NotImplementedException("H1: ChainedHashTable.Add fehlt.");
    }

    public bool Contains(int articleNumber)
    {
        int index = GetBucketIndex(articleNumber);
        // TODO H2: Voraussetzung ist die fertige Listensuche L1.
        // Contains der vorhandenen SinglyLinkedList in _buckets[index] aufrufen.
        // Das Ergebnis dieser Listensuche zurückgeben.
        throw new NotImplementedException("H2: ChainedHashTable.Contains fehlt.");
    }
}
