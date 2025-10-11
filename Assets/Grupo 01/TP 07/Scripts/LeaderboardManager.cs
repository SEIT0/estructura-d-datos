using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText;
    public TMP_InputField scoreInputField;
    public Button insertButton;

    private MyAVL<int> avlTree = new MyAVL<int>();

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            int randomScore = Random.Range(0, 1000);
            avlTree.Insert(randomScore);
        }

        insertButton.onClick.AddListener(OnInsertButtonClicked);

        RefreshUI();
    }

    private void OnInsertButtonClicked()
    {
        if (int.TryParse(scoreInputField.text, out int score))
        {
            AddScore(score);
            scoreInputField.text = "";
        }
        else
        {
            Debug.LogWarning("Entrada inválida. Ingresá un número entero.");
        }
    }

    public void AddScore(int score)
    {
        avlTree.Insert(score);
        RefreshUI();
    }

    private void RefreshUI()
    {
        SimpleList<int> scores = new SimpleList<int>();
        GetDescending(avlTree.Root, scores);

        StringBuilder sb = new StringBuilder();
        int rank = 1;
        for (int i = 0; i < scores.Count; i++)
        {
            sb.AppendLine(rank + ". " + scores[i]);
            rank++;
        }

        leaderboardText.text = sb.ToString();
    }

    private void GetDescending(BSTNode<int> node, SimpleList<int> list)
    {
        if (node == null) return;
        GetDescending(node.Right, list);
        list.Add(node.Value);
        GetDescending(node.Left, list);
    }
    public void PrintPreOrder()
    {
        Debug.Log("PreOrder: " + string.Join(", ", avlTree.PreOrder()));
    }

    public void PrintInOrder()
    {
        Debug.Log("InOrder: " + string.Join(", ", avlTree.InOrder()));
    }

    public void PrintPostOrder()
    {
        Debug.Log("PostOrder: " + string.Join(", ", avlTree.PostOrder()));
    }

    public void PrintLevelOrder()
    {
        Debug.Log("LevelOrder: " + string.Join(", ", avlTree.LevelOrder()));
    }
}