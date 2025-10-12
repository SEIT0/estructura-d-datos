public class Player
{
    public string Name { get; set; }
    public MySet<InventoryItem> Inventory { get; set; }

    public Player(string name, MySet<InventoryItem> inventory)
    {
        Name = name;
        Inventory = inventory;
    }

    public override string ToString() => $"{Name} {Inventory}";
}