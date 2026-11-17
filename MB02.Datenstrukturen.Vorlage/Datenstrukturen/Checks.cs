using Datenstrukturen.Structures;

namespace Datenstrukturen;

internal static class Checks
{
    public static int Run(string area)
    {
        if (area is not ("alle" or "liste" or "baum" or "hash" or "strings"))
        {
            Console.WriteLine("Testbereich: liste, baum, hash, alle oder strings (optional).");
            return 1;
        }

        int passed = 0, open = 0, failed = 0;
        void Check(string name, Action action)
        {
            try
            {
                action();
                passed++;
                Console.WriteLine($"OK      {name}");
            }
            catch (NotImplementedException ex)
            {
                open++;
                Console.WriteLine($"OFFEN   {name}: {ex.Message}");
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"FEHLER  {name}: {ex.Message}");
            }
        }

        if (area is "alle" or "liste")
        {
            Check("Liste / leere Liste", () => Expect(!new SinglyLinkedList().Contains(12)));
            Check("Liste / einzelner Knoten", () => {
                var list = ListOf(12);
                Expect(list.Contains(12));
                Expect(!list.Contains(17));
            });
            Check("Liste / erster, mittlerer und letzter Knoten", () => {
                var list = ListOf(12, 7, 22, 2, 17, 27, 9);
                foreach (int id in new[] { 12, 2, 9 }) Expect(list.Contains(id));
            });
            Check("Liste / fehlender Wert", () => Expect(!ListOf(12, 7, 22).Contains(17)));
            Check("Liste / wiederholte Nummern bleiben suchbar", () => {
                var list = ListOf(12, 12, 17);
                Expect(list.Count == 3);
                Expect(list.Contains(12));
                Expect(list.Contains(17));
            });
            Check("Liste / 0 und negative Werte", () => {
                var list = ListOf(0, -1, int.MinValue);
                Expect(list.Contains(0));
                Expect(list.Contains(-1));
                Expect(list.Contains(int.MinValue));
                Expect(!list.Contains(-2));
            });
        }

        if (area is "alle" or "baum")
        {
            Check("Baum / leerer Baum", () => Expect(!new BinarySearchTree().Contains(12)));
            Check("Baum / Wurzel und beide Teilbäume", () => {
                var tree = TreeOf(12, 7, 22, 2, 17, 27, 9);
                foreach (int id in new[] { 12, 7, 22, 2, 17, 27, 9 }) Expect(tree.Contains(id));
            });
            Check("Baum / fehlende Werte links, innen und rechts", () => {
                var tree = TreeOf(12, 7, 22, 2, 17, 27, 9);
                foreach (int id in new[] { 1, 8, 99 }) Expect(!tree.Contains(id));
            });
            Check("Baum / aufsteigend aufgebauter Baum", () => {
                var tree = TreeOf(2, 7, 9, 12, 17, 22, 27);
                Expect(tree.Contains(27));
                Expect(!tree.Contains(28));
            });
            Check("Baum / absteigend aufgebauter Baum", () => {
                var tree = TreeOf(27, 22, 17, 12, 9, 7, 2);
                Expect(tree.Contains(2));
                Expect(!tree.Contains(1));
            });
            Check("Baum / Duplikat ignorieren", () => {
                var tree = TreeOf(12, 7, 12, 7);
                Expect(tree.Count == 2);
                Expect(tree.Contains(12));
            });
            Check("Baum / 0 und negative Werte", () => {
                var tree = TreeOf(0, -1, int.MinValue);
                Expect(tree.Contains(0));
                Expect(tree.Contains(-1));
                Expect(tree.Contains(int.MinValue));
                Expect(!tree.Contains(-2));
            });
        }

        if (area is "alle" or "hash")
        {
            Check("Hash / leere Tabelle", () => Expect(!new ChainedHashTable().Contains(12)));
            Check("Hash / einzelner Wert", () => {
                var table = HashOf(5, 12);
                Expect(table.Count == 1);
                Expect(table.Contains(12));
                Expect(!table.Contains(17));
            });
            Check("Hash / Kollisionen erhalten alle Nummern", () => {
                var table = HashOf(5, 12, 17, 22);
                Expect(table.Count == 3);
                foreach (int id in new[] { 12, 17, 22 }) Expect(table.Contains(id));
            });
            Check("Hash / fehlende Nummer im belegten Bucket", () => {
                var table = HashOf(5, 12, 17, 22);
                Expect(!table.Contains(27));
            });
            Check("Hash / fehlende Nummer im leeren Bucket", () => {
                var table = HashOf(5, 12, 17, 22);
                Expect(!table.Contains(9));
            });
            Check("Hash / gemeinsamer Datensatz und Duplikat", () => {
                var table = HashOf(5, 12, 7, 22, 2, 17, 27, 9);
                Expect(table.Count == 7);
                table.Add(12);
                Expect(table.Count == 7);
                foreach (int id in new[] { 12, 7, 22, 2, 17, 27, 9 }) Expect(table.Contains(id));
                Expect(!table.Contains(32));
            });
            Check("Hash / Duplikat ohne neuen Knoten", () => {
                var table = HashOf(5, 12, 17, 12, 17);
                Expect(table.Count == 2);
                Expect(table.Contains(12));
                Expect(table.Contains(17));
            });
            Check("Hash / nur ein Bucket", () => {
                var table = HashOf(1, 12, 7, 22);
                foreach (int id in new[] { 12, 7, 22 }) Expect(table.Contains(id));
                Expect(!table.Contains(17));
            });
            Check("Hash / maximale positive int-Nummer", () => {
                var table = HashOf(5, int.MaxValue);
                Expect(table.Contains(int.MaxValue));
            });
            Check("Hash / 0 und negative Werte", () => {
                var table = HashOf(5, 0, -1, int.MinValue);
                Expect(table.Contains(0));
                Expect(table.Contains(-1));
                Expect(table.Contains(int.MinValue));
                Expect(!table.Contains(-2));
            });
            Check("Hash / Bucketanzahl muss positiv sein", () => {
                ExpectOutOfRange(() => new ChainedHashTable(0));
            });
        }

        // Die optionale Erweiterung gehört nicht zu den Kernprüfungen in "alle".
        if (area == "strings")
        {
            Check("Strings / leere Tabelle", () => {
                var table = new StringChainedHashTable();
                Expect(table.Count == 0);
                Expect(table.BucketCount == 5);
                Expect(!table.Contains("ART-17"));
            });
            Check("Strings / leerer Schlüssel ist erlaubt", () => {
                var table = new StringChainedHashTable();
                Expect(!table.Contains(""));
                table.Add("");
                table.Add(string.Empty);
                Expect(table.Count == 1);
                Expect(table.Contains(""));
            });
            Check("Strings / gleicher Inhalt in verschiedenen Instanzen", () => {
                string first = new string("ART-17".ToCharArray());
                string second = new string("ART-17".ToCharArray());
                Expect(!ReferenceEquals(first, second));
                var table = new StringChainedHashTable();
                table.Add(first);
                Expect(table.Contains(second));
                table.Add(second);
                Expect(table.Count == 1);
            });
            Check("Strings / Kollisionen und fehlender Schlüssel im selben Bucket", () => {
                var table = new StringChainedHashTable(1);
                foreach (string key in new[] { "ART-17", "ART-18", "ART-19", "ART-20" })
                    table.Add(key);
                Expect(table.Count == 4);
                foreach (string key in new[] { "ART-17", "ART-18", "ART-19", "ART-20" })
                    Expect(table.Contains(key));
                Expect(!table.Contains("ART-99"));
                table.Add(new string("ART-18".ToCharArray()));
                Expect(table.Count == 4);
                Expect(table.Contains("ART-17"));
                Expect(table.Contains("ART-20"));
            });
            Check("Strings / Gross- und Kleinschreibung unterscheiden", () => {
                var table = new StringChainedHashTable(1);
                table.Add("ART-17");
                Expect(!table.Contains("art-17"));
                table.Add("art-17");
                Expect(table.Count == 2);
                Expect(table.Contains("ART-17"));
                Expect(table.Contains("art-17"));
            });
            Check("Strings / Unicode ordinal ohne Normalisierung", () => {
                var table = new StringChainedHashTable();
                string[] keys = { "Grün", "東京", "🙂", "é", "e\u0301" };
                foreach (string key in keys) table.Add(key);
                Expect(table.Count == keys.Length);
                foreach (string key in keys) Expect(table.Contains(key));
                Expect(!table.Contains("grün"));
            });
            Check("Strings / verschiedene Bucket-Anzahlen", () => {
                foreach (int bucketCount in new[] { 2, 5, 11 })
                {
                    var table = new StringChainedHashTable(bucketCount);
                    for (int i = 0; i < 40; i++) table.Add($"ART-{i}");
                    Expect(table.Count == 40);
                    for (int i = 0; i < 40; i++) Expect(table.Contains($"ART-{i}"));
                    Expect(!table.Contains("ART-fehlt"));
                }
            });
            Check("Strings / null ablehnen", () => {
                var table = new StringChainedHashTable();
                ExpectNull(() => table.Add(null!));
                ExpectNull(() => table.Contains(null!));
            });
            Check("Strings / Bucketanzahl muss positiv sein", () => {
                ExpectOutOfRange(() => new StringChainedHashTable(0));
                ExpectOutOfRange(() => new StringChainedHashTable(-1));
            });
        }

        Console.WriteLine($"\nErgebnis: {passed} OK, {open} OFFEN, {failed} FEHLER");
        Console.WriteLine("OK bestätigt nur den jeweiligen Prüffall.");
        Console.WriteLine("OFFEN bedeutet: Eine benötigte TODO-Methode fehlt noch.");
        return failed > 0 ? 1 : open > 0 ? 2 : 0;
    }

    private static void Expect(bool condition)
    {
        if (!condition) throw new InvalidOperationException("Erwartung nicht erfüllt.");
    }

    private static void ExpectOutOfRange(Action action)
    {
        try { action(); }
        catch (ArgumentOutOfRangeException) { return; }
        throw new InvalidOperationException("ArgumentOutOfRangeException erwartet.");
    }

    private static void ExpectNull(Action action)
    {
        try { action(); }
        catch (ArgumentNullException) { return; }
        throw new InvalidOperationException("ArgumentNullException erwartet.");
    }

    private static SinglyLinkedList ListOf(params int[] ids)
    {
        var list = new SinglyLinkedList();
        foreach (int id in ids) list.Add(id);
        return list;
    }

    private static BinarySearchTree TreeOf(params int[] ids)
    {
        var tree = new BinarySearchTree();
        foreach (int id in ids) tree.Add(id);
        return tree;
    }

    private static ChainedHashTable HashOf(int buckets, params int[] ids)
    {
        var table = new ChainedHashTable(buckets);
        foreach (int id in ids) table.Add(id);
        return table;
    }
}
