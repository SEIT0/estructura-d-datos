using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] float speed = 5f;
    [SerializeField] Rigidbody2D rb;

    Vector2 moveDir;

    [Header("Misiones")]
    public MyQueue<string> misiones = new MyQueue<string>();
    [SerializeField] Text uiMisionActual;

    void Start()
    {        
        misiones.Enqueue("Toca el cuadrado rojo");
        misiones.Enqueue("Toca el cuadrado azul");
        misiones.Enqueue("Toca el cuadrado verde");

        ActualizarMisionUI();
    }

    void Update()
    {        
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(moveX, moveY).normalized;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDir * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (misiones.Count == 0) return;

        string misionActual = misiones.Peek();
        
        if (other.CompareTag("Mision") && other.name == misionActual)
        {
            misiones.Dequeue();
            Destroy(other.gameObject);
            ActualizarMisionUI();
        }
    }

    public void CumplirMisionActual()
    {
        if (misiones.Count == 0) return;
        misiones.Dequeue();
        ActualizarMisionUI();
    }

    void ActualizarMisionUI()
    {
        if (misiones.Count == 0)
        {
            uiMisionActual.text = "Misiones completadas";
        }
        else
        {
            uiMisionActual.text = "Misión:" + misiones.Peek();
        }
    }
}
