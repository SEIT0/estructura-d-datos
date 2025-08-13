using UnityEngine;
using UnityEngine.UI;

public class SimpleListButton : MonoBehaviour
{
    public Text displayText;  //Mostrar la lista y mensajes
    public InputField inputField; //Escribir el texto, Add y Remove
    public InputField addRangeInputField; //Escribir varios elementos separados por coma, AddRange

    private SimpleList<string> simpleList;

    void Start()
    {
        simpleList = new SimpleList<string>();
        UpdateDisplay();
    }

    public void AddButton()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            simpleList.Add(inputField.text);
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
            {
                items[i] = items[i].Trim();
            }
            simpleList.AddRange(items);
            addRangeInputField.text = "";
            UpdateDisplay();
        }
    }

    public void RemoveButton()
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            bool removed = simpleList.Remove(inputField.text);            
            inputField.text = "";
            UpdateDisplay();
        }
    }

    public void RemoveLastButton()
    {
        bool removed = simpleList.RemoveLast();        
        UpdateDisplay();
    }

    public void ClearButton()
    {
        simpleList.Clear();
        UpdateDisplay();
        displayText.text = "Lista limpiada";
    }

    public void CountButton()
    {
        displayText.text = "Cantidad de elementos: " + simpleList.Count;
    }

    private void UpdateDisplay()
    {
        displayText.text = "-" + simpleList.ToString();
    }
}
