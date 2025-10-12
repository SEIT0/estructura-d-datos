public class InventoryItem
{
    public string Name { get; set; }
    public float Price { get; set; }

    public InventoryItem(string name, float price)
    {
        Name = name;
        Price = price;
    }

    public override string ToString() => $"{Name} (${Price})";

    // Importante: para que los sets funcionen bien, redefinimos Equals y GetHashCode
    public override bool Equals(object obj)
    {
        if (obj is InventoryItem other)
            return Name == other.Name; // igualdad por nombre
        return false;
    }

    public override int GetHashCode() => Name.GetHashCode();
}