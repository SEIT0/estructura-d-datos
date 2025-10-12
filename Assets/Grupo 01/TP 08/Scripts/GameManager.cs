using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text player1Text;
    public TMP_Text player2Text;
    public TMP_Text allItemsText;
    public TMP_Text resultText;
    public TMP_Dropdown differenceDropdown; 

    private Player p1;
    private Player p2;
    private InventoryItem[] allItems;

    void Start()
    {
        allItems = new InventoryItem[]
        {
            new InventoryItem("Espada de Hierro", 50),
            new InventoryItem("Escudo de Madera", 30),
            new InventoryItem("Arco Largo", 70),
            new InventoryItem("Flechas", 10),
            new InventoryItem("Poción de Vida", 25),
            new InventoryItem("Poción de Mana", 25),
            new InventoryItem("Casco de Acero", 40),
            new InventoryItem("Armadura Ligera", 60),
            new InventoryItem("Botas de Cuero", 20),
            new InventoryItem("Guantes de Hierro", 15),
            new InventoryItem("Hacha de Guerra", 80),
            new InventoryItem("Báculo Mágico", 90),
            new InventoryItem("Daga Envenenada", 55),
            new InventoryItem("Anillo de Fuerza", 100),
            new InventoryItem("Collar de Sabiduría", 120),
            new InventoryItem("Capa de Invisibilidad", 200),
            new InventoryItem("Martillo Pesado", 85),
            new InventoryItem("Lanza de Caza", 45),
            new InventoryItem("Libro de Hechizos", 150),
            new InventoryItem("Llave Misteriosa", 5)
        };

        var inv1 = new MySetList<InventoryItem>();
        var inv2 = new MySetList<InventoryItem>();

        for (int i = 0; i < 10; i++)
        {
            inv1.Add(allItems[Random.Range(0, allItems.Length)]);
            inv2.Add(allItems[Random.Range(0, allItems.Length)]);
        }

        p1 = new Player("Jugador 1", inv1);
        p2 = new Player("Jugador 2", inv2);

        // Mostrar inventarios
        player1Text.text = $"Jugador 1 ({p1.Inventory.Count} ítems):\n{p1.Inventory}";
        player2Text.text = $"Jugador 2 ({p2.Inventory.Count} ítems):\n{p2.Inventory}";

        // Mostrar todos los ítems posibles
        string all = "";
        for (int i = 0; i < allItems.Length; i++)
            all += allItems[i].ToString() + (i < allItems.Length - 1 ? ", " : "");
        allItemsText.text = "Todos los ítems posibles:\n" + all;

        resultText.text = "Selecciona una opción para ver resultados";
    }

    public void ShowCommon()
    {
        resultText.text = "Ítems en común:\n" + p1.Inventory.IntersectWith(p2.Inventory).ToString();
    }

    public void ShowUnion()
    {
        resultText.text = "Unión:\n" + p1.Inventory.UnionWith(p2.Inventory).ToString();
    }

    public void ShowDifference()
    {
        if (differenceDropdown.value == 0) // Jugador 1 - Jugador 2
        {
            resultText.text = "Diferencia (J1 - J2):\n" +
                              p1.Inventory.DifferenceWith(p2.Inventory).ToString();
        }
        else if (differenceDropdown.value == 1) // Jugador 2 - Jugador 1
        {
            resultText.text = "Diferencia (J2 - J1):\n" +
                              p2.Inventory.DifferenceWith(p1.Inventory).ToString();
        }
    }

    public void ShowNone()
    {
        var allSet = new MySetList<InventoryItem>();
        for (int i = 0; i < allItems.Length; i++)
            allSet.Add(allItems[i]);

        var union = p1.Inventory.UnionWith(p2.Inventory);
        var none = allSet.DifferenceWith(union);

        resultText.text = "Ítems que ninguno tiene:\n" + none.ToString();
    }
}