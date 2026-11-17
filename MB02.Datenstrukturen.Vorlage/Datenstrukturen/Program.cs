using System.Text;
using Datenstrukturen.Structures;

namespace Datenstrukturen;

internal static class Program
{
    private static readonly int[] ArticleNumbers = { 12, 7, 22, 2, 17, 27, 9 };

    private static int Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        if (args.Length > 0)
        {
            if (args[0] == "--test")
                return Checks.Run(args.Length > 1 ? args[1].ToLowerInvariant() : "alle");
            if (args[0] == "--demo")
            {
                Demo();
                return 0;
            }
            if (args[0] == "--demo-strings")
            {
                DemoStrings();
                return 0;
            }
            Console.WriteLine("Aufruf: --test [liste|baum|hash|alle|strings], --demo oder --demo-strings");
            return 1;
        }

        while (true)
        {
            Console.WriteLine("\nARTIKELNUMMERN FINDEN");
            Console.WriteLine("1  Tests Liste      2  Tests Suchbaum      3  Tests Hashtabelle");
            Console.WriteLine("4  Alle Kerntests   5  Gemeinsame Demo     0  Beenden");
            Console.WriteLine("Optional: 6  Tests Strings     7  String-Demo");
            Console.Write("Auswahl: ");
            switch (Console.ReadLine())
            {
                case "1": Checks.Run("liste"); break;
                case "2": Checks.Run("baum"); break;
                case "3": Checks.Run("hash"); break;
                case "4": Checks.Run("alle"); break;
                case "5": Demo(); break;
                case "6": Checks.Run("strings"); break;
                case "7": DemoStrings(); break;
                case "0":
                case null: return 0;
                default: Console.WriteLine("Bitte 0 bis 7 eingeben."); break;
            }
        }
    }

    private static void Demo()
    {
        Console.WriteLine("\nEinfügereihenfolge: " + string.Join(", ", ArticleNumbers));
        Console.WriteLine("Gesucht werden die vorhandene Nummer 17 und die fehlenden Nummern 8 und 32.");
        RunDemo("Liste", () => {
            var list = new SinglyLinkedList();
            foreach (int id in ArticleNumbers) list.Add(id);
            PrintResult(list.Contains);
        });
        RunDemo("Suchbaum", () => {
            var tree = new BinarySearchTree();
            foreach (int id in ArticleNumbers) tree.Add(id);
            PrintResult(tree.Contains);
        });
        RunDemo("Hashtabelle mit 5 Buckets", () => {
            var table = new ChainedHashTable(5);
            foreach (int id in ArticleNumbers) table.Add(id);
            PrintResult(table.Contains);
        });
        Console.WriteLine("Buckets: 12, 7, 22, 2, 17 und 27 haben Rest 2. Die Nummer 9 hat Rest 4.");
        Console.WriteLine("Wie viele Knoten besuchst du bei der Suche nach 17? Begründe am Modell.");
    }

    private static void DemoStrings()
    {
        Console.WriteLine("\nOPTIONALE ERWEITERUNG: STRING-SCHLÜSSEL");
        Console.WriteLine("Ein Bucket erzwingt Kollisionen für alle Schlüssel.");
        Console.WriteLine("Erwartung: ART-17 und ART-18 vorhanden, ART-99 und art-17 fehlen.");
        RunDemo("String-Hashtabelle", () => {
            var table = new StringChainedHashTable(1);
            table.Add("ART-17");
            table.Add("ART-18");
            table.Add(new string("ART-17".ToCharArray()));
            Console.WriteLine($"Count: {table.Count} (erwartet: 2)");
            foreach (string key in new[] { "ART-17", "ART-18", "ART-99", "art-17" })
                Console.WriteLine($"{key}: {table.Contains(key)}");
        });
        Console.WriteLine("Ordinaler Vergleich: Gross-/Kleinschreibung zählt.");
        Console.WriteLine("Hashwerte können sich zwischen Programmstarts ändern.");
    }

    private static void PrintResult(Func<int, bool> contains)
    {
        Console.WriteLine($"17: {contains(17)}; 8: {contains(8)}; 32: {contains(32)}");
    }

    private static void RunDemo(string name, Action action)
    {
        Console.Write($"{name}: ");
        try { action(); }
        catch (NotImplementedException ex) { Console.WriteLine($"OFFEN ({ex.Message})"); }
        catch (Exception ex) { Console.WriteLine($"FEHLER ({ex.Message})"); }
    }
}
