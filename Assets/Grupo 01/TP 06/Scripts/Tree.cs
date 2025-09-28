using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private MyBST<int> bst;
    public TreeVisualizer visualizer;


    void Start()
    {
        int[] myArray = { 20, 10, 1, 26, 35, 40, 18, 12, 15, 14, 30, 23 };
        bst = new MyBST<int>();
        


        foreach (var value in myArray)
        {
            bst.Insert(value);
        }

        Debug.Log("InOrder: " + string.Join(", ", bst.InOrder()));
        Debug.Log("PreOrder: " + string.Join(", ", bst.PreOrder()));
        Debug.Log("PostOrder: " + string.Join(", ", bst.PostOrder()));
        Debug.Log("LevelOrder: " + string.Join(", ", bst.LevelOrder()));

        Debug.Log("Altura del árbol: " + bst.GetHeight());
        Debug.Log("Balance factor de la raíz: " + bst.GetBalanceFactor(bst.Root));

        int height = bst.GetHeight();
        float spread = Mathf.Pow(1.5f, height); // cuanto más alto, más ancho
        visualizer.DrawTree(bst.Root, new Vector2(0, 0), spread);
        CenterCameraOnTree(bst.GetHeight(), 16f);


    }
    void CenterCameraOnTree(int height, float spread)
    {
        Camera cam = Camera.main;
        cam.orthographic = true;

        // Ajustar tamaño según altura del árbol
        cam.orthographicSize = height * 2f;

        // Centrar la cámara en el origen (donde dibujás la raíz)
        cam.transform.position = new Vector3(0, -5, -10);
    }


}