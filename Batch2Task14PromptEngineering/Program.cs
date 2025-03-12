using InMemoryTable;
using static InMemoryTable.ItemMasterTable;

public class Program
{
    public static void Main(string[] args)
    {
        var itemMasterTable1 = new ItemMasterTable();
        itemMasterTable1.Add(new ItemData(1, "Item1"));
        itemMasterTable1.Add(new ItemData (2, "Item2"));
        itemMasterTable1.Add(new ItemData (3, "Item3"));

        var itemMasterTable2 = new ItemMasterTable();
        itemMasterTable2.Add(new ItemData (4, "Item4"));
        itemMasterTable2.Add(new ItemData (5, "Item5"));
        itemMasterTable2.Add(new ItemData (6, "Item6"));

        itemMasterTable1.Append(itemMasterTable2);

        Console.WriteLine((itemMasterTable1.Find(1) as ItemData).Name);
        Console.WriteLine((itemMasterTable1.Find(2) as ItemData).Name);
        Console.WriteLine((itemMasterTable1.Find(3) as ItemData).Name);
        Console.WriteLine((itemMasterTable1.Find(4) as ItemData).Name);
        Console.WriteLine((itemMasterTable1.Find(5) as ItemData).Name);
        Console.WriteLine((itemMasterTable1.Find(6) as ItemData).Name);
    }
}