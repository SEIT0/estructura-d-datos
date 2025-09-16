using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void SimpleList()
    {
        SceneManager.LoadScene(1);
    }

    public void LinikedList()
    {
        SceneManager.LoadScene(2);
    }

    public void Shop()
    {
        SceneManager.LoadScene(3);
    }

    public void Misiones()
    {
        SceneManager.LoadScene(4);
    }

    public void Undo()
    {
        SceneManager.LoadScene(5);
    }

    public void Recursión()
    {
        SceneManager.LoadScene(6);
    }

    public void VolverAlMenu()
    {
        SceneManager.LoadScene(0);
    }

}
