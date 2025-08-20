using UnityEngine;
using UnityEngine.UI;

public class LinkedListButton : MonoBehaviour
{
    public Text displayText;              // Mostrar la lista y mensajes
    public InputField inputField;         // Escribir el texto, Add y Remove
    public InputField addRangeInputField; // Escribir varios elementos separados por coma, AddRange

    private MyLinkedList<string> list;

    void Start()
    {
        list = new MyLinkedList<string>();
        UpdateDisplay();
    }

    public void AddButton()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            list.Add(inputField.text);
            inputField.text = "";
            UpdateDisplay();
        }
    }

    public void AddRangeButton()
    {
        if (!string.IsNullOrEmpty(addRangeInputField.text))
        {
            string[] items = addRangeInputField.text.Split(',');
            for (int i = 0; i < items.Length; i++)
                items[i] = items[i].Trim();

            list.AddRange(items);
            addRangeInputField.text = "";
            UpdateDisplay();
        }
    }

    public void RemoveButton()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            bool removed = list.Remove(inputField.text);
            inputField.text = "";
            UpdateDisplay();
            return;
        }

        // Si no se ingresó texto, eliminamos el último (como hace tu escena original)
        if (list.Count > 0)
        {
            list.RemoveAt(list.Count - 1);
            UpdateDisplay();
        }
    }

    public void ClearButton()
    {
        list.Clear();
        UpdateDisplay();
        displayText.text = "Lista limpiada";
    }

    public void CountButton()
    {
        displayText.text = "Cantidad de elementos: " + list.Count;
    }

    private void UpdateDisplay()
    {
        displayText.text = "-" + list.ToString();
        displayText.text += "\nCantidad de elementos: " + list.Count;
    }
}