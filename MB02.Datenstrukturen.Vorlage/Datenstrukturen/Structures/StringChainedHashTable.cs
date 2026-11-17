namespace Datenstrukturen.Structures;

// Optionale Erweiterung: Strings als Schlüssel, mit echten Bucket-Listen.
public sealed class StringChainedHashTable
{
    private readonly LinkedList<string>[] _buckets;
    public int Count { get; private set; }
    public int BucketCount => _buckets.Length;

    public StringChainedHashTable(int bucketCount = 5)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(bucketCount);
        _buckets = new LinkedList<string>[bucketCount];
        for (int i = 0; i < _buckets.Length; i++)
            _buckets[i] = new LinkedList<string>();
    }

    // Vorgegeben: Duplikate ignorieren, neue Schlüssel am Kopf einfügen.
    // Add benötigt die beiden Aufgabenmethoden S1 und S2.
    public void Add(string key)
    {
        if (Contains(key))
            return;

        int index = GetBucketIndex(key);
        _buckets[index].AddFirst(key);
        Count++;
    }

    private int GetBucketIndex(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        // TODO S1: StringComparer.Ordinal.GetHashCode(key) verwenden.
        // Den Hash mit unchecked nach uint umwandeln, dann modulo BucketCount.
        // Das Ergebnis liegt zwischen 0 und BucketCount - 1.
        // Ein leerer String ist erlaubt. Hashwerte sind nicht prozessübergreifend stabil.
        throw new NotImplementedException("S1: StringChainedHashTable.GetBucketIndex fehlt.");
    }

    public bool Contains(string key)
    {
        int index = GetBucketIndex(key);
        // TODO S2: Bei _buckets[index].First starten und über Next weitergehen.
        // Schlüssel mit StringComparer.Ordinal.Equals vergleichen.
        // Gross-/Kleinschreibung zählt. Gleicher Inhalt bedeutet einen Treffer.
        // Am Ende der Bucket-Liste false liefern.
        throw new NotImplementedException("S2: StringChainedHashTable.Contains fehlt.");
    }
}
