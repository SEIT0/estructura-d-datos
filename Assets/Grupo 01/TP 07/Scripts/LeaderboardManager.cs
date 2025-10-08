using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardManager : MonoBehaviour
{
    public TextMeshProUGUI leaderboardText; // Referencia al TMP en la escena
    private MyAVL<int> avlTree = new MyAVL<int>();

    void Start()
    {
        // Inicializar con 100 puntajes aleatorios
        for (int i = 0; i < 100; i++)
        {
            int randomScore = Random.Range(0, 1000);
            avlTree.Insert(randomScore);
        }

        RefreshUI();
    }

    public void AddScore(int score)
    {
        avlTree.Insert(score);
        RefreshUI();
    }

    private void RefreshUI()
    {
        // Obtener lista ordenada de mayor a menor
        List<int> scores = new List<int>();
        GetDescending(avlTree.Root, scores);

        // Construir string
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        int rank = 1;
        foreach (int score in scores)
        {
            sb.AppendLine(rank + ". " + score);
            rank++;
        }

        leaderboardText.text = sb.ToString();
    }

    private void GetDescending(BSTNode<int> node, List<int> list)
    {
        if (node == null) return;
        GetDescending(node.Right, list);
        list.Add(node.Value);
        GetDescending(node.Left, list);
    }

    // Métodos para los botones
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